using System.Collections;
using UnityEngine;

public class AI_Horse : HorseController
{
    [Header("AI Horse")]
    [SerializeField] public RaceController raceController;
    [SerializeField] FrontChecker frontChecker;

    public enum Strategy { Runway, Front, Stalker, Closer }
    [SerializeField] public Strategy strategy;
    [SerializeField] bool drawGizmo;

    [SerializeField] float wallDistance;
    [SerializeField] public float speed;
    [SerializeField] bool start, spurt;
    [SerializeField] public bool goalIn;
    [SerializeField] int turn;

    [SerializeField] Vector3 rotaDir, sideDir, turnDir;
    [SerializeField] Vector3 leftSight, fowardSight, rightSight;

    [ContextMenu("Setting")]
    public override void Initialize()
    {
        DataSetting();
        base.Initialize();
    }

    void DataSetting()
    {
        gameObject.name = horse.Data.horseName;
        goalIn = false;
        spurt = false;
        slipStream = 0;
        wallDistance = horse.Data.intelligence * 2f;
        leastStamina = horse.Data.stamina;
        horse.RestartAnimator();
        start = false;
    }

    public override void StartMove()
    {
        base.StartMove();
    }

    protected override IEnumerator StartDash()
    {
        yield return new WaitForSeconds(Random.Range(0f, (20f - horse.Data.intelligence) * 0.1f));
        start = true;
        speed = horse.Data.speed * 0.3f;
    }

    protected override IEnumerator URoutine()
    {
        while (!start)
            yield return null;

        while (true)
        {
            SpurtCheck();
            CheckWay();
            CalculateSpeed();
            CheckSideDistance();
            SetAnimatiton();
            yield return null;
        }
    }

    void CheckWay()
    {
        RaycastHit wallHit;

        if (Physics.Raycast(transform.position, transform.forward, out wallHit, wallDistance, LayerMask.GetMask("Wall")))
        {
            rotaDir = Vector3.ProjectOnPlane(transform.forward.normalized, wallHit.normal).normalized;
            rotaDir.y = transform.position.y;
        }
        turnDir = Vector3.Lerp(transform.forward, rotaDir, Time.deltaTime);
    }

    void CalculateSpeed()
    {
        if (goalIn)
            SpeedDown();
        else if(leastStamina <= 0f)
        {
            speed = horse.Data.speed * 0.5f;
        }
        else
        {
            switch (strategy)
            {
                case Strategy.Runway:
                    if (spurt)
                    {
                        speed = horse.Data.speed;
                        break;
                    }
                    if (rank <= 1)
                        SpeedDown();        // 1위라면 감속
                    else if (rank > 1)
                        SpeedUp();          // 2위 이하라면 가속
                    break;
                case Strategy.Front:
                    if (spurt)
                    {
                        speed = horse.Data.speed;
                        break;
                    }
                    if (rank <= raceController.raceSetting.strategy[0] + 1)
                        SpeedDown();        // 도주보다 빨라지면 감속
                    else
                        SpeedUp();          // 그 외에 가속
                    break;
                case Strategy.Stalker:
                    if (spurt)
                    {
                        speed = horse.Data.speed;
                        break;
                    }
                    if (rank <= raceController.raceSetting.strategy[0] + raceController.raceSetting.strategy[1] + 1)
                        SpeedDown();        // 선행보다 빨라지면 감속
                    else
                        SpeedUp();          // 그 외에 가속
                    break;
                case Strategy.Closer:
                    if (spurt)
                    {
                        speed = horse.Data.speed;
                        break;
                    }
                    if (rank <= raceController.raceSetting.strategy[0] + raceController.raceSetting.strategy[1] + raceController.raceSetting.strategy[2] + 1)
                        SpeedDown();        // 선입보다 빨라지면 감속
                    else
                        SpeedUp();          // 그 외에 가속
                    break;
            }
        }

        moveDir = transform.forward * (speed + 1f);
    }

    void SpeedUp()
    {
        if (speed < horse.Data.speed * 0.8f)
            speed += horse.Data.power * Time.deltaTime;
        else
            speed = horse.Data.speed * 0.8f;
    }

    void SpeedDown()
    {
        if (speed > horse.Data.speed * 0.5f)
            speed -= Time.deltaTime;
        else
            speed = horse.Data.speed * 0.5f;
    }

    void CheckSideDistance()
    {
        float leftDistance = wallDistance, rightDistance = wallDistance;

        if (Physics.Raycast(transform.position, -transform.right, out RaycastHit leftHit, wallDistance, LayerMask.GetMask("Wall")))
            leftDistance = leftHit.distance;
        if (Physics.Raycast(transform.position, transform.right, out RaycastHit rightHit, wallDistance, LayerMask.GetMask("Wall")))
            rightDistance = rightHit.distance;

        if (leftDistance > rightDistance && rightDistance < 3f)
        {
            sideDir = 0.2f * horse.Data.intelligence * -transform.right;
            turn = -1;
        }
        else if (leftDistance < rightDistance && leftDistance < 3f)
        {
            sideDir = 0.2f * horse.Data.intelligence * transform.right;
            turn = 1;
        }
        else
        {
            if (frontChecker.IsHorse)
            {
                if(raceController.raceSetting.isLeftRound)
                {
                    sideDir = 0.2f * horse.Data.intelligence * transform.right;
                    turn = 1;
                }
                else
                {
                    sideDir = 0.2f * horse.Data.intelligence * -transform.right;
                    turn = -1;
                }
            }
            else
            {
                sideDir = Vector3.Lerp(sideDir, Vector3.zero, Time.deltaTime);
                turn = 0;
            }
        }
    }

    protected override void SetAnimatiton()
    {
        if (leastStamina > 0f)
        {
            riderAnimator.SetBool("Run", true);
            if (speed >= horse.Data.speed * 0.9f)
                fowardAnimValue = Mathf.Lerp(fowardAnimValue, 1f, Time.deltaTime);
            else if (speed >= horse.Data.speed * 0.8f)
                fowardAnimValue = Mathf.Lerp(fowardAnimValue, 0.7f, Time.deltaTime);
            else if (speed >= horse.Data.speed * 0.7f)
                fowardAnimValue = Mathf.Lerp(fowardAnimValue, 0.5f, Time.deltaTime);
            else if (speed >= horse.Data.speed * 0.6f)
                fowardAnimValue = Mathf.Lerp(fowardAnimValue, 0.3f, Time.deltaTime);
            else if (speed >= horse.Data.speed * 0.1f)
                fowardAnimValue = Mathf.Lerp(fowardAnimValue, 0.1f, Time.deltaTime);
            else
                fowardAnimValue = Mathf.Lerp(fowardAnimValue, 0f, Time.deltaTime);
        }
        else
        {
            fowardAnimValue = Mathf.Lerp(fowardAnimValue, 0.1f, Time.deltaTime);
            riderAnimator.SetBool("Run", false);
        }

        if (turn > 0)
            turnAnimValue = Mathf.Lerp(turnAnimValue, 1f, Time.deltaTime);
        else if (turn < 0)
            turnAnimValue = Mathf.Lerp(turnAnimValue, -1f, Time.deltaTime);
        else
            turnAnimValue = Mathf.Lerp(turnAnimValue, 0f, Time.deltaTime);

        horseAnimator.SetFloat("Foward", fowardAnimValue);
        horseAnimator.SetFloat("Side", turnAnimValue);
    }

    void SpurtCheck()
    {
        switch (strategy)
        {
            case Strategy.Runway:
                if (totalMoveDistance >= raceController.raceDistanceChecker.totalDistance * 0.95f)
                    spurt = true;
                break;
            case Strategy.Front:
                if (totalMoveDistance >= raceController.raceDistanceChecker.totalDistance * 0.9f)
                    spurt = true;
                break;
            case Strategy.Stalker:
                if (totalMoveDistance >= raceController.raceDistanceChecker.totalDistance * 0.9f)
                    spurt = true;
                break;
            case Strategy.Closer:
                if (totalMoveDistance >= raceController.raceDistanceChecker.totalDistance * 0.8f)
                    spurt = true;
                break;
        }
    }

    protected override IEnumerator FRoutine()
    {
        while (!start)
            yield return null;

        while (true)
        {
            Move();
            yield return new WaitForFixedUpdate();
        }
    }

    void Move()
    {
        rb.AddForce(moveDir + sideDir + Vector3.up, ForceMode.Acceleration);
        transform.LookAt(turnDir + transform.position);
    }

    protected override IEnumerator StaminaComsume()
    {
        while (!start)
            yield return null;
        while (true)
        {
            float consume = (speed - horse.Data.intelligence * 0.1f) * 0.015f;
            if (turn != 0)
                consume += horse.Data.speed * 0.01f;
            consume = (consume < 0.1f ? 0.1f : consume);
            if (slipStream > 0)
                consume *= 0.5f;
            leastStamina -= consume;
            if (leastStamina < 0f)
            {
                StartCoroutine(ExhaustionRoutine());
                leastStamina = 0f;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    void OnDrawGizmos()
    {
        if (!drawGizmo)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * wallDistance);
        Gizmos.DrawLine(transform.position, transform.position + transform.right * wallDistance);
        Gizmos.DrawLine(transform.position, transform.position - transform.right * wallDistance);
    }
}

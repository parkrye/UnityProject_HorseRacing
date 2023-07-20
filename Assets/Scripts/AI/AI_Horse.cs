using System.Collections;
using UnityEngine;

public class AI_Horse : HorseController
{
    [Header("AI Horse")]
    [SerializeField] FewerWayChecker fewerWayChecker;
    enum Strategy { Runway, Front, Stalker, Closer }
    [SerializeField] Strategy strategy;
    [SerializeField] bool drawGizmo;

    [SerializeField] float wallDistance, speed;
    [SerializeField] bool start;
    [SerializeField] int turn, spurt;
    [SerializeField] int leftCount = 0, fowardCount = 0, rightCount = 0;

    [SerializeField] Vector3 rotaDir, sideDir, turnDir;
    [SerializeField] Vector3 leftSight, fowardSight, rightSight;

    [ContextMenu("Initialize")]
    public override void Initialize()
    {
        RaceSetting();
        base.Initialize();
    }

    void RaceSetting()
    {
        spurt = 0;
        switch (strategy)
        {
            case Strategy.Runway:
                break;
            case Strategy.Front:
                break;
            case Strategy.Stalker:
                break;
            case Strategy.Closer:
                break;
        }
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
    }

    protected override IEnumerator URoutine()
    {
        while (!start)
            yield return null;

        while (true)
        {
            CheckWay();
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

        switch (strategy)
        {
            case Strategy.Runway:
                if (spurt >= 4)
                {
                    speed = horse.Data.speed;
                    break;
                }
                if (rank == 1)          // 1위라면 감속
                    SpeedDown();
                else if (rank > 1)      // 2위 이하라면 가속
                    SpeedUp();
                break;
            case Strategy.Front:
                if (spurt >= 3)
                {
                    speed = horse.Data.speed;
                    break;
                }
                if (rank == 1)          // 1위라면 감속
                    SpeedDown();
                else if (rank > 4)      // 5위 이하라면 가속
                    SpeedUp();
                break;
            case Strategy.Stalker:
                if (spurt >= 2)
                {
                    speed = horse.Data.speed;
                    break;
                }
                if (rank <= 2)          // 2위 이상이라면 감속
                    SpeedDown();
                else if (rank > 6)      // 7위 이하라면 가속
                    SpeedUp();
                break;
            case Strategy.Closer:
                if (spurt >= 1)
                {
                    speed = horse.Data.speed;
                    break;
                }
                if (rank <= 6)          // 6위 이상이라면 감속
                    SpeedDown();
                else if (rank == 8)     // 8위라면 가속
                    SpeedUp();
                break;
        }

        if (leastStamina <= 0f)
            moveDir = transform.forward * (speed * 0.5f + 1f);
        else
            moveDir = transform.forward * (speed + 1f);
    }

    void SpeedUp()
    {
        if (speed < horse.Data.speed)
            speed += horse.Data.power * Time.deltaTime;
        else
            speed = horse.Data.speed;
    }

    void SpeedDown()
    {
        if (speed > 0f)
            speed -= Time.deltaTime;
        else
            speed = 0f;
    }

    void CheckSideDistance()
    {
        float leftDistance = wallDistance, rightDistance = wallDistance;

        if (Physics.Raycast(transform.position, -transform.right, out RaycastHit leftHit, wallDistance, LayerMask.GetMask("Wall")))
            leftDistance = leftHit.distance;
        if (Physics.Raycast(transform.position, transform.right, out RaycastHit rightHit, wallDistance, LayerMask.GetMask("Wall")))
            rightDistance = rightHit.distance;

        if (leftDistance > rightDistance && rightDistance < 5f)
        {
            sideDir = 0.5f * horse.Data.intelligence * -transform.right;
            turn = -1;
        }
        else if (leftDistance < rightDistance && leftDistance < 5f)
        {
            sideDir = 0.5f * horse.Data.intelligence * transform.right;
            turn = 1;
        }
        else
        {
            if(fewerWayChecker.HorseList.Count > 0)
            {
                leftSight = (transform.forward - transform.right) + transform.position;
                fowardSight = transform.forward + transform.position;
                rightSight = (transform.forward + transform.right) + transform.position;
                leftCount = 0; fowardCount = 0; rightCount = 0;

                for (int i = fewerWayChecker.HorseList.Count - 1; i >= 0; i--)
                {
                    Vector3 horsePos = fewerWayChecker.HorseList[i].transform.position;
                    float leftDifference = Vector3.SqrMagnitude(leftSight - horsePos);
                    float fowardDifference = Vector3.SqrMagnitude(fowardSight - horsePos);
                    float rightDifference = Vector3.SqrMagnitude(rightSight - horsePos);
                    if (leftDifference <= fowardDifference && leftDifference <= rightDifference)
                        leftCount++;
                    else if (rightDifference <= leftDifference && rightDifference <= fowardDifference)
                        rightCount++;
                    else
                        fowardCount++;
                }

                if (leftCount < fowardCount && leftCount < rightCount)
                {
                    sideDir = 0.5f * horse.Data.intelligence * -transform.right;
                    turn = -1;
                }
                else if (rightCount < leftCount && rightCount < fowardCount)
                {
                    sideDir = 0.5f * horse.Data.intelligence * transform.right;
                    turn = 1;
                }
                else
                {
                    sideDir = Vector3.Lerp(sideDir, Vector3.zero, Time.deltaTime);
                    turn = 0;
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
        rb.AddForce(moveDir + sideDir, ForceMode.Acceleration);
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

    public void Spurt()
    {
        spurt++;
    }

    void OnDrawGizmos()
    {
        if (!drawGizmo)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * wallDistance);
        Gizmos.DrawLine(transform.position, transform.position + transform.right * wallDistance);
        Gizmos.DrawLine(transform.position, transform.position - transform.right * wallDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + transform.up, leftSight + transform.up);
        Gizmos.DrawLine(transform.position + transform.up, fowardSight + transform.up);
        Gizmos.DrawLine(transform.position + transform.up, rightSight + transform.up);
    }
}

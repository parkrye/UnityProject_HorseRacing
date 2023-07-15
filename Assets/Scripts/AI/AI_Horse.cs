using System.Collections;
using UnityEngine;

public class AI_Horse : HorseController
{
    enum Strategy { Runner, Stalker, Closer }
    [SerializeField] Strategy strategy;

    [SerializeField] float wallDistance;
    [SerializeField] bool start;
    [SerializeField] float[] steps;
    [SerializeField] int step, turn;

    [SerializeField] Vector3 rotaDir, sideDir;

    [ContextMenu("Initialize")]
    public override void Initialize()
    {
        RaceSetting();
        base.Initialize();
    }

    void RaceSetting()
    {
        step = 0;
        steps = new float[3];
        switch (strategy)
        {
            case Strategy.Runner:
                steps[0] = 0.9f;
                steps[1] = 0.9f;
                steps[2] = 0.7f;
                break;
            case Strategy.Stalker:
                steps[0] = 0.83f;
                steps[1] = 0.83f;
                steps[2] = 0.84f;
                break;
            case Strategy.Closer:
                steps[0] = 0.7f;
                steps[1] = 0.9f;
                steps[2] = 0.9f;
                break;
        }
        slipStream = 0;
        wallDistance = horse.Data.intelligence;
        leastStamina = horse.Data.stamina;
        start = false;
    }

    protected override void StartMove()
    {
        base.StartMove();
    }

    protected override IEnumerator StartDash()
    {
        yield return new WaitForSeconds(Random.Range(0f, (40f - horse.Data.intelligence) * 0.1f));
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
            rotaDir = Vector3.ProjectOnPlane(transform.forward.normalized, wallHit.normal).normalized;
        else
            rotaDir = transform.forward.normalized;
        rotaDir.y = transform.forward.y;

        moveDir = transform.forward;

        if (leastStamina > 0f)
            moveDir *= horse.Data.speed * steps[step];
        else
            moveDir *= horse.Data.speed * 0.5f;
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
            sideDir = -transform.right * horse.Data.intelligence * 0.5f;
            turn = -1;
        }
        else if (leftDistance < rightDistance && leftDistance < 5f)
        {
            sideDir = transform.right * horse.Data.intelligence * 0.5f;
            turn = 1;
        }
        else
        {
            sideDir = Vector3.Lerp(sideDir, Vector3.zero, Time.deltaTime);
            turn = 0;
        }
    }

    protected override void SetAnimatiton()
    {
        if (leastStamina > 0f)
        {
            if (steps[step] >= 0.9f)
                fowardAnimValue = Mathf.Lerp(fowardAnimValue, 1f, Time.deltaTime);
            else if (steps[step] >= 0.8f)
                fowardAnimValue = Mathf.Lerp(fowardAnimValue, 0.7f, Time.deltaTime);
            else if (steps[step] >= 0.7f)
                fowardAnimValue = Mathf.Lerp(fowardAnimValue, 0.5f, Time.deltaTime);
            else
                fowardAnimValue = Mathf.Lerp(fowardAnimValue, 0f, Time.deltaTime);
        }
        else
            fowardAnimValue = Mathf.Lerp(fowardAnimValue, 0f, Time.deltaTime);


        if (turn > 0f)
            turnAnimValue = Mathf.Lerp(turnAnimValue, 1f, Time.deltaTime);
        else if (turn < 0f)
            turnAnimValue = Mathf.Lerp(turnAnimValue, -1f, Time.deltaTime);
        else
            turnAnimValue = Mathf.Lerp(turnAnimValue, 0f, Time.deltaTime);

        animator.SetFloat("Foward", fowardAnimValue);
        animator.SetFloat("Side", turnAnimValue);
    }

    protected override IEnumerator FRoutine()
    {
        while (!start)
            yield return null;

        while (true)
        {
            Move();
            SideMove();
            yield return new WaitForFixedUpdate();
        }
    }

    void Move()
    {
        rb.AddForce(moveDir, ForceMode.Acceleration);
        transform.LookAt(Vector3.Lerp(transform.position + transform.forward, transform.position + rotaDir, Time.deltaTime));
    }

    void SideMove()
    {
        rb.AddForce(sideDir, ForceMode.Acceleration);
    }

    protected override IEnumerator StaminaComsume()
    {
        while (!start)
            yield return null;
        while (true)
        {
            float consume = (horse.Data.speed * steps[step] - horse.Data.intelligence * 0.5f) * 0.015f;
            consume = (consume < 0.1f ? 0.1f : consume);
            if (slipStream > 0)
                consume *= 0.5f;
            leastStamina -= consume;
            if (leastStamina < 0f)
                leastStamina = 0f;
            yield return new WaitForSeconds(1f);
        }
    }

    public void StepUp(int index)
    {
        if (index <= 0 || index > 2)
            return;
        if (index > step)
            step = index;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position + transform.up, transform.position + transform.forward * wallDistance + transform.up);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Horse : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Horse horse;

    [SerializeField] float wallDistance;
    [SerializeField] float speed;

    [SerializeField] Vector3 moveDir, rotaDir;

    [ContextMenu("Initialize")]
    public void Initialize()
    {
        RaceSetting();
        StartMove();
    }

    void RaceSetting()
    {
        speed = horse.Data.speed * 0.3f;
    }

    public void StartMove()
    {
        StartDash();

        StartCoroutine(URoutine());
        StartCoroutine(FRoutine());
    }

    void StartDash()
    {

    }

    IEnumerator URoutine()
    {
        while (true)
        {
            CheckWay();
            yield return null;
        }
    }

    void CheckWay()
    {
        RaycastHit wallHit;
        moveDir = transform.forward * speed;

        if (Physics.Raycast(transform.position, transform.forward, out wallHit, wallDistance, LayerMask.GetMask("Wall")))
        {
            if (wallHit.distance <= wallDistance)
                rotaDir = Vector3.ProjectOnPlane(transform.forward, wallHit.normal).normalized;
            else
                rotaDir = transform.forward.normalized;
        }
        else
        {
            rotaDir = transform.forward.normalized;
        }
    }

    IEnumerator FRoutine()
    {
        while(true)
        {
            Move();
            yield return new WaitForFixedUpdate();
        }
    }

    void Move()
    {
        rb.AddForce(moveDir, ForceMode.Force);
        transform.LookAt(Vector3.Lerp(transform.position + transform.forward, transform.position + rotaDir, Time.deltaTime));
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + transform.up, transform.position + transform.forward * wallDistance + transform.up);
    }
}

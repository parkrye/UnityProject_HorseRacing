using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator animator;

    [SerializeField] Horse horse;

    [Header("Movement Values")]
    [SerializeField] Vector3 moveDir;
    [SerializeField] float sideInput, turnInput, speedInput;
    [SerializeField] float nowFowardSpeedValue, nowSideSpeedValue, nowTurnValue;
    [SerializeField] float sideMoveModifier, turnModifier;

    [Header("Animation Values")]
    [SerializeField] int idleMotionValue;
    [SerializeField] float fowardAnimValue, turnAnimValue;


    [ContextMenu("Initialize")]
    public void Initialize()
    {
        StartCoroutine(IdleMotionRoutine());
        StatMove();
    }

    public void StatMove()
    {
        StartDash();

        StartCoroutine(URoutine());
        StartCoroutine(FRoutine());
    }

    void StartDash()
    {
        nowFowardSpeedValue += horse.Data.power * 0.5f;
    }

    IEnumerator URoutine()
    {
        while (true)
        {
            CalcMoveData();
            CalcAnimation();
            yield return null;
        }
    }

    void CalcMoveData()
    {
        if (speedInput > 0.5f || speedInput < -0.5f)
        {
            if(nowFowardSpeedValue <= horse.Data.speed)
                nowFowardSpeedValue = horse.Data.speed;
            else if (nowFowardSpeedValue >= -horse.Data.speed)
                nowFowardSpeedValue = horse.Data.speed * speedInput;
            else
                nowFowardSpeedValue += horse.Data.power * speedInput * Time.deltaTime;
        }

        if (sideInput != 0f)
        {
            nowSideSpeedValue = horse.Data.power * sideMoveModifier * sideInput;
        }
        else
        {
            nowSideSpeedValue = 0f;
        }

        moveDir = transform.forward * nowFowardSpeedValue + transform.right * nowSideSpeedValue;
    }

    void CalcAnimation()
    {
        if(nowFowardSpeedValue >= horse.Data.speed)
        {
            fowardAnimValue = Mathf.Lerp(fowardAnimValue, 1f, Time.deltaTime);
        }
        else if(nowFowardSpeedValue >= horse.Data.speed * 0.7f)
        {
            fowardAnimValue = Mathf.Lerp(fowardAnimValue, 0.7f, Time.deltaTime);
        }
        else if(nowFowardSpeedValue >= horse.Data.speed * 0.5f)
        {
            fowardAnimValue = Mathf.Lerp(fowardAnimValue, 0.5f, Time.deltaTime);
        }
        else if(nowFowardSpeedValue >= horse.Data.speed * 0.3f)
        {
            fowardAnimValue = Mathf.Lerp(fowardAnimValue, 0.3f, Time.deltaTime);
        }
        else if (nowFowardSpeedValue >= horse.Data.speed * 0.1f)
        {
            fowardAnimValue = Mathf.Lerp(fowardAnimValue, 0.1f, Time.deltaTime);
        }
        else
        {
            fowardAnimValue = Mathf.Lerp(fowardAnimValue, 0f, Time.deltaTime);
        }

        if(turnInput > 0f)
        {
            turnAnimValue = Mathf.Lerp(turnAnimValue, 1f, Time.deltaTime);
        }
        else if(turnInput < 0f)
        {
            turnAnimValue = Mathf.Lerp(turnAnimValue, -1f, Time.deltaTime);
        }
        else
        {
            turnAnimValue = Mathf.Lerp(turnAnimValue, 0f, Time.deltaTime);
        }

        animator.SetFloat("Foward", fowardAnimValue);
        animator.SetFloat("Side", turnAnimValue);
    }

    IEnumerator FRoutine()
    {
        while(true) 
        {
            MoveHorse();
            yield return new WaitForFixedUpdate();
        }
    }

    void MoveHorse()
    {
        rb.AddForce(moveDir, ForceMode.Force);
        transform.Rotate(turnInput * turnModifier * Vector3.up);
    }

    IEnumerator IdleMotionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            idleMotionValue = Random.Range(0, 5);
            animator.SetFloat("Idle", idleMotionValue);
        }
    }

    void OnSideMove(InputValue inputValue)
    {
        sideInput = inputValue.Get<float>();
    }

    void OnTurnMove(InputValue inputValue)
    {
        turnInput = inputValue.Get<float>();
    }

    void OnSpeedChange(InputValue inputValue)
    {
        speedInput = inputValue.Get<float>();
    }
}

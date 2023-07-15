using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : HorseController
{
    [Header("Movement Values")]
    [SerializeField] float sideInput, turnInput, speedInput;
    [SerializeField] float nowFowardSpeedValue, nowSideSpeedValue, nowTurnValue;
    [SerializeField] float sideMoveModifier, turnModifier;

    [ContextMenu("Initialize")]
    public override void Initialize()
    {
        leastStamina = horse.Data.stamina;
        base.Initialize();
    }

    protected override void StartMove()
    {
        base.StartMove();
    }

    protected override IEnumerator StartDash()
    {
        yield return null;
        nowFowardSpeedValue += horse.Data.power * horse.Data.intelligence * 0.1f;
    }

    protected override IEnumerator URoutine()
    {
        while (true)
        {
            CalcMoveData();
            SetAnimatiton();
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
            nowSideSpeedValue = horse.Data.power * sideMoveModifier * sideInput;
        else
            nowSideSpeedValue = 0f;

        moveDir = transform.forward * nowFowardSpeedValue + transform.right * nowSideSpeedValue;
    }

    protected override void SetAnimatiton()
    {
        if(nowFowardSpeedValue >= horse.Data.speed * 0.1f)
        {
            riderAnimator.SetBool("Run", true);
            if (nowFowardSpeedValue >= horse.Data.speed)
                fowardAnimValue = Mathf.Lerp(fowardAnimValue, 1f, Time.deltaTime);
            else if (nowFowardSpeedValue >= horse.Data.speed * 0.7f)
                fowardAnimValue = Mathf.Lerp(fowardAnimValue, 0.7f, Time.deltaTime);
            else if (nowFowardSpeedValue >= horse.Data.speed * 0.5f)
                fowardAnimValue = Mathf.Lerp(fowardAnimValue, 0.5f, Time.deltaTime);
            else if (nowFowardSpeedValue >= horse.Data.speed * 0.3f)
                fowardAnimValue = Mathf.Lerp(fowardAnimValue, 0.3f, Time.deltaTime);
            else
                fowardAnimValue = Mathf.Lerp(fowardAnimValue, 0.1f, Time.deltaTime);
        }
        else
        {
            riderAnimator.SetBool("Run", false);
            fowardAnimValue = Mathf.Lerp(fowardAnimValue, 0f, Time.deltaTime);
        }

        if(turnInput > 0f)
            turnAnimValue = Mathf.Lerp(turnAnimValue, 1f, Time.deltaTime);
        else if(turnInput < 0f)
            turnAnimValue = Mathf.Lerp(turnAnimValue, -1f, Time.deltaTime);
        else
            turnAnimValue = Mathf.Lerp(turnAnimValue, 0f, Time.deltaTime);

        horseAnimator.SetFloat("Foward", fowardAnimValue);
        horseAnimator.SetFloat("Side", turnAnimValue);
    }

    protected override IEnumerator FRoutine()
    {
        while(true) 
        {
            MoveHorse();
            yield return new WaitForFixedUpdate();
        }
    }

    void MoveHorse()
    {
        rb.AddForce(moveDir, ForceMode.Acceleration);
        transform.Rotate(turnInput * turnModifier * Vector3.up);
    }

    protected override IEnumerator StaminaComsume()
    {
        while (true)
        {
            float consume = (nowFowardSpeedValue + nowSideSpeedValue - horse.Data.intelligence * 0.5f) * 0.02f;
            consume = (consume < 0.1f ? 0.1f : consume);
            if (slipStream > 0)
                consume *= 0.5f;
            leastStamina -= consume;
            if (leastStamina < 0f)
                leastStamina = 0f;
            yield return new WaitForSeconds(1f);
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

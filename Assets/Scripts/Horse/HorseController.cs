using System.Collections;
using UnityEngine;

public abstract class HorseController : MonoBehaviour
{
    [Header ("Horse Controller")]
    [SerializeField] protected Rigidbody rb;
    [SerializeField] public Horse horse;
    [SerializeField] protected Animator horseAnimator, riderAnimator;

    [SerializeField] protected int slipStream;
    [SerializeField] protected float leastStamina;
    [SerializeField] protected bool exhaustion;

    [SerializeField] protected float fowardAnimValue, turnAnimValue;
    [SerializeField] public float rankWeight;
    [SerializeField] public RankCheckerZone rankZone;

    [SerializeField] protected Vector3 moveDir;

    [ContextMenu("Initialize")]
    public virtual void Initialize()
    {
        StartCoroutine(IdleMotionRoutine());
    }

    public virtual void StartMove()
    {
        StartCoroutine(StartDash());

        StartCoroutine(URoutine());
        StartCoroutine(FRoutine());
        StartCoroutine(StaminaComsume());
    }

    protected abstract IEnumerator StartDash();

    protected abstract IEnumerator URoutine();

    protected abstract void SetAnimatiton();

    protected abstract IEnumerator FRoutine();

    protected abstract IEnumerator StaminaComsume();

    protected IEnumerator ExhaustionRoutine()
    {
        if (!exhaustion)
        {
            exhaustion = true;
            yield return new WaitForSeconds((60 - horse.Data.intelligence));
            leastStamina = horse.Data.stamina * 0.5f;
            exhaustion = false;
        }
    }

    protected IEnumerator IdleMotionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            horseAnimator.SetFloat("Idle", Random.Range(0, 5));
        }
    }

    public void OnCollideEvent(AI_Horse target)
    {
        float selfPower = Random.Range(0f, horse.Data.power);
        float targetPower = Random.Range(0f, target.horse.Data.power);

        Vector3 selfDir = (transform.position - target.transform.position).normalized;
        Vector3 targetDir = -selfDir;

        CompetitionMove(selfDir, selfPower);
        target.CompetitionMove(targetDir, targetPower);
    }

    public void CompetitionMove(Vector3 dir, float power)
    {
        rb.AddForce(dir * power, ForceMode.Acceleration);
        leastStamina -= Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SlipStreamZone"))
            slipStream++;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SlipStreamZone"))
            slipStream--;
    }
}

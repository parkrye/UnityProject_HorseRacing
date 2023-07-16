using System.Collections;
using UnityEngine;

public abstract class HorseController : MonoBehaviour
{
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected Horse horse;
    [SerializeField] protected Animator horseAnimator, riderAnimator;
    [SerializeField] protected CompetitionChecker leftChecker, rightChecker;

    [SerializeField] protected int slipStream;
    [SerializeField] protected float leastStamina;
    [SerializeField] protected bool exhaustion;

    [SerializeField] protected float fowardAnimValue, turnAnimValue;

    [SerializeField] protected Vector3 moveDir;

    [ContextMenu("Initialize")]
    public virtual void Initialize()
    {
        StartCoroutine(IdleMotionRoutine());
        StartMove();
    }

    protected virtual void StartMove()
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

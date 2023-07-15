using System.Collections;
using UnityEngine;

public abstract class HorseController : MonoBehaviour
{
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected Horse horse;
    [SerializeField] protected Animator animator;
    [SerializeField] protected CompetitionChecker leftChecker, rightChecker;

    [SerializeField] protected int slipStream;
    [SerializeField] protected float leastStamina;

    [SerializeField] protected int idleMotionValue;
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

    protected IEnumerator IdleMotionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            idleMotionValue = Random.Range(0, 5);
            animator.SetFloat("Idle", idleMotionValue);
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

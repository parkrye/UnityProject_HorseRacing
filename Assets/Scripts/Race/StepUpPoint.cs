using UnityEngine;

public class StepUpPoint : MonoBehaviour
{
    [SerializeField] int step;

    void OnTriggerEnter(Collider other)
    {
        AI_Horse ai = other.GetComponent<AI_Horse>();
        if (ai)
        {
            ai.StepUp(step);
        }
    }
}

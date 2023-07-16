using UnityEngine;
using UnityEngine.Events;

public class CompetitionChecker : MonoBehaviour
{
    public UnityEvent<AI_Horse> CollideEvent;

    void OnTriggerStay(Collider other)
    {
        AI_Horse target = other.GetComponent<AI_Horse>();
        if(target)
        {
            CollideEvent?.Invoke(target);
        }
    }
}

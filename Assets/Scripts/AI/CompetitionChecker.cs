using UnityEngine;
using UnityEngine.Events;

public class CompetitionChecker : MonoBehaviour
{
    public UnityEvent<HorseController> CollideEvent;

    void OnTriggerStay(Collider other)
    {
        HorseController target = other.GetComponent<HorseController>();
        if(target)
        {
            CollideEvent?.Invoke(target);
        }
    }
}

using UnityEngine;

public class SpurtPoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        AI_Horse ai = other.GetComponent<AI_Horse>();
        if (ai)
        {
            ai.Spurt();
        }
    }
}

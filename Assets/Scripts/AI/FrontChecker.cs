using UnityEngine;

public class FrontChecker : MonoBehaviour
{
    [SerializeField] int isHorse;
    public bool IsHorse { get { return isHorse > 0; } }

    void OnTriggerEnter(Collider other)
    {
        HorseController horse = other.GetComponent<HorseController>();
        if (horse)
        {
            isHorse++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        HorseController horse = other.GetComponent<HorseController>();
        if (horse)
        {
            isHorse--;
        }
    }
}

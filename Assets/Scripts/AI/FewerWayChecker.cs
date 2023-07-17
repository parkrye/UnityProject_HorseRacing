using System.Collections.Generic;
using UnityEngine;

public class FewerWayChecker : MonoBehaviour
{
    [SerializeField] List<Horse> horseList;
    public List<Horse> HorseList { get { return horseList; } }

    void Awake()
    {
        horseList = new List<Horse>();
    }

    void OnTriggerEnter(Collider other)
    {
        HorseController ai = other.GetComponent<HorseController>();
        if(ai) 
        {
            horseList.Add(ai.horse);
        }
    }

    void OnTriggerExit(Collider other)
    {
        HorseController ai = other.GetComponent<HorseController>();
        if (ai)
        {
            horseList.Remove(ai.horse);
        }
    }
}

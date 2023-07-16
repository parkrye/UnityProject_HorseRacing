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
        Horse ai = other.GetComponent<Horse>();
        if(ai) 
        {
            horseList.Add(ai);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Horse ai = other.GetComponent<Horse>();
        if (ai)
        {
            horseList.Remove(ai);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompetitionChecker : MonoBehaviour
{
    [SerializeField] List<AI_Horse> sideHorses;
    public bool OnSide { get { return sideHorses.Count > 0; } }
    public List<AI_Horse> SideHorses { get { return sideHorses; } }

    void Awake()
    {
        sideHorses = new List<AI_Horse>();
    }

    void OnTriggerEnter(Collider other)
    {
        AI_Horse ai = other.GetComponent<AI_Horse>();
        if(ai)
            SideHorses.Add(ai);
    }

    void OnTriggerExit(Collider other)
    {
        AI_Horse ai = other.GetComponent<AI_Horse>();
        if (ai)
            SideHorses.Remove(ai);
    }
}

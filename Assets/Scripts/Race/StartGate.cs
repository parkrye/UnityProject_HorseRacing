using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGate : MonoBehaviour
{
    [SerializeField] List<Horse> horseList;
    [SerializeField] List<AI_Horse> aiList;
    [SerializeField] float delayTime;

    void Awake()
    {
        horseList = new List<Horse>();
        aiList = new List<AI_Horse>();
    }

    void Start()
    {
        StartCoroutine(StartRace());
    }

    IEnumerator StartRace()
    {
        yield return null;
        for (int i = 0; i < horseList.Count; i++)
            horseList[i].Initialize();
        yield return new WaitForSeconds(Random.Range(delayTime * 0.5f, delayTime * 2f));
        for(int i = 0; i < aiList.Count; i++)
            aiList[i].Initialize();
    }

    void OnTriggerEnter(Collider other)
    {
        Horse horse = other.GetComponent<Horse>();
        if (horse)
        {
            horseList.Add(horse);
            AI_Horse ai = horse.transform.parent.GetComponent<AI_Horse>();
            if (ai)
                aiList.Add(ai);
        }
    }
}

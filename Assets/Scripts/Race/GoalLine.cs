using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalLine : MonoBehaviour
{
    [SerializeField] RaceController raceController;
    [SerializeField] Queue<(AI_Horse, float)> horseQueue;
    [SerializeField] float time;

    public void Initialize()
    {
        horseQueue = new Queue<(AI_Horse, float)>();
        StartCoroutine(TimeRotuine());
    }

    IEnumerator TimeRotuine()
    {
        while (true)
        {
            yield return null;
            time += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        AI_Horse aI_Horse = other.GetComponent<AI_Horse>();
        if(aI_Horse)
        {
            if(aI_Horse.totalMoveDistance >= raceController.raceDistanceChecker.totalDistance * 0.8f)
            {
                aI_Horse.goalIn = true;
                horseQueue.Enqueue((aI_Horse, time));
                Debug.Log((aI_Horse.name, time));
            }
        }
    }
}

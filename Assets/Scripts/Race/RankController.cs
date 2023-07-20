using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankController : MonoBehaviour
{
    [SerializeField] RaceController raceController;
    [SerializeField] List<RankCheckerZone> zones;
    [SerializeField] PriorityQueue<HorseController, float> rankPQ;

    public void Initialize()
    {
        rankPQ = new PriorityQueue<HorseController, float>(false);

        StartCoroutine(RankRoutine());
    }

    IEnumerator RankRoutine()
    {
        yield return null;
        while (true)
        {
            for (int i = 0; i < raceController.Horses.Count; i++)
            {
                float rankWeight = raceController.Horses[i].rankWeight * 100;
                float distance = Vector3.SqrMagnitude(raceController.Horses[i].transform.position - raceController.Horses[i].rankZone.checkPoint.position) * 0.0001f;
                rankPQ.Enqueue(raceController.Horses[i], rankWeight + distance);
            }

            for(int i = 0; i < raceController.Horses.Count; i++)
            {
                Debug.Log($"{i+1}À§ : {rankPQ.Dequeue().name}");
            }

            yield return null;
        }
    }
}

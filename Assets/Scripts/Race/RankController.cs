using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankController : MonoBehaviour
{
    [SerializeField] RaceController raceController;
    [SerializeField] List<RankCheckerZone> zones;
    [SerializeField] PriorityQueue<HorseController, float> rankPQ;
    [SerializeField] Dictionary<HorseController, int> rank;

    public void Initialize()
    {
        rankPQ = new PriorityQueue<HorseController, float>(false);
        rank = new Dictionary<HorseController, int>();

        StartCoroutine(RankRoutine());
    }

    IEnumerator RankRoutine()
    {
        yield return null;
        while (true)
        {
            for (int i = 0; i < raceController.Horses.Count; i++)
            {
                float rankWeight = raceController.Horses[i].rankWeight * 1000000;
                Vector3 distanceVec = raceController.Horses[i].transform.position - raceController.Horses[i].rankZone.checkPoint.position;
                float distance = Vector3.SqrMagnitude(distanceVec);
                raceController.Horses[i].inZoneMoveDistance = distance;
                rankPQ.Enqueue(raceController.Horses[i], rankWeight + distance);
            }

            for(int i = 0; i < raceController.Horses.Count; i++)
            {
                HorseController horse = rankPQ.Dequeue();
                rank[horse] = i + 1;
                horse.rank = rank[horse];
            }

            yield return null;
        }
    }
}

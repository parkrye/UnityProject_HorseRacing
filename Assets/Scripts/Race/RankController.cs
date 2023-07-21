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
                float rankWeight = raceController.Horses[i].rankWeight * 100;
                float distance = Vector3.SqrMagnitude(raceController.Horses[i].transform.position - raceController.Horses[i].rankZone.checkPoint.position) * 0.0001f;
                rankPQ.Enqueue(raceController.Horses[i], rankWeight + distance);
            }

            for(int i = 0; i < raceController.Horses.Count; i++)
            {
                HorseController horse = rankPQ.Dequeue();
                rank[horse] = i + 1;
                horse.rank = rank[horse];
                Debug.Log($"{rank[horse]}À§ : {horse.name}, {((AI_Horse)horse).strategy}, {((AI_Horse)horse).speed == horse.horse.Data.speed}");
            }

            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankController : MonoBehaviour
{
    [SerializeField] RaceController raceController;
    [SerializeField] List<RankCheckerZone> zones;
    [SerializeField] Dictionary<HorseController, float> rankDict;

    public void Initialize()
    {
        rankDict = new Dictionary<HorseController, float>();
        for(int i = 0; i < raceController.Horses.Count; i++)
        {
            rankDict[raceController.Horses[i]] = 0f;
        }

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
                rankDict[raceController.Horses[i]] = rankWeight + distance;
            }
            yield return null;
        }
    }
}

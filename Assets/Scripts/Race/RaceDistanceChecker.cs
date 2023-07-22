using System.Collections.Generic;
using UnityEngine;

public class RaceDistanceChecker : MonoBehaviour
{
    [SerializeField] List<Transform> checkPoints;

    [SerializeField] public float totalDistance;
    [SerializeField] public float reverseTotalDistance;

    public void Initialize()
    {
        for(int i = 0; i < checkPoints.Count; i += 2)
        {
            totalDistance += Vector3.SqrMagnitude(checkPoints[i].position - checkPoints[i+1].position);
        }
        reverseTotalDistance = 1 / totalDistance;
    }
}

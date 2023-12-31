using UnityEngine;

public class RankCheckerZone : MonoBehaviour
{
    [SerializeField] public Transform checkPoint;
    [SerializeField] int rankWeight;

    void OnTriggerEnter(Collider other)
    {
        HorseController horse = other.GetComponent<HorseController>();
        if(horse) 
        {
            if(horse.rankWeight == rankWeight - 1)
            {
                horse.totalMoveDistance += horse.inZoneMoveDistance;
                horse.inZoneMoveDistance = 0f;
                horse.rankWeight = rankWeight;
                horse.rankZone = this;
            }
        }
    }
}

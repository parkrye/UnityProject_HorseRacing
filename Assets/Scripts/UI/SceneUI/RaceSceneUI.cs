using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceSceneUI : SceneUI
{
    public RaceController raceController;
    [SerializeField] List<RectTransform> distanceCheckers;

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(UpdateDistanceUIRoutine());
    }

    public void OnCameraButtonClicked()
    {
        raceController?.raceCameraController.OnChangeCameraButtonClicked();
    }

    public void OnRiderButtonClicked()
    {
        raceController?.raceCameraController.OnChangeTargetButtonClicked();
    }

    IEnumerator UpdateDistanceUIRoutine()
    {
        while (true)
        {
            for(int i = 0; i < distanceCheckers.Count; i++)
            {
                distanceCheckers[i].anchoredPosition = new Vector2(1100f * (raceController.Horses[i].totalMoveDistance + raceController.Horses[i].inZoneMoveDistance) * raceController.raceDistanceChecker.reverseTotalDistance, distanceCheckers[i].anchoredPosition.y);
            }
            yield return null;
        }
    }
}

using System.Collections;
using UnityEngine;

public class RaceScene : BaseScene
{
    [SerializeField] BattingController battingController;
    [SerializeField] RaceController raceController;

    protected override IEnumerator LoadingRoutine()
    {
        raceController.Initialize();
        battingController.Initialize();
        yield return null;
        Progress = 1f;
    }
}

using System.Collections;
using UnityEngine;

public class RaceScene : BaseScene
{
    [SerializeField] RaceController raceController;

    protected override IEnumerator LoadingRoutine()
    {
        raceController.Initialize();
        yield return null;
        Progress = 1f;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class RaceController : MonoBehaviour
{
    [SerializeField] BattingController battingController;

    [SerializeField] List<AI_Horse> horses;
    public List<AI_Horse> Horses { get { return horses; } }

    [SerializeField] public RaceSetting raceSetting;
    [SerializeField] public StartGate startGate;
    [SerializeField] public RankController rankController;
    [SerializeField] public RaceCameraController raceCameraController;
    [SerializeField] public RaceDistanceChecker raceDistanceChecker;
    [SerializeField] public GoalLine goalLine;

    [SerializeField] GameObject raceSceneUI;

    public void Initialize()
    {
        raceSetting.Initialize();
        startGate.Initialize();
        rankController.Initialize();
        raceDistanceChecker.Initialize();
        raceCameraController.Initialize();
        goalLine.Initialize();
        raceSceneUI.SetActive(false);
    }

    public void StartRace(int batEntry)
    {
        raceSceneUI.SetActive(true);
        raceCameraController.SetTarget(batEntry);
        startGate.startRace = true;
    }

    public void CheckBatting(int entryNum)
    {
        battingController.TakeDividend(entryNum);
    }
}

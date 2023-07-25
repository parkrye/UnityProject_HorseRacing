using System.Collections.Generic;
using UnityEngine;

public class RaceController : MonoBehaviour
{
    [SerializeField] List<AI_Horse> horses;
    public List<AI_Horse> Horses { get { return horses; } }

    [SerializeField] public RaceSetting raceSetting;
    [SerializeField] public StartGate startGate;
    [SerializeField] public RankController rankController;
    [SerializeField] public RaceCameraController raceCameraController;
    [SerializeField] public RaceDistanceChecker raceDistanceChecker;
    [SerializeField] public GoalLine goalLine;

    public void Initialize()
    {
        raceSetting.Initialize();
        startGate.Initialize();
        rankController.Initialize();
        raceDistanceChecker.Initialize();
        raceCameraController.Initialize();
        goalLine.Initialize();
    }

    public void StartRace()
    {
        startGate.startRace = true;
    }
}

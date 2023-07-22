using System.Collections.Generic;
using UnityEngine;

public class RaceController : MonoBehaviour
{
    [SerializeField] List<HorseController> horses;
    public List<HorseController> Horses { get { return horses; } }

    [SerializeField] StartGate startGate;
    [SerializeField] RankController rankController;
    [SerializeField] public RaceCameraController raceCameraController;
    [SerializeField] public RaceDistanceChecker raceDistanceChecker;

    [SerializeField] public int[] strategy;
    [SerializeField] public bool isLeftRound;

    public void Initialize()
    {
        strategy = new int[4];
        startGate.Initialize();
        rankController.Initialize();
        raceDistanceChecker.Initialize();
        raceCameraController.Initialize();
    }
}

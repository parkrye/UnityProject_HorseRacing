using System.Collections.Generic;
using UnityEngine;
using static AI_Horse;

public class RaceController : MonoBehaviour
{
    [SerializeField] List<HorseController> horses;
    public List<HorseController> Horses { get { return horses; } }

    [SerializeField] StartGate startGate;
    [SerializeField] RankController rankController;
    [SerializeField] RaceCameraController raceCameraController;

    [SerializeField] public int[] strategy;
    [SerializeField] public bool isLeftRound;

    void Awake()
    {
        strategy = new int[4];
        startGate.Initialize();
        rankController.Initialize();
        raceCameraController.Initialize();
    }
}

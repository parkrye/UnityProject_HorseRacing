using System.Collections.Generic;
using UnityEngine;

public class RaceController : MonoBehaviour
{
    [SerializeField] List<HorseController> horses;
    public List<HorseController> Horses { get { return horses; } }

    [SerializeField] StartGate startGate;
    [SerializeField] RankController rankController;

    void Awake()
    {
        startGate.Initialize();
        rankController.Initialize();
    }
}

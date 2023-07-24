using System.Collections.Generic;
using UnityEngine;

public class RaceSetting : MonoBehaviour
{
    [SerializeField] RaceController raceController;

    [SerializeField] public int[] strategy;
    [SerializeField] public bool isLeftRound;
    [SerializeField] Dictionary<string, List<string>> horseNames;

    public void Initialize()
    {
        horseNames = new Dictionary<string, List<string>>();
        horseNames = CSVRW.ReadCSV("HorseName");
        strategy = new int[4];
        for (int i = 0; i < raceController.Horses.Count; i++)
        {
            raceController.Horses[i].horse.Initialize(horseNames[Random.Range(1, 101).ToString()][0]);
            AI_Horse ai = raceController.Horses[i];
            if (ai)
            {
                ai.raceController = raceController;
                switch (ai.strategy)
                {
                    case AI_Horse.Strategy.Runway:
                        strategy[0]++;
                        break;
                    case AI_Horse.Strategy.Front:
                        strategy[1]++;
                        break;
                    case AI_Horse.Strategy.Stalker:
                        strategy[2]++;
                        break;
                    case AI_Horse.Strategy.Closer:
                        strategy[3]++;
                        break;
                }
            }
        }
        for (int i = 0; i < raceController.Horses.Count; i++)
            raceController.Horses[i].Initialize();
    }
}

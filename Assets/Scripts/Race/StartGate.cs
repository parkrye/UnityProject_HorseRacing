using System.Collections;
using UnityEngine;

public class StartGate : MonoBehaviour
{
    [SerializeField] RaceController raceController;
    [SerializeField] float delayTime;

    public void Initialize()
    {
        StartCoroutine(StartRace());
    }

    IEnumerator StartRace()
    {
        yield return null;
        for (int i = 0; i < raceController.Horses.Count; i++)
        {
            raceController.Horses[i].horse.Initialize();
            AI_Horse ai = (AI_Horse)raceController.Horses[i];
            if (ai)
            {
                ai.raceController = raceController;
                switch (ai.strategy)
                {
                    case AI_Horse.Strategy.Runway:
                        raceController.strategy[0]++;
                        break;
                    case AI_Horse.Strategy.Front:
                        raceController.strategy[1]++;
                        break;
                    case AI_Horse.Strategy.Stalker:
                        raceController.strategy[2]++;
                        break;
                    case AI_Horse.Strategy.Closer:
                        raceController.strategy[3]++;
                        break;
                }
            }
        }
        for (int i = 0; i < raceController.Horses.Count; i++)
            raceController.Horses[i].Initialize();
        yield return new WaitForSeconds(Random.Range(delayTime * 0.5f, delayTime * 2f));
        for (int i = 0; i < raceController.Horses.Count; i++)
            raceController.Horses[i].StartMove();
    }
}

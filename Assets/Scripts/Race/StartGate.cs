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
            raceController.Horses[i].horse.Initialize();
        for (int i = 0; i < raceController.Horses.Count; i++)
            raceController.Horses[i].Initialize();
        yield return new WaitForSeconds(Random.Range(delayTime * 0.5f, delayTime * 2f));
        for (int i = 0; i < raceController.Horses.Count; i++)
            raceController.Horses[i].StartMove();
    }
}

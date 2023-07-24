using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class StartGate : MonoBehaviour
{
    [SerializeField] RaceController raceController;
    [SerializeField] float delayTime;
    [SerializeField] public bool startRace;

    public void Initialize()
    {
        startRace = false;
        StartCoroutine(StartRace());
    }

    IEnumerator StartRace()
    {
        while(!startRace)
            yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(delayTime);
        for (int i = 0; i < raceController.Horses.Count; i++)
            raceController.Horses[i].StartMove();
    }
}

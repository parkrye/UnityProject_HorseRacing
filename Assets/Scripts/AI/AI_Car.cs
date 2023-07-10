using System.Collections;
using UnityEngine;

public abstract class AI_Car : MonoBehaviour
{
    protected bool uRoutineIsRunning, fuRoutineIsRunning;
    IEnumerator uRoutine, fuRoutine;

    public void Initialize()
    {
        uRoutine = URoutine();
        fuRoutine = FURoutine();
    }

    public void StartURoutine()
    {
        if (uRoutineIsRunning)
            return;
        uRoutineIsRunning = true;
        StartCoroutine(uRoutine);
    }

    public void StopURoutine()
    {
        if (!uRoutineIsRunning)
            return;
        StopCoroutine(uRoutine);
    }

    public void StartFURoutine()
    {
        if (fuRoutineIsRunning)
            return;
        fuRoutineIsRunning = true;
        StartCoroutine(fuRoutine);
    }

    public void StopFURoutine()
    {
        if (!fuRoutineIsRunning)
            return;
        StopCoroutine(fuRoutine);
    }

    public abstract IEnumerator URoutine();

    public abstract IEnumerator FURoutine();
}

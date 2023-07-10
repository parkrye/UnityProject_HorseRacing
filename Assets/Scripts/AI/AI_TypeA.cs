using System.Collections;
using UnityEngine;

public class AI_TypeA : AI_Car
{
    public override IEnumerator URoutine()
    {
        while (uRoutineIsRunning)
        {
            yield return new WaitForFixedUpdate();
        }
    }

    public override IEnumerator FURoutine()
    {
        while (fuRoutineIsRunning)
        {
            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_CarController : MonoBehaviour
{
    [SerializeField] AI_Car ai;

    public void Initialize()
    {
        ai.StartURoutine();
        ai.StartFURoutine();
    }
}

using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RaceCameraController : MonoBehaviour
{
    [SerializeField] List<CinemachineVirtualCamera> cameras;
    [SerializeField] Dictionary<CinemachineVirtualCamera, Volume> cvDict;

    public void Initialize()
    {
        cvDict = new Dictionary<CinemachineVirtualCamera, Volume>();
        for(int i = 0; i < cameras.Count; i++)
        {
            cvDict[cameras[i]] = cameras[i].GetComponentInChildren<Volume>();
        }
        SetCamera(0);
    }

    public void SetCamera(int index)
    {
        if (index < 0 || index >= cameras.Count)
            return;
        for(int i = 0; i < cameras.Count; i++)
        {
            cvDict[cameras[i]].enabled = (i == index);
        }
    }
}

using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RaceCameraController : MonoBehaviour
{
    [SerializeField] RaceController raceController;
    [SerializeField] List<CinemachineVirtualCamera> cameras;
    [SerializeField] Dictionary<CinemachineVirtualCamera, Volume> cvDict;

    [SerializeField] int cameraNum, targetNum;

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

        for (int i = 0; i < cameras.Count; i++)
        {
            cameras[i].Priority = (i == cameraNum) ? 1 : 0;
        }

        cameras[0].Follow = raceController.Horses[targetNum].transform;
        cameras[0].LookAt = raceController.Horses[targetNum].transform;
        cameras[1].Follow = raceController.Horses[targetNum].head;
        cameras[1].LookAt = raceController.Horses[targetNum].chest;
        cameras[2].Follow = raceController.Horses[targetNum].transform;
        cameras[2].LookAt = raceController.Horses[targetNum].transform;
    }

    public void OnChangeCameraButtonClicked()
    {
        cameraNum++;
        if (cameraNum == cameras.Count)
            cameraNum = 0;

        for (int i = 0; i < cameras.Count; i++)
        {
            cameras[i].Priority = (i == cameraNum) ? 1 : 0;
        }
    }

    public void OnChangeTargetButtonClicked()
    {
        targetNum++;
        if (targetNum == raceController.Horses.Count)
            targetNum = 0;

        cameras[0].Follow = raceController.Horses[targetNum].transform;
        cameras[0].LookAt = raceController.Horses[targetNum].transform;
        cameras[1].Follow = raceController.Horses[targetNum].head;
        cameras[1].LookAt = raceController.Horses[targetNum].chest;
        cameras[2].Follow = raceController.Horses[targetNum].transform;
        cameras[2].LookAt = raceController.Horses[targetNum].transform;
    }
}

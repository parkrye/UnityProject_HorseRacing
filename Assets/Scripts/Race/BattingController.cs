using Cinemachine;
using UnityEngine;

public class BattingController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera battingCam;
    [SerializeField] GameObject battingUI;

    public void Initialize()
    {

    }

    public void StopBatting()
    {
        battingCam.Priority = -1;
        battingUI.SetActive(false);
    }
}

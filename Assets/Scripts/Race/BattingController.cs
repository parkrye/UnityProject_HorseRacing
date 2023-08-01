using Cinemachine;
using TMPro;
using UnityEngine;

public class BattingController : MonoBehaviour
{
    [SerializeField] RaceController raceController;

    [SerializeField] CinemachineVirtualCamera battingCam;
    [SerializeField] GameObject battingUI;
    [SerializeField] GameObject resetUI;

    [SerializeField] TMP_Text[] batTexts;
    [SerializeField] float[] batPortion;

    [SerializeField] (int, int) batData;

    public void Initialize()
    {
        resetUI.SetActive(false);
        int total = 0;
        batPortion = new float[batTexts.Length];
        for (int i = 0; i < batTexts.Length; i++)
        {
            batPortion[i] = raceController.Horses[i].horse.Data.speed + 
                            raceController.Horses[i].horse.Data.stamina +
                            raceController.Horses[i].horse.Data.power +
                            raceController.Horses[i].horse.Data.intelligence;
            total += (int)batPortion[i];
        }

        for(int i = 0; i < batTexts.Length; i++)
        {
            batPortion[i] = 1f + (total / batPortion[i]) / batTexts.Length;
        }

        for (int i = 0; i < batTexts.Length; i++)
        {
            batTexts[i].text = $"{raceController.Horses[i].horse.Data.horseName}\n" +
                               $"Dividend : {batPortion[i]}";
        }
    }

    public void StopBatting()
    {
        battingCam.Priority = 0;
        battingUI.SetActive(false);
        raceController.StartRace(batData.Item1);
    }

    public void Batting(int batPoint, int batNum)
    {
        GameManager.Data.PlayerData.Money -= batPoint;
        GameManager.Data.PlayerData.GameCount++;
        batData = (batNum, batPoint);
        StopBatting();
    }

    public void TakeDividend(int entry)
    {
        if(entry.Equals(batData.Item1))
        {
            GameManager.Data.PlayerData.Money += (int)(batData.Item2 * batPortion[batData.Item1]);
            GameManager.Data.PlayerData.WinCount++;
            resetUI.SetActive(true);
        }
    }
}

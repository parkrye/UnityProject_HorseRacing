using UnityEngine;

public class BattingEntry : MonoBehaviour
{
    [SerializeField] BattingController battingController;
    [SerializeField] int batPoint;
    [SerializeField] int entryNum;

    public void OnBattingPointInputed(string _batPoint)
    {
        batPoint = int.Parse(_batPoint);
    }

    public void OnBattingButtonClicked()
    {
        if(batPoint <= GameManager.Data.PlayerData.Money)
            battingController.Batting(batPoint, entryNum);
    }
}

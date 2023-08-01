using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    public PlayerData PlayerData { get { return playerData; } set { playerData = value; } }

    public void Initialize()
    {
        playerData = GameManager.Resource.Load<PlayerData>("Player/PlayerData");
        if (playerData.Money == 0)
            playerData.Money = 10;
    }
}

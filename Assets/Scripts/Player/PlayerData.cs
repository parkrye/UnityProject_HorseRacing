using UnityEngine;

[CreateAssetMenu (fileName = "PlayerData", menuName = "Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField] int money;
    [SerializeField] int gameCount;
    [SerializeField] int winCount;

    public int Money { get {  return money; } set {  money = value; } }
    public int GameCount { get {  return gameCount; } set {  gameCount = value; } }
    public int WinCount { get { return winCount; } set {  winCount = value; } }
}

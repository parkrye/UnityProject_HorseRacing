[System.Serializable]
public class HorseData : JsonData
{
    public string horseName;
    public float speed, stamina, power, intelligence;

    public HorseData(string horseName, float speed, float stamina, float power, float intelligence)
    {
        this.horseName = horseName;
        this.speed = speed;
        this.stamina = stamina;
        this.power = power;
        this.intelligence = intelligence;
    }
}

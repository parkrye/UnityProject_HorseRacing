using UnityEngine;

public class Horse : MonoBehaviour
{
    [SerializeField] HorseData horseData;
    public HorseData Data { get { return horseData; } }
    [SerializeField] Material[] materials;
    [SerializeField] SkinnedMeshRenderer[] body, fur, accesory;

    [ContextMenu("Initialize")]
    public void Initialize()
    {
        // 단거리 표준
        //horseData.speed = 0;
        //horseData.stamina = 0;
        //horseData.power = 0;
        //horseData.intelligence = 0;

        // 마일 표준
        //horseData.speed = 30;
        //horseData.stamina = 50;
        //horseData.power = 20;
        //horseData.intelligence = 20;

        // 중거리 표준
        //horseData.speed = 0;
        //horseData.stamina = 0;
        //horseData.power = 0;
        //horseData.intelligence = 0;

        // 장거리 표준
        //horseData.speed = 0;
        //horseData.stamina = 0;
        //horseData.power = 0;
        //horseData.intelligence = 0;

        horseData.speed += Random.Range(-horseData.speed * 0.5f, horseData.speed * 0.5f);
        horseData.stamina += Random.Range(-horseData.stamina * 0.5f, horseData.stamina * 0.5f);
        horseData.power += Random.Range(-horseData.power * 0.5f, horseData.power * 0.5f);
        horseData.intelligence += Random.Range(-horseData.intelligence * 0.5f, horseData.intelligence * 0.5f);

        int material = Random.Range(0, materials.Length);
        for(int i = 0; i < body.Length; i++)
            body[i].material = materials[material];

        material = Random.Range(0, materials.Length);
        for(int i = 0; i < fur.Length; i++)
            fur[i].material = materials[material];

        for(int i = 0; i < accesory.Length; i++)
            accesory[i].material = materials[Random.Range(0, materials.Length)];
    }
}

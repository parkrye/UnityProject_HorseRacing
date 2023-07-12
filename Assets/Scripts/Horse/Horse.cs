using System.Collections;
using System.Collections.Generic;
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
        horseData.speed = Random.Range(20000, 30000);
        horseData.stamina = Random.Range(1000, 3000);
        horseData.power = Random.Range(3000, 5000);

        int material = Random.Range(0, materials.Length);
        for(int i = 0; i < body.Length; i++)
        {
            body[i].material = materials[material];
        }

        material = Random.Range(0, materials.Length);
        for(int i = 0; i < fur.Length; i++)
        {
            fur[i].material = materials[material];
        }

        for(int i = 0; i < accesory.Length; i++)
        {
            accesory[i].material = materials[Random.Range(0, materials.Length)];
        }
    }
}

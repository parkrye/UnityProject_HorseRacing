using System.Collections;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    void Awake()
    {
        StartCoroutine(LoadMainSceneRoutine());
    }

    IEnumerator LoadMainSceneRoutine()
    {
        yield return null;
        GameManager.Scene.LoadScene("MainScene");
    }
}

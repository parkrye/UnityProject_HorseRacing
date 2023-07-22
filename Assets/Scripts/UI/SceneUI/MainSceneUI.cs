using UnityEngine;

public class MainSceneUI : SceneUI
{
    public void OnStartButtonClicked()
    {
        GameManager.Scene.LoadRacneScene("MilersL");
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}

using UnityEngine;

public class MainSceneUI : SceneUI
{
    public void OnStartButtonClicked()
    {
        GameManager.Scene.LoadRacneScene("StayersL");
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}

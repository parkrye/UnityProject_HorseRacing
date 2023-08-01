using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetUI : BaseUI
{
    protected override void Awake()
    {
        base.Awake();
        buttons["ResetButton"].onClick.AddListener(() => { OnResetButtonClicked(); });
    }

    void OnResetButtonClicked()
    {
        GameManager.Scene.LoadScene("MainScene");
    }
}

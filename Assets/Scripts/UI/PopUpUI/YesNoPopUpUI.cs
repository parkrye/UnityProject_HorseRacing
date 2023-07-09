using UnityEngine.Events;

/// <summary>
/// 범용 예/아니오 팝업창
/// </summary>
public class YesNoPopUpUI : PopUpUI
{
    public UnityEvent YesEvent, NoEvent;

    protected override void Awake()
    {
        base.Awake();

        buttons["YesButton"].onClick.AddListener(YesButton);
        buttons["NoButton"].onClick.AddListener(NoButton);
    }

    /// <summary>
    /// 텍스트 문구를 수정
    /// </summary>
    /// <param name="textNum">0: 설명, 1: yes버튼, 2: no버튼</param>
    public void SetText(int textNum, string text)
    {
        switch(textNum)
        {
            case 0:
                texts["DescText"].text = text;
                break;
            case 1:
                texts["YesText"].text = text;
                break;
            case 2:
                texts["NoText"].text = text;
                break;
        }
    }

    /// <summary>
    /// YesEvent를 발동시키고 삭제
    /// </summary>
    public void YesButton()
    {
        YesEvent?.Invoke();
        CloseUI();
    }

    /// <summary>
    /// NoEvent를 발동시키고 삭제
    /// </summary>
    public void NoButton()
    {
        NoEvent?.Invoke();
        CloseUI();
    }
}

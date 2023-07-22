using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    Slider slider;
    Animator anim;

    void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        anim = GetComponent<Animator>();
    }

    public void SetProgress(float progress)
    {
        slider.value = progress;
    }

    public void FadeIn()
    {
        anim.SetTrigger("FadeIn");
        gameObject.SetActive(false);
    }

    public void FadeOut()
    {
        anim.SetTrigger("FadeOut");
        gameObject.SetActive(false);
    }
}

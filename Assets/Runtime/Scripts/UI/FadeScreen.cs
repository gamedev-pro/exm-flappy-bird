using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    [SerializeField] private Image fadeImage;

    public IEnumerator Flash()
    {
        yield return StartCoroutine(FadeIn(0.05f, Color.white));
        yield return StartCoroutine(FadeOut(0.05f, Color.white));
    }

    public IEnumerator FadeIn(float fadeTime, Color fadeColor)
    {
        fadeImage.enabled = true;
        fadeColor.a = 0;
        fadeImage.color = fadeColor;
        Tween fadeTween = fadeImage.DOFade(1, fadeTime);
        yield return fadeTween.WaitForCompletion();
    }

    public IEnumerator FadeOut(float fadeTime, Color fadeColor)
    {
        fadeImage.enabled = true;
        fadeColor.a = 1;
        fadeImage.color = fadeColor;
        Tween fadeTween = fadeImage.DOFade(0, fadeTime);
        yield return fadeTween.WaitForCompletion();
        fadeImage.enabled = false;
    }
}

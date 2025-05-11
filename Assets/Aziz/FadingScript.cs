using UnityEngine;
using System.Collections;

public class FadingScript : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeInDuration = 5.0f;
    public float fadeOutDuration = 1f;
    public bool fadeIn = false;
    private void Start()
    {
        if (fadeIn)
        {
            FadeIn();
        }
        else
        {
            FadeOut();
        }
    }
    public void FadeIn()
    {
        StartCoroutine(FadeCanvasGroup(canvasGroup,canvasGroup.alpha,0,fadeInDuration));
    }
    public void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1, fadeOutDuration));
    }
    IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeInDuration || elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsedTime / duration);
            yield return null;
        }
        cg.alpha = end;
    }
}

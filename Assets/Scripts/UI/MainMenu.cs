using System.Collections;
using UnityEngine;
[RequireComponent(typeof(CanvasGroup))]
public class MainMenu : MonoBehaviour
{
    private const float Step = 0.02f;

    public bool IsShowed { get; private set; } = true;

    private CanvasGroup _canvas;
    private Coroutine _coroutine;

    private void Awake()
    {
        _canvas = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        _canvas.blocksRaycasts = true;
        IsShowed = true;
        StopCoroutineIfStarted();
        _coroutine = StartCoroutine(FadeInOut(true));
    }

    public void Hide()
    {
        _canvas.blocksRaycasts = false;
        IsShowed = false;
        StopCoroutineIfStarted();
        _coroutine = StartCoroutine(FadeInOut(false));
    }

    private IEnumerator FadeInOut(bool direction)
    {
        if (direction)
        {
            do
            {
                _canvas.alpha += Step;
                yield return new WaitForEndOfFrame();
            } while (_canvas.alpha < 1f);
        }
        else
        {
            do
            {
                _canvas.alpha -= Step;
                yield return new WaitForEndOfFrame();
            } while (_canvas.alpha > 0f);
        }

        _coroutine = null;
    }

    private void StopCoroutineIfStarted()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public CanvasGroup activeCanvas;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void ChangeCanvas(CanvasGroup newCanvas)
    {
        StartCoroutine(SwitchCanvas(newCanvas));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
    public IEnumerator FadeCanvas(CanvasGroup canvas, float duration, float targetAlpha)
    {
        float currentTime = 0;
        float startAlpha = canvas.alpha;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            canvas.alpha = Mathf.Lerp(startAlpha, targetAlpha, currentTime / duration);
            yield return null;
        }
        canvas.alpha = targetAlpha;
    }
    
    public IEnumerator SwitchCanvas(CanvasGroup newCanvas)
    {
        yield return FadeCanvas(activeCanvas, 1, 0);
        activeCanvas.gameObject.SetActive(false);
        newCanvas.gameObject.SetActive(true);
        yield return FadeCanvas(newCanvas, 1, 1);
        activeCanvas = newCanvas;    
    }
}

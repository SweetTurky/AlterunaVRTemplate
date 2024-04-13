using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public Image canvasImage;
    public float fadeDuration = 1.0f;
    
    void Start()
    {
        StartCoroutine(StartScene());
    }

    IEnumerator StartScene()
    {
        FadeToBlack();
        yield return new WaitForSeconds(20); // Venter 5 sekunder inden fade
        yield return new WaitForSeconds(fadeDuration);
        // Skift scene her
        SceneManager.LoadScene("PeterXRInteraction");
        
        yield return null;
    }

    void FadeToBlack()
    {
        StartCoroutine(FadeImage(canvasImage, fadeDuration, Color.black));
    }

    IEnumerator FadeImage(Image image, float fadeTime, Color endColor)
    {
        Color startColor = image.color;
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            image.color = Color.Lerp(startColor, endColor, elapsedTime / fadeTime);
            yield return null;
        }

        image.color = endColor;
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("PeterXRInteraction");
    }
    
}


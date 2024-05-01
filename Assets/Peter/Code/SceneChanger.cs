using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public Image canvasImage;
    public float fadeDuration = 5.0f; // Fade to black duration

    private bool isFading = false;

    void Start()
    {
        // No need to start fading here
    }

    void Update()
    {
        // Check for the "E" key press to start scene transition
        if (Input.GetKeyDown(KeyCode.E) && !isFading)
        {
            StartCoroutine(StartScene());
        }
    }

    IEnumerator StartScene()
    {
        // Fade out before changing scene
        FadeToBlack();
        yield return new WaitForSeconds(fadeDuration);

        // Change scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("PeterXRInteraction");

        // Wait until the new scene is fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    void FadeToBlack()
    {
        StartCoroutine(FadeImage(canvasImage, fadeDuration, Color.black));
    }

    IEnumerator FadeImage(Image image, float fadeTime, Color endColor)
    {
        isFading = true; // Set the fading flag

        Color startColor = image.color;
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            image.color = Color.Lerp(startColor, endColor, elapsedTime / fadeTime);
            yield return null;
        }

        image.color = endColor;
        isFading = false; // Reset the fading flag
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log("OnEnable Called");
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Debug.Log("OnDisable Called");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Ensure the canvas image is fully visible (alpha = 1) when the new scene is loaded
        canvasImage.color = new Color(canvasImage.color.r, canvasImage.color.g, canvasImage.color.b, 1f);

        // Start fading from black after scene is loaded
        StartCoroutine(FadeFromBlack());
    }

    IEnumerator FadeFromBlack()
    {
        // Wait a short duration before starting the fade from black
        yield return new WaitForSeconds(2.5f);

        // Start the fade from black with the new duration
        StartCoroutine(FadeImage(canvasImage, 10.0f, Color.clear)); // Fade from black duration set to 10 seconds
    }
}

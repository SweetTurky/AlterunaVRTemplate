using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTeleporter : MonoBehaviour
{
    public Image canvasImage;
    public float fadeDuration = 2.0f; // Fade duration
    public Transform[] anchorPoints;
    public Transform TeleportDestination;
    public Transform TeleportDestination2;
    public Collider TeleportWall;
    public Collider TeleportWall2;

    // Start is called before the first frame update
    void Start()
    {
        // No need to start fading here
    }

    void Update()
    {
        // Check for the "E" key press to start fading
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(FadeToAndFromBlack());   
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == TeleportWall)
        {
            TeleportTo(TeleportDestination);
        }
        if (collision.collider == TeleportWall2)
        {
            TeleportTo(TeleportDestination2);
        }
    }

    IEnumerator FadeToAndFromBlack()
    {
        yield return FadeImage(canvasImage, fadeDuration, Color.black); // Fade to black
        yield return new WaitForSeconds(1f); // Wait for a short duration
        TeleportToRandomAnchorPoint();
        yield return new WaitForSeconds(1f); // Wait for a short duration
        yield return FadeImage(canvasImage, fadeDuration, Color.clear); // Fade from black
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

        image.color = endColor; // Ensure final color matches endColor
    }

    // Metode til at teleportere NPC'en til en given destination
    public void TeleportTo(Transform destination)
    {
        // Teleportér NPC'en til den givne destination
        transform.position = destination.position;
        transform.rotation = destination.rotation;
        Debug.Log("Har teleporteret");
    }

    // Eksempel på, hvordan du kan kalde teleportationen
    void TeleportToRandomAnchorPoint()
    {
        // Vælg tilfældigt et anchor point
        int randomIndex = Random.Range(0, anchorPoints.Length);
        Transform randomAnchorPoint = anchorPoints[randomIndex];

        // Teleportér NPC'en til det tilfældigt valgte anchor point
        TeleportTo(randomAnchorPoint);
    }
    void PlayerTeleportOne()
    {
        TeleportTo(anchorPoints[0]);
    }

    void PlayerTeleportTwo()
    {
        TeleportTo(anchorPoints[1]);
    }

    void PlayerTeleportThree()
    {
        TeleportTo(anchorPoints[2]);
    }

    void PlayerTeleportFour()
    {
        TeleportTo(anchorPoints[3]);
    }
}

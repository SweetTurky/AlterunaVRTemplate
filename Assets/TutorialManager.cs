using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public Animator videoScreen; // Reference to the Animator component

    // Start is called before the first frame update
    void Start()
    {
        // Get the Animator component attached to the videoScreen GameObject
        Animator anim = videoScreen.GetComponent<Animator>();

        // Check if the Animator component is found
        if (anim == null)
        {
            Debug.LogError("Animator component not found on the videoScreen GameObject!");
            return;
        }

        // Play the animation
        anim.Play("EaseIn");
    }
}

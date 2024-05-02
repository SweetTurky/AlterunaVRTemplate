using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public Animator videoScreenAnim; // Reference to the Animator component
    public VideoPlayer videoPlayer;
    public float videoTime;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Animator component attached to the videoScreen GameObject
        Animator anim = videoScreenAnim.GetComponent<Animator>();

        // Check if the Animator component is found
        if (anim == null)
        {
            Debug.LogError("Animator component not found on the videoScreen GameObject!");
            return;
        }

        // Play the animation
        anim.Play("EaseIn");

    }

      // Function to start the video clip (is called from animation event)
    public void StartVideo()
    {
        videoTime = (float)videoPlayer.length;
        Invoke("VideoEnded", videoTime);
        // Play the video clip here
        videoPlayer.Play();
        Debug.Log("Video started!");
        
        // After the video has finished playing, play the EaseOut animation
        // You might want to implement logic to detect when the video finishes and call PlayEaseOutAnimation() accordingly
    }

    // Function to play the EaseOut animation
    private void VideoEnded()
    {
        // Get the Animator component attached to the videoScreen GameObject
        Animator anim = videoScreenAnim.GetComponent<Animator>();

        // Check if the Animator component is found
        if (anim == null)
        {
            Debug.LogError("Animator component not found on the videoScreen GameObject!");
            return;
        }

        // Play the EaseOut animation
        anim.Play("EaseOut");
    }

    
}

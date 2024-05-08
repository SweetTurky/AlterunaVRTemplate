using UnityEngine;
using System.Collections;

public class HearingLossSimulation : MonoBehaviour
{
    // Adjust these variables as needed to control the rate of hearing loss
    public float hearingLossRate = 0.01f; // Rate at which hearing loss occurs per second
    public float stereoPanningSkew = 0.5f; // Skew of stereo panning towards one ear
    public float minCutoffFrequency = 500f; // Minimum cutoff frequency for low pass filter
    public float minDryMix = 0.1f; // Minimum dry mix for chorus filter

    private AudioListener audioListener;
    private AudioLowPassFilter lowPassFilter;
    private AudioChorusFilter chorusFilter;

    private float timeSinceLastQChange = 0f;
    private float timeBetweenQChanges = 2f; // Change Q every 2 seconds

    private void Start()
    {
        audioListener = FindObjectOfType<AudioListener>(); // Find the AudioListener in the scene
        lowPassFilter = audioListener.GetComponent<AudioLowPassFilter>();
        chorusFilter = audioListener.GetComponent<AudioChorusFilter>();
    }

    private void Update()
    {
        if(audioListener == null || lowPassFilter == null || chorusFilter == null)
        {
            Debug.LogError("One or more necessary components not found.");
            return;
        }

         if (lowPassFilter.cutoffFrequency <= minCutoffFrequency)
        {
            // Update time since last Q change
            timeSinceLastQChange += Time.deltaTime;

            // Check if it's time to change Q
            if (timeSinceLastQChange >= timeBetweenQChanges)
            {
                // Change the resonance Q to a random value between 0 and 3
                //lowPassFilter.lowpassResonanceQ = Random.Range(0f, 3f);
                // Start a new Q change
                StartCoroutine(ChangeResonanceQSmoothly(Random.Range(0f, 3f)));

                // Reset time since last Q change
                timeSinceLastQChange = 0f;
            }
        }

        // Gradually reduce the sensitivity of the AudioListener to high frequencies
        lowPassFilter.cutoffFrequency -= hearingLossRate * Time.deltaTime;
        lowPassFilter.cutoffFrequency = Mathf.Max(lowPassFilter.cutoffFrequency, minCutoffFrequency);


        // Gradually increase the stereo panning skew towards one ear
        chorusFilter.dryMix -= stereoPanningSkew * Time.deltaTime;
        chorusFilter.dryMix = Mathf.Max(chorusFilter.dryMix, minDryMix);
    }
        
        private IEnumerator ChangeResonanceQSmoothly(float targetQ)
    {
        float startQ = lowPassFilter.lowpassResonanceQ;
        float elapsedTime = 0f;

        while (elapsedTime < 2f) // Duration of 2 seconds
        {
            // Calculate the interpolation factor (0 to 1) based on elapsed time
            float t = elapsedTime / 2f;

            // Smoothly interpolate between start and target Q values
            lowPassFilter.lowpassResonanceQ = Mathf.Lerp(startQ, targetQ, t);

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure that the target Q value is set precisely when the interpolation finishes
        lowPassFilter.lowpassResonanceQ = targetQ;
    }
}

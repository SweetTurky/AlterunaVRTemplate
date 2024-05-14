using UnityEngine;
using System.Collections;

public class HearingLossSimulation : MonoBehaviour
{
    public static HearingLossSimulation Instance;
    // Adjust these variables as needed to control the rate of hearing loss
    public float hearingLossRate = 0.01f; // Rate at which hearing loss occurs per second
    public float stereoPanningSkew = 0.5f; // Skew of stereo panning towards one ear
    public float minCutoffFrequency = 500f; // Minimum cutoff frequency for low pass filter
    public float minDryMix = 0.1f; // Minimum dry mix for chorus filter
    public AudioSource tinnitusAudio;
    public bool hearinglossActivated = false;
    private bool tinnitusHasPlayed = false;
    private AudioListener audioListener;
    public AudioLowPassFilter lowPassFilter;
    public AudioChorusFilter chorusFilter;
    public float hearingLossIncreaseRate = 150f;
    public float stereoPanningPositiveSkew = 0f;
    public float minimumRange = 0f;
    public float maximumRange = 3f;

    private float timeSinceLastQChange = 0f;
    private float timeBetweenQChanges = 2f; // Change Q every 2 seconds


    private void Awake()
    {
        // Ensure only one instance of the class exists
        if (Instance == null)
        {
            // Set the instance to this object
            Instance = this;
        }
        else
        {
            // If an instance already exists, destroy this duplicate
            Debug.LogWarning("Duplicate instance of HearingLossSimulation found. Destroying this instance.");
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        audioListener = FindObjectOfType<AudioListener>(); // Find the AudioListener in the scene
        lowPassFilter = audioListener.GetComponent<AudioLowPassFilter>();
        chorusFilter = audioListener.GetComponent<AudioChorusFilter>();
    }

    private void Update()
    {
        if (audioListener == null || lowPassFilter == null || chorusFilter == null)
        {
            Debug.LogError("One or more necessary components not found.");
            return;
        }
        if (hearinglossActivated == true)
        {
            if (lowPassFilter.cutoffFrequency <= minCutoffFrequency) //When it hits 500, this fires
            {
                // Update time since last Q change
                timeSinceLastQChange += Time.deltaTime;

                // Check if it's time to change Q
                if (timeSinceLastQChange >= timeBetweenQChanges)
                {
                    // Change the resonance Q to a random value between 0 and 3
                    //lowPassFilter.lowpassResonanceQ = Random.Range(0f, 3f);
                    // Start a new Q change
                    StartCoroutine(ChangeResonanceQSmoothly(Random.Range(minimumRange, maximumRange)));

                    // Reset time since last Q change
                    timeSinceLastQChange = 0f;
                }

                if (!tinnitusHasPlayed)
                {
                    tinnitusAudio.Play();
                    /*while (tinnitusAudio.isPlaying)
                    {

                    }*/
                    tinnitusHasPlayed = true;
                }
            }

            // Gradually reduce the sensitivity of the AudioListener to high frequencies
            lowPassFilter.cutoffFrequency -= hearingLossRate * Time.deltaTime;
            lowPassFilter.cutoffFrequency = Mathf.Max(lowPassFilter.cutoffFrequency, minCutoffFrequency);


            // Gradually increase the stereo panning skew towards one ear
            chorusFilter.dryMix -= stereoPanningSkew * Time.deltaTime;
            chorusFilter.dryMix = Mathf.Max(chorusFilter.dryMix, minDryMix);
        }

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

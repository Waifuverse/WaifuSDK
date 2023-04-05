using UnityEngine;
using System.Collections.Generic;

public class BlinkController : MonoBehaviour
{
    public SkinnedMeshRenderer skinMeshRenderer;
    public List<int> blinkBlendShapeIndices; // The indices of the blendshapes controlling the blink
   
    
    private float minBlinkValue = 0f; // The minimum blendshape value for a blink
    private float maxBlinkValue = 100f; // The maximum blendshape value for a blink
    private float blinkSpeed = 1250f; // The speed at which the blendshape value changes during a blink
    private float minTimeBetweenBlinks = 1f; // The minimum time between blinks
    private float maxTimeBetweenBlinks = 11f; // The maximum time between blinks
    private float eyelidUpValue = 0f; // The blendshape value for the eyelids up position

    private enum BlinkState { None, Blinking, EyelidsUp }
    private BlinkState currentState = BlinkState.None; // The current blink state
    private float currentBlinkValue = 0f; // The current blendshape value
    private float timeSinceLastBlink = 0f; // The time since the last blink
    private float nextBlinkTime = 0f; // The time at which the next blink will occur

    public AudioSource audioSource;
    void Start()
    {
        // Set the time for the first blink
        nextBlinkTime = Time.time + Random.Range(minTimeBetweenBlinks, maxTimeBetweenBlinks);
        if (skinMeshRenderer == null)
        {
            // Find the SkinnedMeshRenderer component in the children of this GameObject
            skinMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        }
        audioSource = GetComponent<AudioSource>();
    }
    //on validate get the skin mesh renderer form child
    private void OnValidate()
    {
        if (skinMeshRenderer == null)
        {
            // Find the SkinnedMeshRenderer component in the children of this GameObject
            skinMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        }

        if (skinMeshRenderer != null)
        {
            //if the list is empty
            if (blinkBlendShapeIndices == null || blinkBlendShapeIndices.Count == 0)
            {

                blinkBlendShapeIndices = new List<int>();
                for (int i = 0; i < skinMeshRenderer.sharedMesh.blendShapeCount; i++)
                {
                    if (skinMeshRenderer.sharedMesh.GetBlendShapeName(i).Equals("Blink", System.StringComparison.OrdinalIgnoreCase))
                    {
                        blinkBlendShapeIndices.Add(i);
                    }
                }
            }
            
        }
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Play the audio clip
            
            audioSource.Play();
        }
        switch (currentState)
        {
            case BlinkState.None:
                // Check if it's time to start a blink
                if (Time.time >= nextBlinkTime)
                {
                    // Start a blink
                    currentState = BlinkState.Blinking;
                    currentBlinkValue = minBlinkValue;
                }
                break;

            case BlinkState.Blinking:
                // Change the blendshape value
                currentBlinkValue += blinkSpeed * Time.deltaTime;

                if (currentBlinkValue >= maxBlinkValue)
                {
                    // End the blink
                    currentState = BlinkState.EyelidsUp;
                    currentBlinkValue = maxBlinkValue;
                }
                break;

            case BlinkState.EyelidsUp:
                // Change the blendshape value
                currentBlinkValue -= blinkSpeed * Time.deltaTime;

                if (currentBlinkValue <= eyelidUpValue)
                {
                    // End the eyelids up animation
                    currentState = BlinkState.None;
                    currentBlinkValue = eyelidUpValue;

                    // Set the time for the next blink
                    nextBlinkTime = Time.time + Random.Range(minTimeBetweenBlinks, maxTimeBetweenBlinks);
                }
                break;
            // on space bar play audio
               
        }

        // Set the blendshape values
        foreach (int blendShapeIndex in blinkBlendShapeIndices)
        {
            skinMeshRenderer.SetBlendShapeWeight(blendShapeIndex, currentBlinkValue);
        }
    }
}

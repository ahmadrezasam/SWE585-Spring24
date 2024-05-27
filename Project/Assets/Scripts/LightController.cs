using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    // Reference to the spotlight
    public Light spotlight;

    // Spotlight parameters
    public float intensity = 1.0f;
    public float intensityChangeRate = 1.0f; // Rate at which the intensity changes
    public Color color = Color.white;
    public float range = 10.0f;
    public float rangeChangeRate = 1.0f; // Rate at which the range changes
    public float spotAngle = 30.0f; // Initial spotlight angle
    public float angleChangeRate = 1.0f; // Rate at which the spot angle changes

    public KeyCode toggleKey = KeyCode.T; // Key to toggle the spotlight on and off
    public KeyCode increaseIntensityKey = KeyCode.UpArrow; // Key to increase intensity
    public KeyCode decreaseIntensityKey = KeyCode.DownArrow; // Key to decrease intensity
    public KeyCode increaseRangeKey = KeyCode.RightArrow; // Key to increase range
    public KeyCode decreaseRangeKey = KeyCode.LeftArrow; // Key to decrease range
    public KeyCode increaseAngleKey = KeyCode.PageUp; // Key to increase spot angle
    public KeyCode decreaseAngleKey = KeyCode.PageDown; // Key to decrease spot angle

    // Initial state
    private bool isSpotlightOn;

    void Start()
    {
        if (spotlight == null)
        {
            Debug.LogError("Spotlight not assigned.");
            return;
        }

        // Set initial spotlight parameters
        spotlight.intensity = intensity;
        spotlight.color = color;
        spotlight.range = range;
        spotlight.spotAngle = spotAngle;
        isSpotlightOn = spotlight.enabled;
    }

    void Update()
    {
        // Check for the toggle key press
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleSpotlight();
        }

        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
        {
            // Change intensity
            if (Input.GetKey(increaseIntensityKey))
            {
                ChangeIntensity(intensityChangeRate * Time.deltaTime);
            }

            if (Input.GetKey(decreaseIntensityKey))
            {
                ChangeIntensity(-intensityChangeRate * Time.deltaTime);
            }

            // Change range
            if (Input.GetKey(increaseRangeKey))
            {
                ChangeRange(rangeChangeRate * Time.deltaTime);
            }

            if (Input.GetKey(decreaseRangeKey))
            {
                ChangeRange(-rangeChangeRate * Time.deltaTime);
            }

            // Change spot angle
            if (Input.GetKey(increaseAngleKey))
            {
                ChangeSpotAngle(angleChangeRate * Time.deltaTime);
            }

            if (Input.GetKey(decreaseAngleKey))
            {
                ChangeSpotAngle(-angleChangeRate * Time.deltaTime);
            }
        }

        if (isSpotlightOn)
        {
            spotlight.intensity = intensity;
            spotlight.color = color;
            spotlight.range = range;
            spotlight.spotAngle = spotAngle;
        }
    }

    void ToggleSpotlight()
    {
        isSpotlightOn = !isSpotlightOn;
        spotlight.enabled = isSpotlightOn;
    }

    void ChangeIntensity(float change)
    {
        intensity = Mathf.Clamp(intensity + change, 0, 8);
    }

    void ChangeRange(float change)
    {
        range = Mathf.Clamp(range + change, 0, 100);
    }

    void ChangeSpotAngle(float change)
    {
        spotAngle = Mathf.Clamp(spotAngle + change, 1, 179);
    }
}

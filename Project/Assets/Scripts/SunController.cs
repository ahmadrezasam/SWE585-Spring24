using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
    public Light directionalLight;

    // Update is called once per frame
    void Update()
    {
        // Check for input to toggle the light
        if (Input.GetKeyDown(KeyCode.L))
        {
            ToggleLight();
        }
    }

    // Method to toggle the light on and off
    void ToggleLight()
    {
        if (directionalLight != null)
        {
            directionalLight.enabled = !directionalLight.enabled;
        }
    }
}

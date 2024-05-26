using UnityEngine;

public class WindZoneController : MonoBehaviour
{
    private WindZone[] windZones;
    public KeyCode toggleKey = KeyCode.P;
    public KeyCode increaseStrengthKey = KeyCode.UpArrow;
    public KeyCode decreaseStrengthKey = KeyCode.DownArrow;
    public KeyCode increaseTurbulenceKey = KeyCode.RightArrow;
    public KeyCode decreaseTurbulenceKey = KeyCode.LeftArrow;

    public float strengthChangeAmount = 1.0f;
    public float turbulenceChangeAmount = 1.0f;

    private void Start()
    {
        windZones = FindObjectsOfType<WindZone>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleWindZones();
        }

        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            if (Input.GetKeyDown(increaseStrengthKey))
            {
                IncreaseWindStrength();
            }

            if (Input.GetKeyDown(decreaseStrengthKey))
            {
                DecreaseWindStrength();
            }

            if (Input.GetKeyDown(increaseTurbulenceKey))
            {
                IncreaseWindTurbulence();
            }

            if (Input.GetKeyDown(decreaseTurbulenceKey))
            {
                DecreaseWindTurbulence();
            }
        }
}

    private void ToggleWindZones()
    {
        foreach (var windZone in windZones)
        {
            windZone.gameObject.SetActive(!windZone.gameObject.activeSelf);
        }
    }

    private void IncreaseWindStrength()
    {
        foreach (var windZone in windZones)
        {
            windZone.windMain += strengthChangeAmount;
        }
    }

    private void DecreaseWindStrength()
    {
        foreach (var windZone in windZones)
        {
            windZone.windMain -= strengthChangeAmount;
        }
    }

    private void IncreaseWindTurbulence()
    {
        foreach (var windZone in windZones)
        {
            windZone.windTurbulence += turbulenceChangeAmount;
        }
    }

    private void DecreaseWindTurbulence()
    {
        foreach (var windZone in windZones)
        {
            windZone.windTurbulence -= turbulenceChangeAmount;
        }
    }
}

using UnityEngine;

public class FlashRainbow : MonoBehaviour
{
    public Material baseMaterial;
    public float flashSpeed = 1f;       // Pulses per second
    public float hueShiftSpeed = 0.1f;  // Color cycle speed

    private Renderer rend;
    private Material instanceMaterial;
    private float hueOffset;
    private float phaseOffset;

    void Start()
    {
        rend = GetComponent<Renderer>();

        // Clone material so each cube flashes independently
        instanceMaterial = new Material(baseMaterial);
        rend.material = instanceMaterial;

        hueOffset = Random.value; // Random starting hue for each cube
        phaseOffset = Random.Range(0f, Mathf.PI * 2f);

        instanceMaterial.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        float time = Time.time;

        // Compute hue from time
        float hue = Mathf.Repeat(hueOffset + time * hueShiftSpeed, 1f);
        Color baseColor = Color.HSVToRGB(hue, 1f, 1f);

        // Pulse intensity
        float intensity = (Mathf.Sin(time * flashSpeed * Mathf.PI * 2f + phaseOffset) + 1f) / 2f;

        // Apply color * intensity
        Color emission = baseColor * intensity * 2f;

        instanceMaterial.SetColor("_EmissionColor", emission);
    }
}

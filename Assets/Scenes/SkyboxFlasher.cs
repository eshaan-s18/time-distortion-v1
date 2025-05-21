using UnityEngine;

public class SkyboxFlasher : MonoBehaviour
{
    public Material skyboxMaterial;
    public float hueShiftSpeed = 0.1f;
    public float flashSpeed = 2f;

    private float phaseOffset;

    void Start()
    {
        if (skyboxMaterial == null)
            skyboxMaterial = RenderSettings.skybox;

        phaseOffset = Random.Range(0f, Mathf.PI * 2f);
    }

    void Update()
    {
        float time = Time.time;

        // Cycle hue over time
        float hue = Mathf.Repeat(time * hueShiftSpeed, 1f);
        Color baseColor = Color.HSVToRGB(hue, 1f, 1f);

        // Add pulsing intensity (optional)
        float intensity = (Mathf.Sin(time * flashSpeed * Mathf.PI * 2f + phaseOffset) + 1f) / 2f;
        Color animatedColor = baseColor * intensity * 2f;

        // Apply to procedural skybox
        if (skyboxMaterial.HasProperty("_SkyTint"))
            skyboxMaterial.SetColor("_SkyTint", animatedColor);
    }
}

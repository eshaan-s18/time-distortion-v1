using UnityEngine;

public class FlashRed : MonoBehaviour
{
    public Material flashMaterial;
    public float flashSpeed = 2f;  // Pulses per second

    private Renderer rend;
    private Material instanceMaterial;
    private Color baseEmissionColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        
        // Clone material to avoid changing shared material
        instanceMaterial = new Material(flashMaterial);
        rend.material = instanceMaterial;

        instanceMaterial.EnableKeyword("_EMISSION");

        if (instanceMaterial.HasProperty("_EmissionColor"))
        {
            baseEmissionColor = instanceMaterial.GetColor("_EmissionColor");
        }
        else
        {
            baseEmissionColor = Color.red; // fallback if missing
        }

    }

    void Update()
    {
        float intensity = (Mathf.Sin(Time.time * flashSpeed * Mathf.PI * 2f) + 1f) / 2f; // Range: 0–1
        Color emission = baseEmissionColor * intensity;
        instanceMaterial.SetColor("_EmissionColor", emission);
    }
}

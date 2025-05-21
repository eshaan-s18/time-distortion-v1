using UnityEngine;

public class OrbitSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public Transform center;         // Player or world center
    public int numberOfCubes = 9;    // Per row
    public float radius = 2f;
    public float baseHeight = 1f;    // Starting Y
    public float rowSpacing = 0.5f;  // Vertical distance between rows
    public int numberOfRows = 5;     // Number of vertical layers
    public float orbitSpeed = 30f;
    public Material flashingRedMaterial;

    void Start()
    {
        for (int row = 0; row < numberOfRows; row++)
        {
            float y = baseHeight + row * rowSpacing;

            for (int i = 0; i < numberOfCubes; i++)
            {
                float angle = i * Mathf.PI * 2 / numberOfCubes;

                Vector3 position = new Vector3(
                    Mathf.Cos(angle) * radius,
                    y,
                    Mathf.Sin(angle) * radius
                );

                GameObject cube = Instantiate(cubePrefab, center.position + position, Quaternion.identity);

                // Add and configure orbit script
                Oddball1Orbit orbit = cube.AddComponent<Oddball1Orbit>();
                orbit.center = center;
                orbit.orbitSpeed = orbitSpeed;

                // Add and configure flash red script
                FlashRed flash = cube.AddComponent<FlashRed>();
                flash.flashMaterial = flashingRedMaterial;

            }
        }
    }
}

using UnityEngine;

public class Orbit2Spawner : MonoBehaviour
{
    public GameObject redCubePrefab;
    public GameObject greenCubePrefab;
    public Transform center;
    public int numberOfCubes = 9;
    public float radius = 2f;
    public float baseHeight = 1f;
    public float rowSpacing = 0.5f;
    public int numberOfRows = 3;
    public float orbitSpeed = 30f;

    public Material flashingRedMaterial;

    public Material baseFlashMaterial;

    public AudioClip oddballAudio;


    void Start()
    {
        // Pick a random cube to be green (you could make this fixed if you want)
        int specialIndex = Random.Range(0, numberOfCubes * numberOfRows);

        int cubeIndex = 0;

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

                // Choose which prefab to use
                GameObject prefabToUse = (cubeIndex == specialIndex) ? greenCubePrefab : redCubePrefab;

                GameObject cube = Instantiate(prefabToUse, center.position + position, Quaternion.identity);

                // Add and configure orbit script
                Oddball1Orbit orbit = cube.AddComponent<Oddball1Orbit>();
                orbit.center = center;
                orbit.orbitSpeed = orbitSpeed;

                if (cubeIndex == specialIndex)
                {
                    AudioSource audio = cube.AddComponent<AudioSource>();
                    audio.clip = oddballAudio;
                    audio.playOnAwake = true;
                    audio.spatialBlend = 1f;
                    audio.volume = 1f;
                    audio.minDistance = 1f;
                    audio.maxDistance = 1f;
                    audio.loop = true;
                    audio.Play();
                }
                // Add FlashRed only to red cubes
                else
                {
                    FlashRainbow flash = cube.AddComponent<FlashRainbow>();
                    flash.baseMaterial = baseFlashMaterial;
                }

                cubeIndex++;
            }
        }
    }
}

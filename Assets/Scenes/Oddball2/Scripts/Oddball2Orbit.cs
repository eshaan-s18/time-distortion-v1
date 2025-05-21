using UnityEngine;

public class Oddball2Orbit : MonoBehaviour
{
    public Transform center;           // The object to orbit around
    public float orbitSpeed = 30f;     // Degrees per second

    private Vector3 offset;            // Initial offset from center
    private float angle;               // Current angle in radians

    void Start()
    {
        // Compute initial offset and angle
        offset = transform.position - center.position;
        angle = Mathf.Atan2(offset.z, offset.x); // initial angle in radians
    }

    void Update()
    {
        angle += orbitSpeed * Mathf.Deg2Rad * Time.deltaTime;

        float radius = new Vector2(offset.x, offset.z).magnitude;

        Vector3 newPos = new Vector3(
            Mathf.Cos(angle) * radius,
            offset.y,
            Mathf.Sin(angle) * radius
        );

        transform.position = center.position + newPos;
    }
}

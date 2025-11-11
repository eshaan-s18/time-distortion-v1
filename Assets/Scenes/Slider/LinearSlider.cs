using UnityEngine;

public class LinearSlider : MonoBehaviour
{
    public Transform minimum;
    public Transform maximum;

    private Rigidbody rb;
    private Vector3 lineDir;          // Direction of movement
    private Quaternion initialRotation; // For locking orientation

    public void ApplyConstraint()
    {
        // Vector3 lineDir = (maximum.position - minimum.position).normalized;
        // Vector3 projected = minimum.position + Vector3.Project(transform.position - minimum.position, lineDir);
        // float dist = Vector3.Dot(projected - minimum.position, lineDir);
        // float maxDist = Vector3.Distance(minimum.position, maximum.position);
        // dist = Mathf.Clamp(dist, 0, maxDist);
        // transform.position = minimum.position + lineDir * dist;
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation; // prevent spinning
        initialRotation = transform.rotation;                 // remember start rotation
        lineDir = (maximum.position - minimum.position).normalized;
    }

    void FixedUpdate()
    {
        // Project current position onto the slider line
        Vector3 projected = minimum.position + Vector3.Project(transform.position - minimum.position, lineDir);

        // Clamp between endpoints
        float dist = Vector3.Dot(projected - minimum.position, lineDir);
        float maxDist = Vector3.Distance(minimum.position, maximum.position);
        dist = Mathf.Clamp(dist, 0, maxDist);

        // Set final constrained position
        Vector3 targetPos = minimum.position + lineDir * dist;
        rb.MovePosition(targetPos);

        // Keep original orientation
        rb.MoveRotation(initialRotation);
    }
}

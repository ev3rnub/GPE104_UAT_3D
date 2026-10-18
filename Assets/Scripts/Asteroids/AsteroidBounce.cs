using UnityEngine;

public class AsteroidBounce : MonoBehaviour
{
    public float bounceForce = 10f;
    public float bounceDamping = 0.8f;
    public LayerMask floorLayer; // Layer mask for the floor

    private Rigidbody rb;
    private Vector3 initialVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        

        rb.angularDamping = 0.05f;
        
        // Store initial velocity for reference
        initialVelocity = rb.linearVelocity;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if we're colliding with the floor (or ground)
        if (floorLayer == (floorLayer | (1 << collision.gameObject.layer)))
        {
            // Get the collision normal (perpendicular to the surface)
            Vector3 normal = collision.GetContact(0).normal;
            
            // Calculate bounce direction
            Vector3 bounceDirection = Vector3.Reflect(rb.linearVelocity, normal);
            
            // Apply bounce with damping
            rb.linearVelocity = bounceDirection * bounceDamping;
            
            // Add some upward force to prevent the asteroid from getting stuck
            rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        }
    }
}
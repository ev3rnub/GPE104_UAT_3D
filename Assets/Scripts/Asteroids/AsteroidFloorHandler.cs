using UnityEngine;

public class AsteroidFloorHandler : MonoBehaviour
{
    [Header("Bounce Settings")]
    public float bounceForce = 10f;
    public float bounceDamping = 0.8f;
    
    [Header("Collision Settings")]
    public bool preventDestruction = true;  // Prevents destruction when hitting floor
    
    private Rigidbody rb;
    private bool hasBeenProcessed = false;

    void Start()
    {
        // Ensure we have a Rigidbody component
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        
        // Configure Rigidbody for proper physics
        rb.useGravity = true;
        rb.angularDamping = 0.05f;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Only process once per collision to avoid multiple triggers
        if (hasBeenProcessed) return;
        
        // Check if we're colliding with the floor
        if (collision.gameObject.CompareTag("Floor"))
        {
            // Prevent destruction if requested
            if (preventDestruction)
            {
                HandleFloorBounce(collision);
                hasBeenProcessed = true;
            }
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        // Handle trigger collisions with floor
        if (other.CompareTag("Floor"))
        {
            if (preventDestruction)
            {
                // For trigger collisions, we can still bounce
                HandleFloorBounceTrigger(other);
            }
        }
    }
    
    void HandleFloorBounce(Collision collision)
    {
        if (rb != null)
        {
            // Get collision normal (perpendicular to the surface)
            Vector3 normal = collision.GetContact(0).normal;
            
            // Calculate bounce direction using reflection
            Vector3 bounceDirection = Vector3.Reflect(rb.linearVelocity, normal);
            
            // Apply bounce with damping to lose energy
            rb.linearVelocity = bounceDirection * bounceDamping;
            
            // Add upward force to prevent getting stuck
            rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            
            // Log for debugging
            Debug.Log("Asteroid bounced off floor");
        }
    }
    
    void HandleFloorBounceTrigger(Collider floorCollider)
    {
        if (rb != null)
        {
            // Simple bounce from trigger collision
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, -rb.linearVelocity.y * bounceDamping, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        }
    }
    
    // Reset the processed flag when the asteroid is reactivated
    void OnEnable()
    {
        hasBeenProcessed = false;
    }
}
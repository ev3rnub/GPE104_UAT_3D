//Course: GPE104 
//Prof: Matthew Henry 
//Student: Chad V

using UnityEngine;

public class AsteroidFloorCollision : MonoBehaviour
{
    [Header("Bounce Settings")]
    public float bounceForce = 10f;
    public float bounceDamping = 0.8f;
    public LayerMask floorLayer; // Layer mask for the floor
    
    [Header("Collision Settings")]
    public bool destroyOnCollision = false; // Set this to false to prevent destruction
    
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        
        // Make sure the asteroid is affected by physics
        rb.useGravity = true;
        rb.angularDamping = 0.05f;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if we're colliding with a floor object
        if (floorLayer == (floorLayer | (1 << collision.gameObject.layer)))
        {
            // Prevent destruction if destroyOnCollision is false
            if (!destroyOnCollision)
            {
                // Handle bounce physics
                HandleBounce(collision);
            }
            else
            {
                // If we want to destroy on collision, let the normal death process happen
                // This would be handled by the Death component or other systems
            }
        }
    }
    
    void HandleBounce(Collision collision)
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
        }
    }
}
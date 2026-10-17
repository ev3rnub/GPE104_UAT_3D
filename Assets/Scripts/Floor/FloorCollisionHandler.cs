using UnityEngine;

public class FloorCollisionHandler : MonoBehaviour
{
    void Start()
    {
        // Ensure this object has a Collider
        Collider collider = GetComponent<Collider>();
        if (collider == null)
        {
            collider = gameObject.AddComponent<BoxCollider>();
            collider.isTrigger = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object is an asteroid
        if (collision.gameObject.CompareTag("Astroid"))
        {
            HandleAsteroidCollision(collision);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("SHOULDNT destroy the floor?");
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is an asteroid
        if (other.CompareTag("Astroid"))
        {
            HandleAsteroidCollisionTrigger(other);
        }
    }
    
    void HandleAsteroidCollision(Collision collision)
    {
        Debug.Log("Floor hit by asteroid - bouncing asteroid");
        
        // Get the asteroid that hit us
        Rigidbody asteroidRb = collision.gameObject.GetComponent<Rigidbody>();
        if (asteroidRb != null)
        {
            // Get collision normal
            Vector3 normal = collision.GetContact(0).normal;
            
            // Reflect the asteroid's velocity
            Vector3 bounceDirection = Vector3.Reflect(asteroidRb.linearVelocity, normal);
            
            // Apply bounce with damping
            asteroidRb.linearVelocity = bounceDirection * 0.8f;
            
            // Add upward force to prevent getting stuck
            asteroidRb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
        
        // Make sure the floor is not destroyed
        // The floor should remain intact
    }
    
    void HandleAsteroidCollisionTrigger(Collider asteroidCollider)
    {
        Debug.Log("Floor trigger hit by asteroid - bouncing asteroid");
        
        // Get the asteroid that hit us
        Rigidbody asteroidRb = asteroidCollider.GetComponent<Rigidbody>();
        if (asteroidRb != null)
        {
            // Simple bounce (reverse Y velocity)
            asteroidRb.linearVelocity = new Vector3(asteroidRb.linearVelocity.x, -asteroidRb.linearVelocity.y * 0.8f, asteroidRb.linearVelocity.z);
            asteroidRb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }
}
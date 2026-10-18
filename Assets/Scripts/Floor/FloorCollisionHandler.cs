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
        // Enemy?
        if (collision.gameObject.CompareTag("Enemy"))
        {
            HandleEnemyAICollision(collision);
        }
    }
    
    // void OnTriggerEnter(Collider other)
    // {
    //     // Check if the colliding object is an asteroid
    //     if (other.CompareTag("Astroid"))
    //     {
    //         HandleAsteroidCollisionTrigger(other);
    //     }

    // }
    
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
    }

    void HandleEnemyAICollision(Collision collision)
    {
        Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();
        if (enemyRB != null)
        {
            // get col norm
            Vector3 normal = collision.GetContact(0).normal;
            // reflect
            Vector3 bounceDirection = Vector3.Reflect(enemyRB.linearVelocity, normal);
            //apply bounch
            enemyRB.linearVelocity = bounceDirection * 0.8f;
            //add upward force
            enemyRB.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
    }
    
    // void HandleAsteroidCollisionTrigger(Collider asteroidCollider)
    // {
    //     Debug.Log("Floor trigger hit by asteroid - bouncing asteroid");
        
    //     // Get the asteroid that hit us
    //     Rigidbody asteroidRb = asteroidCollider.GetComponent<Rigidbody>();
    //     if (asteroidRb != null)
    //     {
    //         // Simple bounce (reverse Y velocity)
    //         asteroidRb.linearVelocity = new Vector3(asteroidRb.linearVelocity.x, -asteroidRb.linearVelocity.y * 0.8f, asteroidRb.linearVelocity.z);
    //         asteroidRb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
    //     }
    // }
}
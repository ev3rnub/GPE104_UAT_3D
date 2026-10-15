using UnityEngine;

public class BulletSetup : MonoBehaviour
{
    void Start()
    {
        // Add a collider to the bullet if it doesn't have one
        Collider collider = GetComponent<Collider>();
        if (collider == null)
        {
            // Add a simple sphere collider for bullet detection
            SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider>();
            sphereCollider.radius = 0.5f;
            sphereCollider.isTrigger = true; // Make it a trigger so Damager can detect it
        }
        else
        {
            // If it already has a collider, make sure it's a trigger
            collider.isTrigger = true;
        }
    }
}
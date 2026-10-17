//Course: GPE104 
//Prof: Matthew Henry 
//Student: Chad V
//NOT USED yet.

using UnityEngine;

public class AsteroidBreak : MonoBehaviour
{
    public float astroidSize;
    public float minSize = 0.25f;
    public GameObject astroidPrefab;
    public int breakCount = 2;
    public bool broken = false;
    private Death death;

    void Start()
    {
        death = GetComponent<Death>();
        astroidSize = gameObject.transform.localScale.x;
    }
    
    public void Break()
    {
        if (broken) return;   // guards against two bullets killing it in the same frame
        broken = true;

        if (astroidSize > minSize)
        {
            Health myHealth = GetComponent<Health>();
            int childMaxHealth = (myHealth != null) ? Mathf.Max(1, myHealth.maxHealth / 2) : 0;
            float childSize = astroidSize / 2f;

            for (int i = 0; i < breakCount; i++)
            {
                GameObject newAstroid = Instantiate(astroidPrefab, transform.position, Quaternion.identity);
                newAstroid.transform.localScale = new Vector3(childSize, childSize, childSize);

                AsteroidBreak newAstroidScript = newAstroid.GetComponent<AsteroidBreak>();
                if (newAstroidScript != null)
                {
                    newAstroidScript.astroidPrefab = astroidPrefab;
                    newAstroidScript.astroidSize = childSize;
                }

                // Set before the child's Start() runs, so it starts at this value.
                Health childHealth = newAstroid.GetComponent<Health>();
                if (childHealth != null && childMaxHealth > 0)
                {
                    childHealth.maxHealth = childMaxHealth;
                }
            }
        }
    }
    
    // Handle collisions with floor/ground (non-trigger colliders only)
    void OnCollisionEnter(Collision collision)
    {
        // First check if this is a bullet collision - let bullets work normally
        if (collision.gameObject.CompareTag("Bullet"))
        {
            return;
        }
        
        // Check if we're colliding with the floor
        if (IsFloor(collision.gameObject))
        {
            Debug.Log("Asteroid hit floor - bouncing instead of destroying");
            HandleFloorBounce(collision);
        }
    }
    
    bool IsFloor(GameObject obj)
    {
        // Method 1: Check by tag
        if (obj.CompareTag("Floor"))
        {
            return true;
        }
        
        // Method 2: Check by name (case insensitive)
        if (obj.name.ToLower().Contains("floor"))
        {
            return true;
        }
        
        // Method 3: Check by layer
        int floorLayer = LayerMask.NameToLayer("Floor");
        if (floorLayer != -1 && obj.layer == floorLayer)
        {
            return true;
        }
        
        return false;
    }
    
    void HandleFloorBounce(Collision collision)
    {
        // Get the asteroid's Rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Get collision normal (perpendicular to the surface)
            Vector3 normal = collision.GetContact(0).normal;
            
            // Calculate bounce direction using reflection
            Vector3 bounceDirection = Vector3.Reflect(rb.linearVelocity, normal);
            
            // Apply bounce with damping to lose some energy
            rb.linearVelocity = bounceDirection * 0.8f;
            
            // Add upward force to prevent getting stuck
            rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }
}
using UnityEngine;

public class AstronautPickup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.someGameManager != null)
            {
                GameManager.someGameManager.OnAstronautPickedUp();
            }
            Destroy(gameObject);
        }
    }
}
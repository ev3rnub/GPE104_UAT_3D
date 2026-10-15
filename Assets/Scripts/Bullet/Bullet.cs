//Course: GPE104 
//Prof: Matthew Henry 
//Student: Chad V

using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public GameManager someGameManager;
    public float bulletForce = 300;
    private Rigidbody rb;
    private Transform tf;
    private Death death;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        death = GetComponent<Death>();

        GameManager.someGameManager.bulletList.Add(this);

        if (rb != null && tf != null)
        {
            Debug.Log($"Bullet force: {bulletForce}, forward: {tf.forward}");
            rb.AddForce(tf.forward * bulletForce);
            Debug.Log($"Bullet velocity after force: {rb.linearVelocity}");

            //I wanted to add a deployed projectile like seen in RL with missles, where an initial charge deploys the weapon
            //payload and then the weapon ignites, speeding up. I decided to try and use the coroutine to increase the bulletforce
            // after .3 seconds
            StartCoroutine(ChangeVariableAfterTime());
            // this will ensure we don't run out of memory due to 10000000s of bullets. After 10 seconds the bullet is destroyed.
            StartCoroutine(DestroyAfterTime());
        }

        // // after .3 seconds increase bullet force to 500
        IEnumerator ChangeVariableAfterTime()
        {
            // Wait for exactly 3 seconds
            yield return new WaitForSeconds(0.2f);
            Debug.Log("CHANGED Bforce to 2000");
            bulletForce = 2000;
            rb.AddForce(tf.forward * bulletForce);
        }

        // after 10 seconds destroy bullet.
        IEnumerator DestroyAfterTime()
        {
            yield return new WaitForSeconds(10f);
            Debug.Log($"{this} Bullet has died due to not impacting anything");
            death.Die();
        }
    }
}

using UnityEngine;

public class ShooterBullet : Shooter
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public AudioSource audioSource;
    public AudioClip shootClip;

    void start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void Shoot()
    {

        if (audioSource != null && shootClip != null)
        {
          audioSource.PlayOneShot(shootClip);
        }

        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            Debug.Log($"Shooting from {bulletSpawnPoint.name}, forward: {bulletSpawnPoint.forward}");
            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
           if (GameManager.someGameManager != null)
           {
               GameManager.someGameManager.bulletsFired++;
           }
        }
        else
        {
            //OOAC (replace debug log call w/custom GUI status message center or box etc.)
            Debug.Log("[STATUS] Out of AMMO or CHARGE!");
            if (bulletPrefab == null) Debug.Log("  -> bulletPrefab is null");
            if (bulletSpawnPoint == null) Debug.Log("  -> bulletSpawnPoint is null");
        }
    }
}

using UnityEngine;
using UnityEngine.Audio;

public class AstroidSFX : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip explosionClip;
    public float volume = 1f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnDestroy()
    {
        AudioSource.PlayClipAtPoint(explosionClip, transform.position, volume);
    }
}

using UnityEngine;
using UnityEngine.Audio;

public class PawnThrusterSFX : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip thrusterClip;
    public float volume = 0.5f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


}
//Course: GPE104 
//Prof: Matthew Henry
//Student: Chad V

using UnityEngine;

public class PawnSpaceship : Pawn
{

    [Header("MoveSpeed")]
    public float moveSpeed = 3;
    public float turnSpeed = 0.005f;

    [Header("GaameObjects")]
    public Transform tf;
    private Shooter sh;
    private Rigidbody rb;

    [Header("Altitude Limit")]
    public float maxAltitudeAboveSpawn = 200f;
    private float spawnHeight;

    [Header("ThrusterSFX")]
    public AudioSource audioSource;
    public AudioClip thrusterClip;
    public float volume = 0.5f;
    private bool isThrusterPlaying = false;

    [Header("LaserSFX")]
    public AudioSource laserAudioSource;

    // using addForce, AddTorque to move the pawn w/forces. 
    public override void Move(Vector3 someVec3)
    {
        if (rb != null) rb.AddForce(someVec3 * moveSpeed, ForceMode.Force);
        isThrusterPlaying = true;
    }

    public override void Rotate3D(float someEuler)
    {
        // Vector3.up handles Y-axis turning (Yaw)
        if (rb != null) rb.AddTorque(transform.up * someEuler * turnSpeed, ForceMode.Force);
    }

    public override void Pitch3D(float direction)
    {
        // transform.right handles local X-axis tilting (Pitch up/down)
        if (rb != null) rb.AddTorque(transform.right * direction * turnSpeed, ForceMode.Force);
    }

    public override void Roll3D(float direction)
    {
        // Vector3.forward handles the Z-axis (rolling left and right)
        // Space.Self is crucial so it rolls relative to the ship's current tilt
        if (rb != null) rb.AddTorque(transform.forward * direction * turnSpeed, ForceMode.Force);
    }

    public override void Shoot()
    {
        //shoot
        if (sh != null)
        {
            sh.Shoot();
        }
    }

    // stop all spinning etc
    public override void Brake()
    {
        //rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.linearVelocity = Vector3.zero;
        audioSource.Stop();
        isThrusterPlaying = false;
    }

    // use arrow keys to trust directionally, simulating RCS thrusters.
    public override void StrafeLeft(float amount)
    {
       if (rb != null) rb.AddForce(-transform.right * amount * moveSpeed, ForceMode.Force);
    }

    public override void StrafeRight(float amount)
    {
       if (rb != null) rb.AddForce(transform.right * amount * moveSpeed, ForceMode.Force);
    }

    public override void StrafeUp(float amount)
    {
       if (rb != null) rb.AddForce(transform.up * amount * moveSpeed, ForceMode.Force);
    }

    public override void StrafeDown(float amount)
    {
       if (rb != null) rb.AddForce(-transform.up * amount * moveSpeed, ForceMode.Force);
    }


    //AI Pawn Seek functions that looks for the player and seeks them out. 
    public override void Seek(GameObject someGameObject)
    {
        Seek(someGameObject.transform);
    }

    public override void Seek(Transform someTransform)
    {
        Seek(someTransform.position);
    }

    public override void Seek(Vector3 someVec3)
    {
        RotateTowards(someVec3);
        Move(tf.forward);
    }

    public void RotateTowards(Vector3 someVec3)
    {
        //Find Player Vector
        Vector3 vec3ToTarget = someVec3 - tf.position;
        //Find rotation instructions
        Quaternion lookRotation = Quaternion.LookRotation(vec3ToTarget, Vector3.up);
        // rotate a bit each frame. 
        tf.rotation = Quaternion.RotateTowards(tf.rotation, lookRotation, turnSpeed * Time.deltaTime);
    }


    void Update()
    {
        // Check if ship is moving
        float speed = rb.linearVelocity.magnitude;
        float minSpeedForThruster = 0.2f; // Threshold to consider "moving"

       if (speed > minSpeedForThruster)
       {
           if (!audioSource.isPlaying)
           {
               audioSource.Play();
           }
           isThrusterPlaying = true;
       }
       else
       {
           if (audioSource.isPlaying)
           {
               audioSource.Stop();
           }
           isThrusterPlaying = false;
       }
    }

    //use late update to teleport back below height limit. 
    void LateUpdate()
    {
        if (rb == null) return;

        float limit = spawnHeight + maxAltitudeAboveSpawn;
        if (transform.position.y > limit)
        {
        // Teleport back just under the cap
        Vector3 pos = transform.position;
        pos.y = limit - 1f;
        transform.position = pos;

        // Kill upward momentum, or it re-crosses the cap next frame
        Vector3 vel = rb.linearVelocity;
        if (vel.y > 0f) vel.y = 0f;
        rb.linearVelocity = vel;
        }
    }

    //start
    public void Start()
    {
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        sh = GetComponent<ShooterBullet>();
        spawnHeight = transform.position.y;
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null && thrusterClip != null)
        {
           audioSource.clip = thrusterClip;
           audioSource.volume = volume;
           audioSource.loop = true;
        }
        // Get all AudioSources on this GameObject
        AudioSource[] audioSources = GetComponents<AudioSource>();

        if (audioSources.Length >= 2)
        {
            laserAudioSource = audioSources[1]; // Second AudioSource (index 1)
        }        
    }
}
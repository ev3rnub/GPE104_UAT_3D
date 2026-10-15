//Course: GPE104 
//Prof: Matthew Henry
//Student: Chad V

using UnityEngine;

public class PawnSpaceship : Pawn
{

    public float moveSpeed = 3;
    public float turnSpeed = 0.005f;
    public Transform tf;
    private Shooter sh;
    private Rigidbody rb;


    // using addForce
    public override void Move(Vector3 someVec3)
    {
        if (rb != null) rb.AddForce(someVec3 * moveSpeed, ForceMode.Force);
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

    public void Start()
    {
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        sh = GetComponent<ShooterBullet>();
    }
}
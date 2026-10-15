using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 3, -6);
    private float smoothSpeed = 55.0f;
    private float rotationSmoothSpeed = 55.0f; // Added for smooth rotation

    void Start()
    {
        // Automatically find the PawnSpaceship if target is not assigned
        if (target == null)
        {
            GameObject pawn = GameObject.FindGameObjectWithTag("PawnStarShip");
            if (pawn != null)
            {
                target = pawn.transform;
            }
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // 1. Calculate desired position relative to the target's local rotation
            Vector3 desiredPosition = target.TransformPoint(offset);
            
            // 2. Smoothly lerp the position
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            
            // 3. Smoothly lerp the rotation to match the target's rotation facing forward
            Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSmoothSpeed * Time.deltaTime);
        }
    }
}

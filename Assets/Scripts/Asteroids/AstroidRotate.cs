//Course: GPE104 
//Prof: Matthew Henry 
//Student: Chad V

using UnityEngine;                                                                                                                                               

[RequireComponent(typeof(Rigidbody))]
public class AstroidRotate : MonoBehaviour
{
   public float astroidRotateSpeed = 45f; // Increased for deltaTime correction
   public float moveSpeed = 2f;

   private Rigidbody rb;

   void Start()
   {
       rb = GetComponent<Rigidbody>();

       // 1. Set random movement direction using physics velocity
       float randomAngle = Random.Range(0f, 360f);
       Vector3 moveDirection = new Vector3(Mathf.Sin(randomAngle * Mathf.Deg2Rad), Mathf.Cos(randomAngle * Mathf.Deg2Rad), 0f);
       
       rb.linearVelocity = moveDirection * moveSpeed;

       // 2. Set constant physics-based rotation (spinning on the Z axis)
       // This forces the Rigidbody to spin perfectly around its physics center of mass!
       rb.angularVelocity = new Vector3(0f, 0f, astroidRotateSpeed * Mathf.Deg2Rad);
   }
}
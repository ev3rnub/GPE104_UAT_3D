//Course: GPE104 
//Prof: Matthew Henry 
//Student: Chad V

using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    public MyController someController; 

    public abstract void Move(Vector3 someMoveVector);
    public abstract void Rotate3D(float someEuler);
    public abstract void Pitch3D(float someDirection);
    public abstract void Roll3D(float someDirection);
    public abstract void Shoot();
    public abstract void Brake();
    public abstract void StrafeUp(float someDirection);
    public abstract void StrafeDown(float someDirection);
    public abstract void StrafeRight(float someDirection);
    public abstract void StrafeLeft(float someDirection);
}

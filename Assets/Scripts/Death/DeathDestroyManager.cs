//Course: GPE104 
//Prof: Matthew Henry 
//Student: Chad V

using UnityEngine;
using System;
using System.Collections;

public class DeathDestroyManager : Death
{
    private Obstacle obstacleToRemove;
    private Bullet bullet;
    public int someScoreValue = 10;
    public bool destroyOnFloorCollision = false;

    public override void Die()
    {
        // Check if we're colliding with the floor - if so, don't destroy
        // We'll use a simple approach: check if the object is close to the ground
        // or we can check collision contacts
        

        if (GameManager.someGameManager != null)
        {
            if (GameManager.someGameManager.obstacleList != null && obstacleToRemove != null)
            {
                GameManager.someGameManager.obstacleList.Remove(obstacleToRemove);
            }
            GameManager.someGameManager.someScore += someScoreValue;
            GameManager.someGameManager.someStatusMsg.text = $"Obtained {someScoreValue} points, total is now {GameManager.someGameManager.someScore}";

            // Reworked bullet scoring. 
            // if (GameManager.someGameManager.bulletList != null)
            // {
            //     GameManager.someGameManager.bulletList.Remove(bulletToRemove);
            //     //convert our string to an int
            //     string numberString = GameManager.someGameManager.someBulletValue.text;
            //     int number = Convert.ToInt32(numberString);
            //     number += 1;
            //     GameManager.someGameManager.someBulletValue.text = number.ToString();
            // }
        }

        AsteroidBreak asteroidBreak = GetComponent<AsteroidBreak>();
        if (asteroidBreak != null)
        {
            // If this is an asteroid, handle the break logic but don't destroy
            asteroidBreak.Break();

        }
        
        //removes gameobject from scene.
        Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        obstacleToRemove = GetComponent<Obstacle>();
        Debug.Log($"Removing obstacle. Total before: {GameManager.someGameManager.obstacleList.Count}");
        GameManager.someGameManager.obstacleList.Remove(obstacleToRemove);
        Debug.Log($"Obstacle removed. Total after: {GameManager.someGameManager.obstacleList.Count}");        
    }
    
    // Handle floor collisions to prevent destruction
    void OnCollisionEnter(Collision collision)
    {
        // If this is an asteroid hitting the floor, prevent destruction
        if (collision.gameObject.CompareTag("Floor"))
        {
            Debug.Log("Asteroid hit floor - preventing destruction");
            // We prevent the destruction by not calling Die() in this case
            // The AsteroidBreak script should handle the bounce
        }
    }
}
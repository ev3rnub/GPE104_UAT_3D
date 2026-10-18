using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.someGameManager.obstacleList.Add(this);
        Debug.Log($"Adding obstacle to list. Total before: {GameManager.someGameManager.obstacleList.Count}");
        Debug.Log($"Obstacle added. Total after: {GameManager.someGameManager.obstacleList.Count}");        
    }
}

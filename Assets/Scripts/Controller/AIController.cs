using UnityEngine;

public class AIController : MyController
{
    public GameObject someGameObject; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("MEOW");   
    }

    // Update is called once per frame
    void Update()
    {
        SeekPlayer();
    }

    public void SeekPlayer()
    {
        somePawn.Seek(someGameObject);
    }
}

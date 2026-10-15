using UnityEngine;

public class DeathDeactivate : Death
{
    public override void Die()
    {
        Debug.Log($"Deactivating {gameObject}");
        gameObject.SetActive(false);
    }
}
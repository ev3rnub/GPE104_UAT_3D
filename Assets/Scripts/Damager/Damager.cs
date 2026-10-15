//Course: GPE104 
//Prof: Matthew Henry 
//Student: Chad V

using UnityEngine;

public class Damager : MonoBehaviour
{

    public int damageAmt;
    public bool isInstantKill;

    private Health otherHealthComponent;


    private void OnTriggerEnter(Collider someCollision)
    {
        otherHealthComponent = someCollision.GetComponent<Health>();
        Debug.Log($"{otherHealthComponent}");

        if (otherHealthComponent != null)
        {
            if (isInstantKill)
            {
                Debug.Log("INSTANT KILL");
                otherHealthComponent.InstantDeath();
            }
            else
            {
                otherHealthComponent.SubHealth(damageAmt);    
            }
            Destroy(gameObject);
        }
    }
}

//Course: GPE104 
//Prof: Matthew Henry
//Student: Chad V

using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int minHealth = 0;
    public int currHealth;
    public Death death;
    public Image healthBar;



    // init: init currHealth to maxHealth and grab a reference to the Component Death. 
    void Start()
    {
        currHealth = maxHealth;
        death = GetComponent<Death>();
        if (death != null)
        {
            Debug.Log($"{death}");
        }

        if (healthBar != null)
        {
            healthBar.fillAmount = (float)maxHealth / currHealth;
        }
    }

    // Subtract health, if at or below 0, die.
    public void SubHealth(int someDmg)
    {
        int someCurrHealth = currHealth - someDmg;
        if (someCurrHealth > 0)
        {
            currHealth = someCurrHealth;
        }
        else if (someCurrHealth <= 0 && death != null)
        {
            InstantDeath();
        }

       if (healthBar != null)
        {
            healthBar.fillAmount = (float)currHealth / maxHealth;
        }
    }

    // Add health, if above maxhealth, reduce to maxhealth. 
    public void AddHealth(int someHealth)
    {
        int someCurrHealth = currHealth + someHealth;
        if (someCurrHealth > maxHealth)
        {
            currHealth = someCurrHealth;
        }
        else
        {
            currHealth = maxHealth;
        }

       if (healthBar != null)
        {
            healthBar.fillAmount = (float)currHealth / maxHealth;
        }
    }

    // Instant death
    public void InstantDeath()
    {
        currHealth = 0;
        death.Die();

       if (healthBar != null)
        {
            healthBar.fillAmount = (float)currHealth / maxHealth;
        }
    }


    // instant heal
    public void InstantHeal()
    {
        currHealth = maxHealth;

       if (healthBar != null)
        {
            healthBar.fillAmount = (float)currHealth / maxHealth;
        }
    }


    void Update()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currHealth / maxHealth;
        }
    }
}

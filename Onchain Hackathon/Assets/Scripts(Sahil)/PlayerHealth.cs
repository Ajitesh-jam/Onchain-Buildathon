using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        
        if (currentHealth == 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Handle player death (e.g., respawn, game over)
        Debug.Log("Player died");
    }
}
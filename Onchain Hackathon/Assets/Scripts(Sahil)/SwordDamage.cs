using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    public int damage = 50;

    // void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.gameObject.CompareTag("Enemy"))
    //     {
    //         Health enemyHealth = collision.gameObject.GetComponent<Health>();
    //         if (enemyHealth != null)
    //         {
    //             enemyHealth.TakeDamage(damage);
    //         }
    //     }
    // }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detected with: " + other.gameObject.name);
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy detected: " + other.gameObject.name);
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                Debug.Log("Applying damage to: " + other.gameObject.name);
                enemyHealth.TakeDamage(damage);
            }
        }
    }
}
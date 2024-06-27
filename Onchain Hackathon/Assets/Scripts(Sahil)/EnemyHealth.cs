using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    private Material dissolveMaterial;
    private bool isDissolving = false;
    private float dissolveAmount = 0f;
    [SerializeField] private Renderer renderer;
    void Start()
    {
        currentHealth = maxHealth;
        // Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            dissolveMaterial = renderer.material;
        }
        else
        {
            Debug.LogError("Renderer component not found on the enemy.");
        }
    }

    void Update()
    {
        if (isDissolving)
        {
            dissolveAmount += Time.deltaTime;
            dissolveMaterial.SetFloat("_DissolveAmount", dissolveAmount);

            if (dissolveAmount >= 1f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            StartDissolve();
        }
    }

    void StartDissolve()
    {
        isDissolving = true;
    }
}
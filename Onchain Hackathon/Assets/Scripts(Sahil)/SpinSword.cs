using UnityEngine;

public class SpinSword : MonoBehaviour{
    [SerializeField] private Transform playerTransform;
    public float rotationSpeed = 100f; // Speed of the sword's own rotation
    public float revolutionSpeed = 50f; // Speed of the sword's revolution around the player
    void Start()
    {
        // Find the SwordPivot GameObject if not assigned in the Inspector
        if (playerTransform == null)
        {
            playerTransform = GameObject.Find("PlayerArmature").transform;
        }
    }
    void Update()
    {
        transform.parent.position = playerTransform.position + Vector3.up;
        // Rotate the sword around its own Y-axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Revolve the pivot (and thus the sword) around the player's Y-axis
        transform.parent.Rotate(Vector3.up, revolutionSpeed * Time.deltaTime);
    }
}
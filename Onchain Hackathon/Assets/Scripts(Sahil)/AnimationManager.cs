using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator animator;
    private int velocityHash;
    private int attackHash;

    void Awake()
    {
        animator =  GetComponentInChildren<Animator>();

        // Precompute the hashes for the parameter names
        velocityHash = Animator.StringToHash("velocity");
        attackHash = Animator.StringToHash("attack");
    }

    public void SetVelocity(float velocity)
    {
        animator.SetFloat(velocityHash, velocity);
    }

    public void SetAttack(bool isAttacking)
    {
        animator.SetBool(attackHash, isAttacking);
    }
}
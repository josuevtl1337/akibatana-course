using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class EnemyMeleeAttack : MonoBehaviour
{
    public float damage;
    public float attackDistance;

    [Header("Cadency settings")]
    public float attackCadency;
    public float attackTimming;

    private IAController iaController;
    private Health playerHealth;

    [Header("Animation settings")]
    public Animator animator;
    private bool attackAnim = true;

    private void Start()
    {
        iaController = GetComponent<IAController>();
        if (iaController)
        {
            playerHealth = iaController.targetTransform.GetComponent<Health>();
        }
    }

    private void Update()
    {
        attackTimming += Time.deltaTime;
        if (playerHealth != null)
        {
            if (iaController.targetDistance <= attackDistance)
            {
                if (attackTimming >= attackCadency)
                {
                    if (attackAnim)
                    {
                        animator.SetTrigger("Attack");
                        attackAnim = false;

                        Invoke(nameof(ResetAttackAnim), 1.5f);
                    }
                    playerHealth.ApplyDamage(damage);
                    attackTimming = 0;
                }
            }
        }
    }
    private void ResetAttackAnim() 
    {
        attackAnim = true;
    }

}

using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    public float damage;
    public float attackDistance;

    [Header("Cadency settings")]
    public float attackCadency;
    public float attackTimming;

    private IAController iaController;
    private Health playerHealth;

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
                    playerHealth.ApplyDamage(damage);
                    attackTimming = 0;
                }
            }
        }
    }

}

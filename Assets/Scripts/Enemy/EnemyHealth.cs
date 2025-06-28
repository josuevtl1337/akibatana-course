using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : Health
{
    [Header("Enemy Bar Health")]
    public Slider healthBar;

    public override void Start()
    {
        base.Start();

        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }
    public override void ApplyDamage(float damage)
    {
        base.ApplyDamage(damage);
        healthBar.value = health;
    }
    public override void Death()
    {
        base.Death();
        Destroy(gameObject);
        EnemySpawner.Instance.EnemyDeath();
    }
}

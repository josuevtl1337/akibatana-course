using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    private PlayerController playerController;
    private PlayerWeaponController playerWeaponController;
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerWeaponController = GetComponent<PlayerWeaponController>();
    }
    public override void ApplyDamage(float damage)
    {
        base.ApplyDamage(damage);

        HudPlayerHealth.Instance.UpdateHealth(health, maxHealth);
    }

    public override void Death()
    { 
        base.Death();
        playerController.enabled = false;
        playerWeaponController.enabled = false;
        HudGameOver.Instance.ShowGameOver();

        Invoke(nameof(ResetLevel), 5);
    }

    private void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

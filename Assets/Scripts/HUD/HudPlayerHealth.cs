using UnityEngine;
using UnityEngine.UI;

public class HudPlayerHealth : MonoBehaviour
{
    public static HudPlayerHealth Instance;
    public Slider playerHealthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
    }
    public void UpdateHealth(float health, float maxHealth)
    {
        playerHealthBar.value = health;
        playerHealthBar.maxValue = maxHealth;
    }
}

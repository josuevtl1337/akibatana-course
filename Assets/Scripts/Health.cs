using UnityEngine;

public class Health : MonoBehaviour
{

    public float health;
    public float maxHealth;
    private bool death;
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void ApplyDamage(float damage) 
    {
        if (death)
            return;

        health -= damage;

        if (health <= 0) {
            Death();
        }
    }
    public virtual void Death() {
        health = 0;
        //Destroy(gameObject);
        death = true;
    }
}

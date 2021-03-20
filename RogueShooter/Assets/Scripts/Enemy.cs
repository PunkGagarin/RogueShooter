using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float health;

    public void TakeDamage(float damage) {
        health -= damage;
        if(health <= 0) {
            Die();
        }
    }

    public void Die() {
        //TODO: add deathEffect
        Destroy(gameObject);
    }
}

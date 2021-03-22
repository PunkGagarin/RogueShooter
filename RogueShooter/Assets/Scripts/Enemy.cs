using UnityEngine;

public class Enemy : MonoBehaviour
{

    private float timeBtwAttack;
    public float startTimeBtwAttack = 1f;

    public float health;
    public float speed;
    public float normalSpeed = 5f;

    private float stopTime;
    public float startStopTime;

    public float damage;

    private Animator animator;
    private Player player;

    private void Start() {
        player = FindObjectOfType<Player>();
        animator = GetComponent<Animator>();
        speed = normalSpeed;
    }


    private void Update() {
        if(stopTime <= 0) {
            speed = normalSpeed;
        } else {
            speed = 0;
            stopTime -= Time.deltaTime;
        }

        transform.Translate((player.transform.position - transform.position).normalized * speed * Time.deltaTime);
    }

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

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            animator.SetTrigger("Attack");
        } else {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    public void OnEnemyAttack() {
        player.TakeDamage(damage);
    }
}

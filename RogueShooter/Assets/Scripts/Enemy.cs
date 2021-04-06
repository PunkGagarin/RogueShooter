using UnityEngine;

public class Enemy : MonoBehaviour {
    private float timeBtwAttack;
    public float startTimeBtwAttack = 1f;

    public float health;
    private float speed;
    public float normalSpeed = 5f;

    private float stopTime;
    public float startStopTime;

    private bool isFacingRight;

    public float damage;

    private Animator animator;
    private Player player;

    private void Start() {
        player = FindObjectOfType<Player>();
        animator = GetComponent<Animator>();
        isFacingRight = player.transform.position.x > transform.position.x;
    }

    private void Update() {
        if (stopTime <= 0) {
            speed = normalSpeed;
        }
        else {
            speed = 0;
            stopTime -= Time.deltaTime;
        }
        if (!isFacingRight && player.transform.position.x > transform.position.x) {
            TransformUtils.Flip(transform, ref isFacingRight);
            // transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (isFacingRight && player.transform.position.x < transform.position.x) {
            TransformUtils.Flip(transform, ref isFacingRight);
            // transform.eulerAngles = new Vector3(0, 0, 0);
        }
        // if ((player.transform.position - transform.position).magnitude > 1f)
        // transform.Translate((player.transform.position - transform.position).normalized * speed * Time.deltaTime);
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void TakeDamage(float damage) {
        stopTime = startStopTime;
        health -= damage;
        if (health <= 0) {
            Die();
        }
    }

    private void Die() {
        //TODO: add deathEffect
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            animator.SetTrigger("Attack");
        }
        else {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    public void OnEnemyAttack() {
        player.TakeDamage(damage);
        timeBtwAttack = startTimeBtwAttack;
    }
}
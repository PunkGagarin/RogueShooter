using UnityEngine;

public class Enemy : MonoBehaviour {
    private float timeBtwAttack;
    public float startTimeBtwAttack = 1f;
    public bool isRanged;
    public GameObject meleeWeapon;

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
        player = Player.GetInstance;
        animator = GetComponent<Animator>();
        isFacingRight = player.transform.position.x > transform.position.x;
        if(isRanged)
            meleeWeapon.SetActive(false);
    }

    private void Update() {
        CheckForStop();
        
        EnemyFlip();
        
        EnemyMove();
    }

    private void CheckForStop() {
        if (stopTime <= 0) {
            speed = normalSpeed;
        }
        else {
            speed = 0;
            stopTime -= Time.deltaTime;
        }
    }

    private void EnemyFlip() {
        if (player.transform.position.x > transform.position.x) {
            // TransformUtils.Flip(transform, ref isFacingRight);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (player.transform.position.x < transform.position.x) {
            // TransformUtils.Flip(transform, ref isFacingRight);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    
    private void EnemyMove() {
        // if ((player.transform.position - transform.position).magnitude > 1f)
        // transform.Translate((player.transform.position - transform.position).normalized * speed * Time.deltaTime);
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void TakeDamage(float damage) {
        Debug.Log("enemy just taken "+ damage + "damage");
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
        if(isRanged)
            return;
        
        if (collision.CompareTag("Player")) {
            animator.SetTrigger("Attack");
        }
        else {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    public void OnEnemyAttack() {
        player.ChangeHealth(damage);
        timeBtwAttack = startTimeBtwAttack;
    }
}
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Player_MeleeAttack : MonoBehaviour {
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    //public float attackAnimationLenght = 0.11f;

    public Transform attackPos;
    public LayerMask enemyMask;
    public float attackRange;
    public int damage;

    private Animator animator;


    private void Start() {
        animator = GetComponent<Animator>();
    }
    private void Update() {
        if (timeBtwAttack <= 0) {
            if (Input.GetMouseButton(0)) {
                animator.SetTrigger("Attack");
                //There is another aproach through Coroutine like below
                //StartCoroutine(OnAttack());
                timeBtwAttack = startTimeBtwAttack;
            }
        } else {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    public void OnAttack() {
        //yield return new WaitForSeconds(attackAnimationLenght);
        Debug.Log("On attack method");
        HashSet<GameObject> parents = new HashSet<GameObject>();
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyMask);
        foreach (var enemyHitted in enemies) {
            parents.Add(enemyHitted.gameObject);
        }
        foreach (var parent in parents) {
            parent.GetComponent<Enemy>().TakeDamage(damage);
            Debug.Log("enemy taken damage");
        }
    }

    public void OnAnythingElse() {

    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}

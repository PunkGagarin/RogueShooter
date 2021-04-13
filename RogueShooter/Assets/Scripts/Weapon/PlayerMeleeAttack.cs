using UnityEngine;
using System.Collections.Generic;

public class PlayerMeleeAttack : MonoBehaviour {
    private float timeBtwAttack;

    public float startTimeBtwAttack;

    public Transform attackPos;
    public float attackRange;
    public int damage;

    public LayerMask enemyMask;
    public Animator animator;
    private static readonly int Attack = Animator.StringToHash("Attack");

    private void Update() {
        MeleeAttack();
    }

    private void MeleeAttack() {
        if (timeBtwAttack <= 0) {
            if (Input.GetMouseButton(0)) {
                animator.SetTrigger(Attack);
                timeBtwAttack = startTimeBtwAttack;
            }
        }
        else {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    //TODO: т.к. аниматор не видит детей объекта, нужно делегировать эту функцию родителю.
    public void OnAttack() {
        Debug.Log("On attack method");
        HashSet<GameObject> parents = new HashSet<GameObject>();
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyMask);
        foreach (var enemyHit in enemies) {
            parents.Add(enemyHit.gameObject);
        }
        foreach (var parent in parents) {
            parent.GetComponent<Enemy>().TakeDamage(damage);
            Debug.Log("enemy taken damage");
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
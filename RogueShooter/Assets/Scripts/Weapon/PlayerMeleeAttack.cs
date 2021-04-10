using UnityEngine;
using System.Collections.Generic;

public class PlayerMeleeAttack : MonoBehaviour {
    private float timeBtwAttack;

    public float startTimeBtwAttack;
    //public float attackAnimationLenght = 0.11f;

    public Transform attackPos;
    public LayerMask enemyMask;
    public float attackRange;
    public int damage;

    private WeaponSwitcher weaponSwitcher;
    private Animator animator;
    private static readonly int Attack = Animator.StringToHash("Attack");

    private void Start() {
        animator = GetComponent<Animator>();
        weaponSwitcher = GetComponent<WeaponSwitcher>();
    }

    private void Update() {
        if (timeBtwAttack <= 0) {
            if (Input.GetMouseButton(0) && weaponSwitcher.meleeWeapon.activeInHierarchy) {
                Debug.Log(weaponSwitcher.meleeWeapon);
                animator.SetTrigger(Attack);
                //There is another approach through Coroutine like below
                //StartCoroutine(OnAttack());
                timeBtwAttack = startTimeBtwAttack;
            }
        }
        else {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    public void OnAttack() {
        //yield return new WaitForSeconds(attackAnimationLenght);
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
using System;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed;
    public float damage;
    public float lifeTime;
    public float distance;
    public LayerMask whatShouldWeHit;

    public GameObject bulletImpactEffect;

    private void Start() {
        Invoke(nameof(DestroyBullet), lifeTime);
    }

    void Update() {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatShouldWeHit);
        Collider2D hitObject = hitInfo.collider;
        if (hitObject != null) {
            if (hitObject.TryGetComponent(out Enemy enemy)) {
                enemy.TakeDamage(damage);
            }
            else if (hitObject.TryGetComponent(out Player player)) {
                player.ChangeHealth(damage);
            }
            GameObject effect = Instantiate(bulletImpactEffect, transform.position, Quaternion.identity);
            Destroy(effect, lifeTime);
            DestroyBullet();
        }
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void DestroyBullet() {
        Destroy(gameObject);
    }
}
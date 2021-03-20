using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed;
    public float damage;
    public float lifeTime;
    public float distance;
    public LayerMask whatShouldWeHit;

    public GameObject bulletImpactEffect;

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatShouldWeHit);
        Collider2D hitObject = hitInfo.collider;
        if (hitObject != null) {
            if (hitObject.CompareTag("Enemy")) {
                hitObject.GetComponent<Enemy>().TakeDamage(damage);
            }
            GameObject effect = Instantiate(bulletImpactEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
            Destroy(gameObject);
        }
        transform.Translate(Vector2.up * speed * Time.deltaTime);


    }


    //private void OnTriggerEnter2D(Collider2D collision) {
    //        collision.GetComponent<Enemy>().TakeDamage(damage);
    //        GameObject effect = Instantiate(bulletImpactEffect, transform.position, Quaternion.identity);
            //Destroy(effect, 5f);
    //        Destroy(gameObject);
    //}
}

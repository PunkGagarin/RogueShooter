using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public float offset = 0f;
    public Bullet bullet;
    public Transform firePoint;
    //public Bullet bullet;
    public float bulletLifetime = 2f;


    private float timeBetweenShts;
    public float startTimeBetweenShts = 0.25f;

    void Update() {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.x, difference.y) * -Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (timeBetweenShts <= 0) {
            if (Input.GetMouseButton(0)) {
                GameObject firedBullet = Instantiate(bullet.gameObject, firePoint.position, transform.rotation);
                timeBetweenShts = startTimeBetweenShts;
                //firedBullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bullet.speed, ForceMode2D.Impulse);
                Destroy(firedBullet, bulletLifetime);
            }
        }
        timeBetweenShts -= Time.deltaTime;
    }
}
 
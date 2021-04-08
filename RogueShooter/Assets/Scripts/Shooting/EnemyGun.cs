using UnityEngine;

public class EnemyGun : AbstractGun {

    protected override void NavigateWeapon() {
        direction = player.transform.position - transform.position;
        rotZ = Mathf.Atan2(direction.x, direction.y) * -Mathf.Rad2Deg;
        
        firePoint.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
    }

    protected override void ShootBullet() {
        Instantiate(bullet.gameObject, firePoint.position, firePoint.rotation);
        timeBetweenShts = startTimeBetweenShts;
        //firedBullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bullet.speed, ForceMode2D.Impulse);
    }
}
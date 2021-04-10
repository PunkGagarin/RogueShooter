using UnityEngine;

public abstract class AbstractGun : MonoBehaviour {
    public float offset;
    public Bullet bullet;

    public Transform firePoint;
    public float timeBetweenShts;
    public float startTimeBetweenShts = 0.25f;

    protected Vector3 direction;
    protected float rotZ;
    protected Player player;

    protected void Start() {
        player = Player.GetInstance;
    }

    protected void Update() {
        NavigateWeapon();
        Shoot();
    }

    private void Shoot() {
        if (timeBetweenShts <= 0) {
            ShootBullet();
        }
        timeBetweenShts -= Time.deltaTime;
    }
    
    protected abstract void NavigateWeapon();
    
    protected abstract void ShootBullet();

}
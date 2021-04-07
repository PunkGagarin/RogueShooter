using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Gun : MonoBehaviour {

    public float offset = 0f;
    public Bullet bullet;

    public Transform firePoint;

    public Joystick attackJoystick;
    private float timeBetweenShts;
    public float startTimeBetweenShts = 0.25f;

    private Player player;
    private Vector3 difference;
    private float rotZ;

    private void Start() {
        player = Player.GetInstance;
        if (player.controlType == Player.ControlType.PC)
            attackJoystick.gameObject.SetActive(false);
    }

    private void Update() {
        if (player.controlType == Player.ControlType.PC) {
            difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            rotZ = Mathf.Atan2(difference.x, difference.y) * -Mathf.Rad2Deg;
        }
        else if (player.controlType == Player.ControlType.Android &&
                 (Math.Abs(attackJoystick.Horizontal) > 0.3f || Math.Abs(attackJoystick.Vertical) > 0.3f)) {
            rotZ = Mathf.Atan2(attackJoystick.Horizontal, attackJoystick.Vertical) * -Mathf.Rad2Deg;
        }

        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (timeBetweenShts <= 0) {
            if (Input.GetMouseButton(0) && player.controlType == Player.ControlType.PC) {
                Shoot();
            }
            else if (player.controlType == Player.ControlType.Android &&
                     (attackJoystick.Horizontal != 0 || attackJoystick.Vertical != 0)) {
                Shoot();
            }
        }
        timeBetweenShts -= Time.deltaTime;
    }

    private void Shoot() {
        Instantiate(bullet.gameObject, firePoint.position, transform.rotation);
        timeBetweenShts = startTimeBetweenShts;
        //firedBullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bullet.speed, ForceMode2D.Impulse);
    }
}
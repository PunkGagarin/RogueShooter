using System;
using UnityEngine;

public class PlayerGun : AbstractGun {

    public Joystick attackJoystick;

    private new void Start() {
        base.Start();
        if (player.controlType == Player.ControlType.PC)
            attackJoystick.gameObject.SetActive(false);
    }

    protected override void NavigateWeapon() {
        if (player.controlType == Player.ControlType.PC) {
            direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            rotZ = Mathf.Atan2(direction.x, direction.y) * -Mathf.Rad2Deg;
        }
        else if (player.controlType == Player.ControlType.Android &&
                 (Math.Abs(attackJoystick.Horizontal) > 0.3f || Math.Abs(attackJoystick.Vertical) > 0.3f)) {
            rotZ = Mathf.Atan2(attackJoystick.Horizontal, attackJoystick.Vertical) * -Mathf.Rad2Deg;
        }
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
    }

    private bool PcAttack() {
        return Input.GetMouseButton(0) && player.controlType == Player.ControlType.PC;
    }

    private bool AndroidAttack() {
        return player.controlType == Player.ControlType.Android &&
               (attackJoystick.Horizontal != 0 || attackJoystick.Vertical != 0);
    }

    protected override void ShootBullet() {
        if (!PcAttack() && !AndroidAttack()) 
            return;
        Instantiate(bullet.gameObject, firePoint.position, transform.rotation);
        timeBetweenShts = startTimeBetweenShts;
        //firedBullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bullet.speed, ForceMode2D.Impulse);
    }
}
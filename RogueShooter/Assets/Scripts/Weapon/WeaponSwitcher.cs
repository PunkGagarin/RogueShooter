using UnityEngine;

public class WeaponSwitcher : MonoBehaviour {

    public GameObject gun;
    public GameObject meleeWeapon;

    public void Update() {
        SwitchWeapon();
    }

    private void SwitchWeapon()
    {
        if (!Input.GetKeyDown(KeyCode.Q)) 
            return;
        if (gun.activeSelf) {
            gun.SetActive(false);
            meleeWeapon.SetActive(true);
        } else if (meleeWeapon.activeSelf) {
            meleeWeapon.SetActive(false);
            gun.SetActive(true);
        }
    }
}

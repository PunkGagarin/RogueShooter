using UnityEngine;

public class WeaponSwitcher : MonoBehaviour {

    public GameObject rangeWeapon;
    public GameObject meleeWeapon;

    public void Update() {
        SwitchWeapon();
    }

    private void SwitchWeapon() {
        if (!Input.GetKeyDown(KeyCode.Q))
            return;
        if (rangeWeapon.activeInHierarchy) {
            rangeWeapon.SetActive(false);
            meleeWeapon.SetActive(true);
            Debug.Log("Переключили оружие на " + meleeWeapon);
        }
        else if (meleeWeapon.activeInHierarchy) {
            meleeWeapon.SetActive(false);
            rangeWeapon.SetActive(true);
        }
    }
}
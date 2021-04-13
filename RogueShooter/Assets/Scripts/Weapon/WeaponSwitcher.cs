using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour {

    public List<GameObject> weaponObjects;
    private int currentWeaponIndex = 0;

    public GameObject rangeWeapon;
    public GameObject meleeWeapon;

    public void Start() {
        weaponObjects ??= new List<GameObject>();
        FindCurrentWeaponIndex();
    }

    public void Update() {
        SwitchWeapon();
    }

    private void SwitchWeapon() {
        if (!Input.GetKeyDown(KeyCode.Q))
            return;

        TurnOffCurrentWeapon(weaponObjects[currentWeaponIndex]);

        int nextWeaponIndex = currentWeaponIndex + 1;
        if (nextWeaponIndex >= weaponObjects.Count)
            nextWeaponIndex = nextWeaponIndex % weaponObjects.Count;

        Debug.Log("Current index= " + currentWeaponIndex + "Next weapon index= " + nextWeaponIndex);

        TurnOnNextWeapon(weaponObjects[nextWeaponIndex]);
        currentWeaponIndex = nextWeaponIndex;
    }

    private void FindCurrentWeaponIndex() {
        for (var index = 0; index < weaponObjects.Count; index++) {
            var weapon = weaponObjects[index];
            if (weapon.activeInHierarchy)
                currentWeaponIndex = index;
        }
    }

    private void TurnOnNextWeapon(GameObject weaponObject) {
        Debug.Log(" Trying to turn off " + weaponObject);
        weaponObject.SetActive(true);
    }

    private void TurnOffCurrentWeapon(GameObject weaponObject) {
        Debug.Log(" Trying to turn on " + weaponObject);
        weaponObject.SetActive(false);
    }
}
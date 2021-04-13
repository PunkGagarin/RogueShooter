using System;
using UnityEngine;

public class WeaponPickUp : PickUpItem {

    public GameObject weapon;

    private WeaponSwitcher weaponSwitcher;
    private void Start() {
        weaponSwitcher = FindObjectOfType<WeaponSwitcher>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.TryGetComponent(out Player player)) 
            return;
        
        weaponSwitcher.weaponObjects.Add(weapon);
        Destroy(gameObject);
    }
}

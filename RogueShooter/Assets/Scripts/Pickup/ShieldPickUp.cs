using UnityEngine;

public class ShieldPickUp : PickUpItem {

    private ShieldUI shieldUI;

    private void Start() {
        shieldUI = ShieldUI.GetInstance;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.TryGetComponent(out Player player)) 
            return;
        player.shield.SetActive(true);
        SetShieldUiActive();
        Instantiate(pickUpEffect, effectPoint.position, Quaternion.identity);
        Destroy(gameObject);
    }
    
    private void SetShieldUiActive() {
        shieldUI.SetUiActive();
        shieldUI.isShieldActive = true;
        shieldUI.ResetShield();
    }
}
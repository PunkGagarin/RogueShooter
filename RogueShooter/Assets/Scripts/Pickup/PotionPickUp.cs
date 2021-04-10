using UnityEngine;

public class PotionPickUp : PickUpItem {

    public float healPoint;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.TryGetComponent(out Player player)) 
            return;
        player.ChangeHealth(-healPoint);
        Instantiate(pickUpEffect, effectPoint.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
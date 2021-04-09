using UnityEngine;

public class PickedUp : MonoBehaviour {

    public float healPoint;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.TryGetComponent(out Player player)) 
            return;
        player.ChangeHealth(-healPoint);
        Destroy(gameObject);
    }
}
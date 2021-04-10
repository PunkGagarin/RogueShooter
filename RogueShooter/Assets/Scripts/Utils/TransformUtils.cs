using UnityEngine;

public static class TransformUtils {

    public static void Flip(Transform transform, ref bool isFacingRight) {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
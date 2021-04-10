using UnityEngine;
using UnityEngine.UI;

public class HpBarUI : MonoBehaviour {
    public Text text;

    #region Singleton
    public static HpBarUI GetInstance { get; private set; }

    private void Awake() {
        if (GetInstance == null) {
            GetInstance = this;
        }
    }
    #endregion

    public void SetHealth(float health) {
        text.text = "HP :" + health;
    }
}

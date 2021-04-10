using UnityEngine;
using UnityEngine.UI;

public class ShieldUI : MonoBehaviour {

    public float duration;
    public GameObject shieldUi;

    [HideInInspector] public bool isShieldActive;

    public Image shieldImage;
    private Player player;

    #region Singleton

    public static ShieldUI GetInstance { get; private set; }

    private void Awake() {
        if (GetInstance == null) {
            GetInstance = this;
        }
    }

    #endregion

    private void Start() {
        player = Player.GetInstance;
    }

    private void Update() {
        if (isShieldActive) {
            shieldImage.fillAmount -= 1 / duration * Time.deltaTime;
        }
        if (shieldImage.fillAmount <= 0) {
            CleanUp();
        }
    }

    private void CleanUp() {
        shieldImage.fillAmount = 1f;
        isShieldActive = false;
        player.shield.SetActive(false);
        shieldUi.SetActive(false);
    }

    public void SetUiActive() {
        shieldUi.SetActive(true);
    }

    public void ReduceAmountBy(float amount) {
        shieldImage.fillAmount -= amount/10;
    }

    public void ResetDuration() {
        duration = 1f;
    }
}
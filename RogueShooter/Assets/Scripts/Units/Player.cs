using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float health = 10f;

    //TODO: вынести в контроллер ?
    public ControlType controlType;
    public Joystick movementJoystick;
    public Joystick attackJoystick;
    public float speed;

    private Rigidbody2D rb;
    private Animator animator;

    private HpBarUI hpBarUI;
    private ShieldUI shieldUI;

    [HideInInspector] public GameObject shield;

    #region Deligate for MeleeAttack
    //TODO: вынести в отдельный класс на игроке?
    private PlayerMeleeAttack meleeAttack;

    public void OnAttack() {
        meleeAttack.OnAttack();
    }

    #endregion

    public enum ControlType {
        PC,
        Android
    }

    private Vector2 moveVelocity;
    private bool isFacingRight = true;

    #region Singleton

    public static Player GetInstance { get; private set; }

    public void Awake() {
        if (GetInstance == null) {
            GetInstance = this;
        }
    }

    #endregion

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        //TODO: если у нас несколько инстансов этого скрипта, можем то при обращении к родителю или gameobject что получим?
        meleeAttack = FindObjectOfType<PlayerMeleeAttack>();
        hpBarUI = HpBarUI.GetInstance;
        shieldUI = ShieldUI.GetInstance;
        
        hpBarUI.SetHealth(health);
    }

    private void Update() {
        UpdateControlSettings();

        moveVelocity = moveVelocity.normalized * speed;

        PlayMovementAnimation();

        CheckForFlip();
    }

    private void UpdateControlSettings() {
        if (Input.GetKeyDown(KeyCode.Y))
            controlType = controlType == ControlType.Android ? ControlType.PC : ControlType.Android;

        if (controlType == ControlType.PC) {
            moveVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            //TODO: move to settings
            if (movementJoystick.gameObject.activeSelf)
                movementJoystick.gameObject.SetActive(false);
            if (attackJoystick.gameObject.activeSelf)
                attackJoystick.gameObject.SetActive(false);
        }
        else if (controlType == ControlType.Android) {
            moveVelocity = new Vector2(movementJoystick.Horizontal, movementJoystick.Vertical);
            if (!movementJoystick.gameObject.activeSelf)
                movementJoystick.gameObject.SetActive(true);
            if (!attackJoystick.gameObject.activeSelf)
                attackJoystick.gameObject.SetActive(true);
        }
    }

    private void PlayMovementAnimation() {
        if (moveVelocity.x == 0 && moveVelocity.y == 0) {
            animator.SetBool("isRunning", false);
        }
        else {
            animator.SetBool("isRunning", true);
        }
    }

    private void CheckForFlip() {
        if (!isFacingRight && moveVelocity.x >= 0) {
            TransformUtils.Flip(transform, ref isFacingRight);
        }
        else if (isFacingRight && moveVelocity.x < 0) {
            TransformUtils.Flip(transform, ref isFacingRight);
        }
    }

    public void ChangeHealth(float healthDifference) {
        shieldUI.ReduceAmountBy(1);
        if (shield.activeInHierarchy && healthDifference > 0)
            return;

        health = Mathf.Clamp(health -= healthDifference, 0, health);
        hpBarUI.SetHealth(health);
        if (health <= 0) {
            Die();
        }
    }

    private void Die() {
        Debug.Log("Our player jsut died!!!");
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
}
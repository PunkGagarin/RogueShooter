using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public float speed;
    public float health = 10f;

    public ControlType controlType;

    public Joystick movementJoystick;
    public Joystick attackJoystick;

    private Rigidbody2D rb;
    private Animator animator;

    public enum ControlType {
        PC,
        Android
    }

    private Vector2 moveVelocity;
    private bool isFacingRight = true;

    #region Singleton

    private static Player instance;

    public static Player GetInstance => instance;

    public void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    #endregion

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        UpdateControlSettings();
        moveVelocity = moveVelocity.normalized * speed;

        if (moveVelocity.x == 0 && moveVelocity.y == 0) {
            animator.SetBool("isRunning", false);
        }
        else {
            animator.SetBool("isRunning", true);
        }

        if (!isFacingRight && moveVelocity.x >= 0) {
            TransformUtils.Flip(transform, ref isFacingRight);
        }
        else if (isFacingRight && moveVelocity.x < 0) {
            TransformUtils.Flip(transform, ref isFacingRight);
        }
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

    public void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            Die();
        }
    }

    public void Die() {
        Debug.Log("Our player jsut died!!!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
}
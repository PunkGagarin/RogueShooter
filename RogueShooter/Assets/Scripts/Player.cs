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
         health = Mathf.Clamp(health -= healthDifference, 0, health) ;
        if (health <= 0) {
            Die();
        }
    }

    private void Die() {
        Debug.Log("Our player jsut died!!!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
}
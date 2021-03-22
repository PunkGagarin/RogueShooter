using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public float speed;
    public float health = 10f;


    public ControlType controlType;
    
    public Joystick joystick;

    private Rigidbody2D rb;
    private Animator animator;

    public enum ControlType { PC, Joystick }

    private Vector2 moveVelocity;
    private bool isFacingRight = true;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (controlType == ControlType.PC) {
            moveVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            //TODO: move to settings
            if (joystick.gameObject.activeSelf)
                joystick.gameObject.SetActive(false);
        } else if (controlType == ControlType.Joystick) {
            moveVelocity = new Vector2(joystick.Horizontal, joystick.Vertical);
            if (!joystick.gameObject.activeSelf)
                joystick.gameObject.SetActive(true);
        }
        moveVelocity = moveVelocity.normalized * speed;

        if (moveVelocity.x == 0 && moveVelocity.y == 0) {
            animator.SetBool("isRunning", false);
        } else {
            animator.SetBool("isRunning", true);
        }

        if (!isFacingRight && moveVelocity.x >= 0) {
            Flip();
        } else if (isFacingRight && moveVelocity.x < 0) {
            Flip();
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

    private void Flip() {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}

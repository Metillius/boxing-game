using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Punch Settings")]
    public Animator handRAnimator;
    public Animator handLAnimator;
    public string punchTriggerName = "Punch";
    public float punchCooldown = 0.08f;
    private float nextPunchTime = 0f;
    private bool nextIsRight = true; 
    private float lastPunchTime = 0f;
    public float comboResetTime = 0.9f;


    private Rigidbody2D rb;
    private float moveDirection = 0f;
    private bool isGrounded = false;
    private bool jumpRequested = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue value)
    {
        moveDirection = value.Get<float>();
    }

    void OnJump()
    {
        Debug.Log("JUMP INPUT RECEIVED — isGrounded: " + isGrounded);
        if (isGrounded)
        {
            jumpRequested = true;
            Debug.Log("JUMP EXECUTED");
        }
    }

    void OnPunch()
    {
        Debug.Log("PUNCH INPUT GELDİ");

        if (Time.time < nextPunchTime) return;
        nextPunchTime = Time.time + punchCooldown;

        // combo reset 
        if (Time.time - lastPunchTime > comboResetTime)
        {
            nextIsRight = true;
        }

        bool rightPunch = nextIsRight;
        nextIsRight = !nextIsRight;

        lastPunchTime = Time.time;

        if (rightPunch && handRAnimator != null)
        {
            handRAnimator.ResetTrigger(punchTriggerName);
            handRAnimator.SetTrigger(punchTriggerName);
        }
        else if (handLAnimator != null)
        {
            handLAnimator.ResetTrigger(punchTriggerName);
            handLAnimator.SetTrigger(punchTriggerName);
        }
    }

    void FixedUpdate()
    {
        if (jumpRequested)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
            jumpRequested = false;
        }

        rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);

        if (moveDirection < 0)
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        else if (moveDirection > 0)
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }
}
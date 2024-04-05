using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float groundDistance = 0.1f;
    [SerializeField] private float jumpTime = 1f;

    private float jumpTimer;

    private bool isJumping;
    private float moveInput;
    private bool movingRight = true;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float gravityMultiplierVal = 2f;
    private float originalGravity;
    public bool variableJump = false;
    public bool gravityMultiplier = false;
    public Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpTimer = jumpTime;
        originalGravity = rb.gravityScale;
    }

    private void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if(rb.velocity.x != 0)
            animator.SetBool("isMoving", true);
        else
            animator.SetBool("isMoving", false);

        Flip();
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            jumpTimer = jumpTime;
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
        if (variableJump)
        {
            if (Input.GetButton("Jump") && isJumping)
            {
                if (jumpTimer > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                    jumpTimer -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }
            if (Input.GetButtonUp("Jump"))
            {
                isJumping = false;
            }
        }
        if(gravityMultiplier)
        {
            if(rb.velocity.y < 0)
                rb.gravityScale = gravityMultiplierVal * originalGravity;
            else
                rb.gravityScale = originalGravity;
        }

        animator.SetFloat("moveY", rb.velocity.y);
    }

    public bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, groundLayer);
        return hit.collider != null;
    }
    private void Flip()
    {
        if(movingRight && rb.velocity.x < 0 || !movingRight && rb.velocity.x >0)
        {
            movingRight = !movingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}

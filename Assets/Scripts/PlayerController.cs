using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    [Tooltip("Ground check: Empty GameObject pod hráčem")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 1) Pohyb vlevo/vpravo
        float h = Input.GetAxisRaw("Horizontal"); // A/D nebo šipky
        rb.velocity = new Vector2(h * moveSpeed, rb.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(h));

        // 2) Flip sprite podle směru
        if (h > 0.1f) transform.localScale = Vector3.one;
        else if (h < -0.1f) transform.localScale = new Vector3(-1, 1, 1);

        // 3) Skok
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        anim.SetBool("Grounded", isGrounded);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.SetTrigger("Jump");
        }
    }

    // (volitelné) vizualizace v Scene view
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
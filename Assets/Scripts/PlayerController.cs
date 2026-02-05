using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    private Animator animator;
    private bool isGrounded;
    private bool isFacingRight = true;
    private Rigidbody2D rb;
    private GameManager gameManager;
    private AudioManager audioManager;
    [SerializeField] private ProjectileBehaviour projectilePrefab;
    [SerializeField] private Transform launchOffSet;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float nextFireTime = 0f;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindAnyObjectByType<GameManager>();
        audioManager = FindAnyObjectByType<AudioManager>();
    }
    void Update()
    {
        if(gameManager.IsGameOver()) return;
        HandleMovement();
        HandleJump();
        UpdateAnimation();
        Shoot();
    }
    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        if (moveInput > 0)
        {
            isFacingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput < 0)
        {
            isFacingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            audioManager.PlayJumpSound();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    private void UpdateAnimation()
    {
        bool isRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        bool isJumping = !isGrounded;
        animator.SetBool("IsRunning", isRunning);
        animator.SetBool("IsJumping", isJumping);
    }
    private void Shoot()
    {
        if(Input.GetKeyDown(KeyCode.J) && Time.time >= nextFireTime)
        {
            audioManager.PlayFireSound();
            nextFireTime = Time.time + fireRate;
            ProjectileBehaviour bullet =
                Instantiate(projectilePrefab, launchOffSet.position, Quaternion.identity);

            bullet.SetDirection(isFacingRight);
        }
    }
}

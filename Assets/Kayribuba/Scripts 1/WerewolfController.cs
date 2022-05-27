using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WerewolfController : MonoBehaviour
{
    public bool isFacingLeft { get; private set; }
    public bool isGrounded { get; private set; }

    [SerializeField] float playerSpeed = 15f;
    [SerializeField] float jumpForce = 25f;
    [SerializeField] float fallingAccelerationIntensity = 5f;
    [SerializeField] float playerAttackDistance = 1f;
    [SerializeField] float attackRadius = .7f;
    [SerializeField] LayerMask EnemyLayer;
    [SerializeField] LayerMask GroundLayers;
    [SerializeField] Transform GroundCheck;
    [SerializeField] Transform attackPoint;
    [SerializeField] Transform corePoint;
    
    Rigidbody2D rb;
    Animator animator;

    float mx;
    float defaultGravityScale;
    float coyoteTime;
    float coyoteJump = -1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        defaultGravityScale = rb.gravityScale;
    }
    void Update()
    {
        mx = Input.GetAxis("Horizontal");
        animator.SetFloat("Movement", Mathf.Abs(mx));

        CheckFlipNeed();

        GroundCheckMethod();
        JumpCheck();
        Attack();
    }

    private void GroundCheckMethod()
    {
        if (Physics2D.OverlapCircle(GroundCheck.position, 0.37f, GroundLayers))
        {
            isGrounded = true;
            coyoteTime = Time.time + 0.05f;
            animator.SetTrigger("Grounded");
        }
        else if (coyoteTime >= Time.time)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    private void JumpCheck()
    {
        if(Input.GetButtonDown("Jump"))
            coyoteJump = Time.time + 0.1f;

        if (coyoteJump >= Time.time && isGrounded)
        {
            coyoteJump = -1;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("Jumped");
        }
        else if (rb.velocity.y > 0 && Input.GetButtonUp("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);
        }

        SerialExperimentsLain();
    }
    private void SerialExperimentsLain()
    {
        if (rb.velocity.y < 0)
            rb.gravityScale = defaultGravityScale + fallingAccelerationIntensity;
        else
            rb.gravityScale = defaultGravityScale;
    }
    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Mathf.Abs(mx) > 0.01 && IsMouseBehindTheCharacter())
                return;

            SetAttackPosition();

            animator.SetTrigger("Attack");
            attackPoint.GetComponent<Animator>().SetTrigger("Slash");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, EnemyLayer);
            foreach (Collider2D hit in hitEnemies)
            {
                //atak :)
            }
            hitEnemies = null;
        }
    }
    void SetAttackPosition()
    {
        Vector2 desiredPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (!isFacingLeft && desiredPosition.x < corePoint.position.x)
            Flip();
        else if (isFacingLeft && desiredPosition.x > corePoint.position.x)
            Flip();

        Vector2 direction = desiredPosition - new Vector2(corePoint.position.x, corePoint.position.y);
        direction.Normalize();
        desiredPosition = new Vector2(corePoint.position.x, corePoint.position.y) + direction * playerAttackDistance;

        attackPoint.position = new Vector3(desiredPosition.x, desiredPosition.y, 0);

        Vector2 rotationVector = attackPoint.position - corePoint.position;
        if (!isFacingLeft)
            attackPoint.rotation = Quaternion.Euler(0, 0, Vector2.Angle(rotationVector, Vector2.down) - 90);
        else
            attackPoint.rotation = Quaternion.Euler(0, 0, Vector2.Angle(rotationVector, Vector2.up) - 90);
    }
    bool IsMouseBehindTheCharacter()
    {
        if (!isFacingLeft && Camera.main.ScreenToWorldPoint(Input.mousePosition).x < corePoint.position.x)
            return true;
        else if (isFacingLeft && Camera.main.ScreenToWorldPoint(Input.mousePosition).x > corePoint.position.x)
            return true;
        else
            return false;
    }
    private void CheckFlipNeed()
    {
        if ((mx > 0 && isFacingLeft) || (mx < 0 && !isFacingLeft))
            Flip();
    }
    void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        isFacingLeft = !isFacingLeft;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundCheck.position, 0.37f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}

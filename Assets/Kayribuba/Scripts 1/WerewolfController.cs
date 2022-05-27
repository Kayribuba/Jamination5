using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WerewolfController : MonoBehaviour
{
    public bool isFacingLeft { get; private set; }
    public bool isGrounded { get; private set; }

    [SerializeField] float playerSpeed = 15f;
    [SerializeField] float jumpForce = 25f;
    [SerializeField] float fallingAccelerationIntensity = 5f;
    [SerializeField] float playerAttackDistance = 1f;
    [SerializeField] float attackRadius = .7f;
    [SerializeField] float attackDuration = .2f;
    [SerializeField] float attackCoolDown = .2f;
    [SerializeField] LayerMask EnemyLayer;
    [SerializeField] LayerMask GroundLayers;
    [SerializeField] Transform GroundCheck;
    [SerializeField] Transform attackPoint;
    [SerializeField] Transform corePoint;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] TextMeshProUGUI EditText;
    
    Rigidbody2D rb;
    Animator animator;

    float mx;
    float defaultGravityScale;
    float coyoteTime;
    float coyoteJump = -1;
    float targetAttackTime = float.MinValue;
    float attackUntill = float.MinValue;

    int score = 0;
    bool editorModeIsOn;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        defaultGravityScale = rb.gravityScale;
    }
    void Update()
    {
        mx = Input.GetAxisRaw("Horizontal");
        //animator.SetFloat("Movement", Mathf.Abs(mx));

        CheckFlipNeed();

        rb.velocity = new Vector2(mx * playerSpeed, rb.velocity.y);

        GroundCheckMethod();
        JumpCheck();
        Attack();
        DamageThings();

        EditorMode();
    }

    void EditorMode()
    {
        if (Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.M) && !editorModeIsOn)
        {
            GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
            editorModeIsOn = true;
            EditText.gameObject.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.R) && editorModeIsOn)
        {
            FindObjectOfType<GameManagerScript>().ReloadLevel();
        }
    }

    private void GroundCheckMethod()
    {
        if (Physics2D.OverlapCircle(GroundCheck.position, 0.37f, GroundLayers))
        {
            isGrounded = true;
            coyoteTime = Time.time + 0.05f;
            //animator.SetTrigger("Grounded");
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
            //animator.SetTrigger("Jumped");
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
        if (Input.GetMouseButtonDown(0) && targetAttackTime <= Time.time)
        {
            //animator.SetTrigger("Attack");
            //attackPoint.GetComponent<Animator>().SetTrigger("Slash");

            targetAttackTime = Time.time + attackCoolDown;
            attackUntill = Time.time + attackDuration;
        }
    }
    void DamageThings()
    {
        if (attackUntill <= Time.time)
            return;

        //Debug.Log("ATTAAACK");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, EnemyLayer);
        foreach (Collider2D enemyCol in hitEnemies)
        {
            if (enemyCol.GetComponent<PreyScript>() == null)
            {
                Debug.Log("Enemy has no PreyScript :(");
                break;
            }

            enemyCol.GetComponent<PreyScript>().Die();

            if (enemyCol.CompareTag(Constants.SlimEnemyTag))
            {
                score++;
            }
            else if (enemyCol.CompareTag(Constants.ThickEnemyTag))
            {
                score += 2;
            }

            RefreshScore();
        }
        hitEnemies = null;
    }
    void RefreshScore()
    {
        ScoreText.text = score.ToString();
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

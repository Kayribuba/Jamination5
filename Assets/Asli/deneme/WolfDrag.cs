using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfDrag : MonoBehaviour
{
    public GameObject magenta;
    WerewolfController wwcont;
    public Transform groundCheckTransform;
    public Transform wolfAttackPoint;
    public float wolfAttackRadius = 1f;
    public float groundCheckRadius = .26f;
    public LayerMask groundLayerMask;
    public LayerMask enemyLayerMask;
    public bool wasButtonDown;
    bool isGrounded = false;
    bool wasGrounded = false;
    public float rayLength = .5f;

    bool didFall = false;
    public float fallSpeed = 3f;


    public float power = 2f;
    public Rigidbody2D rb;

    public Vector2 minPower;
    public Vector2 maxPower;

    public float normalGravity;
    public float changedGravity = 10f;
    [Range(0, 100)] public float speedDecelerateOutOf100 = 20;
    public float dragRadius = 1;

    DragLine dl;
    Animator animatorWolf;

    Camera cam;
    Vector2 force;
    Vector3 startPoint;
    Vector3 endPoint;

    public bool isFacingLeft;

    private void Start()
    {
        cam = Camera.main;
        dl = GetComponent<DragLine>();
        animatorWolf = transform.Find("wolfSprite").GetComponent<Animator>();
        wwcont = GetComponent<WerewolfController>();
        normalGravity = rb.gravityScale;

    }

    private void Update()
    {
        Collider2D groundCollider = Physics2D.OverlapCircle(groundCheckTransform.position, groundCheckRadius, groundLayerMask);
        isGrounded = groundCollider != null ? true : false;

        if (groundCollider != null)
        {
            isGrounded = true;
            didFall = false;
            rb.gravityScale = normalGravity;
            animatorWolf.SetBool("Jumped", false);

            if (!wasGrounded)
            {
                rb.velocity = Vector2.zero;
                wasGrounded = true;
            }
        }
        else
        {
            isGrounded = false;
            wasGrounded = false;

        }

        if (isGrounded)
        {
            rb.drag = 3;
        }
        else
        {
            rb.drag = 0;
        }

        if (Input.GetMouseButtonDown(0) && isGrounded)
        {

            startPoint = gameObject.transform.position;
            startPoint.z = 15;
            wasButtonDown = true;
        }

        if (Input.GetMouseButton(0) && isGrounded && wasButtonDown)
        {
            Vector3 linePos = transform.position + (transform.position - cam.ScreenToWorldPoint(Input.mousePosition)).normalized *
                Vector2.Distance(transform.position, cam.ScreenToWorldPoint(Input.mousePosition));

            linePos.z = transform.position.z;

            if (Vector2.Distance(transform.position, linePos) > dragRadius)
            {
                linePos = transform.position + (transform.position - linePos).normalized * -dragRadius;
            }

            dl.RenderLine(gameObject.transform.position, linePos);
        }

        if (Input.GetMouseButtonUp(0) && isGrounded && wasButtonDown)
        {
            animatorWolf.SetBool("Jumped", true);

            endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            endPoint.z = 15;

            force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));
            rb.AddForce(force * power, ForceMode2D.Impulse);
            dl.EndLine();
        }

        if (Input.GetKeyUp(KeyCode.Space) && !didFall)
        {
            rb.velocity = new Vector2(rb.velocity.x / 100 * speedDecelerateOutOf100, rb.velocity.y - 5);
            rb.gravityScale = normalGravity + changedGravity;
            didFall = true;
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            if (isFacingLeft)
                Flip();
            WolfAttack();
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            if (!isFacingLeft)
                Flip();
            WolfAttack();
        }
    }
    void WolfAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(wolfAttackPoint.position, wolfAttackRadius, enemyLayerMask);
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
                wwcont.score += 5;
            }
            else if (enemyCol.CompareTag(Constants.ThickEnemyTag))
            {
                wwcont.score += 10;
            }
            wwcont.RefreshScore();
            wolfAttackPoint.GetComponent<Animator>().SetTrigger("Bite");
        }
        hitEnemies = null;
    }
    void Flip()
    {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            isFacingLeft = !isFacingLeft;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckTransform.position, groundCheckRadius);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(wolfAttackPoint.position, wolfAttackRadius);

    }
}

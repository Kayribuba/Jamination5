using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfDrag : MonoBehaviour
{
    public Transform groundCheckTransform;
    public Transform foot;
    public float groundCheckRadius = .5f;
    public LayerMask groundLayerMask;
    public bool isGrounded = false;
    public bool wasGrounded = false;
    public float rayLength = .5f;

    public bool didFall = false;
    public float fallSpeed = 100f;


    public float power = 10f;
    public Rigidbody2D rb;

    public Vector2 minPower;
    public Vector2 maxPower;

    public float normalGravity;
    public float changedGravity = 10f;

    DragLine dl;

    Camera cam;
    Vector2 force;
    Vector3 startPoint;
    Vector3 endPoint;

    private void Start()
    {
        cam = Camera.main;
        dl = GetComponent<DragLine>();

        normalGravity = rb.gravityScale;

    }

    private void Update()
    {
        //RaycastHit2D footHit = Physics2D.Raycast(foot.position, Vector2.down, rayLength, groundLayerMask);
        Collider2D groundCollider = Physics2D.OverlapCircle(groundCheckTransform.position, groundCheckRadius, groundLayerMask);
        isGrounded = groundCollider != null ? true : false;

        if (groundCollider != null)
        {
            isGrounded = true;
            didFall = false;
            rb.gravityScale = normalGravity;

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

            startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            startPoint.z = 15;
        }

        if (Input.GetMouseButton(0) && isGrounded)
        {
            Vector3 linePos = cam.ScreenToWorldPoint(Input.mousePosition);
            linePos.z = 15;
            dl.RenderLine(startPoint, linePos);
        }

        if (Input.GetMouseButtonUp(0) && isGrounded)
        {
            endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            endPoint.z = 15;

            force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));
            rb.AddForce(force * power, ForceMode2D.Impulse);
            dl.EndLine();
        }

        if (Input.GetKeyUp(KeyCode.Space) && !didFall)
        {

            rb.gravityScale = normalGravity + changedGravity;
            didFall = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckTransform.position, groundCheckRadius);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    enum PreyState { Idle, Walk, Flee}

public class PreyScript : MonoBehaviour
{
    [SerializeField] Transform edgeCheck;
    [SerializeField] LayerMask GroundLayers;
    [SerializeField] float speed = 3f;
    [SerializeField] float edgeCheckRadius = 1f;

    PreyState currentState = PreyState.Idle;
    float changeBehaviorAt = float.MaxValue;
    bool changeAgain = true;
    Rigidbody2D rb;
    Animator animator;
    //bool isSlim;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (Random.Range(0, 2) <= 1)
            currentState = PreyState.Idle;
        else
            currentState = PreyState.Walk;

        //isSlim = gameObject.tag == Constants.SlimEnemyTag;
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (changeAgain)
        {
            changeBehaviorAt = Random.Range(1, 5) + Time.time;
            changeAgain = false;
        }

        if(changeBehaviorAt <= Time.time)
        {
            if(currentState == PreyState.Idle)
            {
                currentState = PreyState.Walk;
            }
            else if (currentState == PreyState.Walk)
            {
                currentState = PreyState.Idle;
            }
            changeAgain = true;
        }

        DoStates();
    }

    public void Die()
    {
        animator.SetBool("Die", true);
        GetComponent<CapsuleCollider2D>().enabled = false;
        Destroy(rb);
        this.enabled = false;
    }
    void DoStates()
    {
        if(currentState == PreyState.Idle)
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("Walk", false);
        }
        else if (currentState == PreyState.Walk)
        {
            animator.SetBool("Walk", true);
            Vector2 forwardVector = GetForwardVector2();

            rb.velocity = new Vector2(forwardVector.x * speed, rb.velocity.y);

            if (!Physics2D.OverlapCircle(edgeCheck.position, edgeCheckRadius, GroundLayers))
            {
                rb.velocity = Vector2.zero;
                Flip();
            }
        }
    }
    Vector2 GetForwardVector2()
    {
        Vector2 forwardVector = edgeCheck.position - transform.position;
        forwardVector = new Vector2(forwardVector.x, 0);
        forwardVector.Normalize();

        return forwardVector;
    }
    void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(edgeCheck.position, edgeCheckRadius);
    }
}

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


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        //GE��C�
        transform.Find("Sprite").GetComponent<SpriteRenderer>().color = Color.green;
        GetComponent<CapsuleCollider2D>().enabled = false;
        Destroy(rb);
        this.enabled = false;
        //GE��C�
    }
    void DoStates()
    {
        if(currentState == PreyState.Idle)
        {

        }
        else if (currentState == PreyState.Walk)
        {
            rb.velocity = speed * transform.forward;
            if (Physics2D.OverlapCircle(edgeCheck.position, edgeCheckRadius, GroundLayers))
            {

            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(edgeCheck.position, edgeCheckRadius);
    }
}

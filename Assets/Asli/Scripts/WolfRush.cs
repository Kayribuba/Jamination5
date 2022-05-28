using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfRush : MonoBehaviour
{
    private bool isPressed;
    private float releaseDelay;

    private Rigidbody2D rb;
    private SpringJoint2D sj;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sj = GetComponent<SpringJoint2D>();

        releaseDelay = 1 / (sj.frequency * 4);
    }
    private void Update()
    {
        if(isPressed)
        {
            DragWolf();
        }
    }

    private void DragWolf()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb.position = mousePosition;
    }

    private void OnMouseDown()
    {
        isPressed = true;
        rb.isKinematic = true;
    }

    private void OnMouseUp()
    {
        isPressed = false;
        rb.isKinematic = false;
        StartCoroutine(Release());
    }

    private IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseDelay);
        sj.enabled = false;
    }
}

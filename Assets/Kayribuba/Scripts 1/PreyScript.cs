using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyScript : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void Die()
    {
        //GE��C�
        transform.Find("Sprite").GetComponent<SpriteRenderer>().color = Color.green;
        GetComponent<CapsuleCollider2D>().enabled = false;
        Destroy(GetComponent<Rigidbody2D>());
        this.enabled = false;
        //GE��C�
    }
}

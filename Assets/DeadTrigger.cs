using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadTrigger : MonoBehaviour
{
    public DeadScreen ds;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            ds.isDead = true;
            if(Camera.main.GetComponent<CameraScroll>() != null)
            {
                Camera.main.GetComponent<CameraScroll>().enabled = false;
            }
        }
    }
}

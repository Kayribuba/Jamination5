using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    public float acc = .01f;
    public float speedModifier = .1f;
    public float speed = .15f;

    void Update()
    {
        speed += acc * Time.deltaTime;

        transform.Translate(speed * speedModifier, 0, 0);
    }
}

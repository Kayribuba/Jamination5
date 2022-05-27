using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    public float acc = 1;
    public float speedModifier = .5f;
    public float speed = 5;

    void Update()
    {
        speed += acc * Time.deltaTime;

        transform.Translate(speed * speedModifier, 0, 0);
    }
}

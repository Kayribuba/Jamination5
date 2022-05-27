using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    public float acc = 1;
    public float speedModifier = .5f;
    public float speed = 0;

    float time = 0;

    void Update()
    {
        time = Time.time;

        speed = acc * time;

        transform.Translate(speed * speedModifier, 0, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeTrigger : MonoBehaviour
{
    public CameraShake cameraShake;
   
    public void Shake()
    {
        StartCoroutine(cameraShake.Shake(.1f, .1f));
    }
}

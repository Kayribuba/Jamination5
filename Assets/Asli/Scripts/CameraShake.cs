using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public GameObject cameraScroll;
    public float z = -10;
    public IEnumerator Shake(float duration, float magnitude)
    {
        

        Vector3 changedPos = cameraScroll.transform.position;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float xShake = Random.Range(-5f, 5f) * magnitude;
            float yShake = Random.Range(-5f, 5f) * magnitude;

            transform.localPosition = new Vector3(xShake, yShake, z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = changedPos; ;

    }
}

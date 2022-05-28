using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float z = -10;
    public IEnumerator Shake(float duration, float magnitude)
    {
        float x = 0;
        float y = 0;

        Vector3 originalPos = new Vector3(x, y, z);

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float xShake = Random.Range(-1f, 1f) * magnitude;
            float yShake = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(xShake, yShake, z);

            elapsed += Time.deltaTime;

            yield return null;

        }

        transform.localPosition = originalPos;

    }
}

using UnityEngine;
using System.Collections;

public class CamEffects : MonoBehaviour
{
    public float shakeAmount;
    public float shakeDuration = 0.5f;

    float shakeTimer = 0f;

    public void ShakeCam()
    {
        shakeTimer = Time.timeSinceLevelLoad + shakeDuration;
    }

    void Update()
    {
        if (shakeTimer >= Time.timeSinceLevelLoad)
        {
            transform.position += Random.insideUnitSphere * shakeAmount;
        }
    }
}

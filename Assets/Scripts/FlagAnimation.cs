using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagAnimation : MonoBehaviour
{
    public float amplitude = 2f;
    public float speed = 2f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        float newY = initialPosition.y + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);
    }
}

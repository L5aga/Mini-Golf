using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBall : MonoBehaviour
{
    public Transform ball;
    public float followSpeed = 0.1f;
    public Vector3 offset = new Vector3(0, 5, -10);

    private Vector3 smoothPosition;

    void LateUpdate()
    {
        if (ball != null)
        {
            Vector3 targetPosition = ball.position + offset;
            smoothPosition = Vector3.Lerp(smoothPosition, targetPosition, followSpeed);
            transform.position = smoothPosition;
            transform.rotation = Quaternion.Euler(30, 0, 0);
        }
    }
}
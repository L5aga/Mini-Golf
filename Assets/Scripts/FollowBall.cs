using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBall : MonoBehaviour
{
    public Transform Ball;
    public float followSpeed = 0.1f;
    public float rotationSpeed = 50f;
    public Vector3 offset = new Vector3(0, 5, -10);

    private Vector3 smoothPosition;
    private float currentRotationY;

    void Start()
    {
        currentRotationY = transform.eulerAngles.y;
    }

    void LateUpdate()
    {
        if (Ball != null)
        {
            Vector3 targetPosition = Ball.position + offset;
            smoothPosition = Vector3.Lerp(smoothPosition, targetPosition, followSpeed);
            transform.position = smoothPosition;

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                currentRotationY -= rotationSpeed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                currentRotationY += rotationSpeed * Time.deltaTime;
            }

            Vector3 direction = (transform.position - Ball.position).normalized;
            Quaternion rotation = Quaternion.Euler(0, currentRotationY, 0);
            transform.position = Ball.position + rotation * (transform.position - Ball.position);
            transform.LookAt(Ball.position);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBall : MonoBehaviour
{
    public float maxForce = 20f;    
    public float decelerationRate = 0.1f;
    private Rigidbody rb;
    private Vector3 hitDirection;
    private bool isCharging = false;
    private float currentForce;
    public float stopThreshold = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude > stopThreshold) 
        {
            return;
        }
        else if (rb.velocity.magnitude <= stopThreshold && rb.velocity.magnitude > 0.1f) 
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, decelerationRate * Time.fixedDeltaTime);
        }
        else if (rb.velocity.magnitude <= 0.1f)
        {
            rb.velocity = Vector3.zero;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            isCharging = true;
            currentForce = Mathf.Clamp(currentForce + Time.deltaTime * 10, 0, maxForce);
        }
        if (Input.GetMouseButtonUp(0) && isCharging)
        {
            isCharging = false;
            HitBall();
        }
    }

    void HitBall()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            hitDirection = (hit.point - transform.position).normalized;
            rb.AddForce(hitDirection * currentForce, ForceMode.Impulse);
            currentForce = 0;
        }
    }
}
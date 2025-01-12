using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GolfBall : MonoBehaviour
{
    public float maxForce = 20f;
    public float decelerationRate = 0.1f;
    public float stopThreshold = 1f;
    public TextMeshProUGUI forceText;
    public TextMeshProUGUI hitCounterText;

    private Rigidbody rb;
    private Vector3 hitDirection;
    private bool isCharging = false;
    private float currentForce;
    private int hitCount = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        UpdateForceText(0);
        UpdateHitCounter();
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
            UpdateForceText(currentForce / maxForce * 100);
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
            UpdateForceText(0);
            
            hitCount++;
            UpdateHitCounter();
        }
    }

    void UpdateForceText(float percentage)
    {
        if (forceText != null)
        {
            forceText.text = $"Force: {percentage:F0}%";
        }
    }

    void UpdateHitCounter()
    {
        if (hitCounterText != null)
        {
            hitCounterText.text = $"Hits: {hitCount}";
        }
    }
}
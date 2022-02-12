using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class driftCtrl : MonoBehaviour
{
    public float speed, steer, traction;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate() {
        rb.AddForce(transform.up * Input.GetAxis("Vertical") * speed, ForceMode2D.Impulse);
        rb.AddTorque(-Input.GetAxisRaw("Horizontal") * steer, ForceMode2D.Impulse);
        rb.velocity = Vector2.Lerp(rb.velocity, transform.up * rb.velocity.magnitude, traction);
    }
}

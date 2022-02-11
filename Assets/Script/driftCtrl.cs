using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class driftCtrl : MonoBehaviour
{
    public float speed, drag, steer, traction;
    float vel_coff;
    Vector2 accelForce;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        vel_coff = Input.GetAxis("Vertical") * speed;
        rb.AddForce(transform.up * vel_coff, ForceMode2D.Impulse);
        //transform.re
        //rb.AddTorque(Input.GetAxis("Horizontal") * rb.velocity.magnitude * vel_coff * steer, ForceMode2D.Impulse);

    }
}

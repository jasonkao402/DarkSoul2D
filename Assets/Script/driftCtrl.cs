using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct vehicleSpec
{
    public float speed, steer, traction;
}
public class driftCtrl : MonoBehaviour
{
    public vehicleSpec[] drftState = new vehicleSpec[2];
    public float driftExit, driftEnter;
    //public Transform trails;
    int state;
    Rigidbody2D rb;
    TrailRenderer[] trails;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trails = GetComponentsInChildren<TrailRenderer>();
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.LeftShift) || Vector2.Angle(rb.velocity, transform.up) > driftEnter)
        {
            state = 1;
            foreach (TrailRenderer t in trails)
            {
                t.emitting = true;
            }
        }
        else if(Vector2.Angle(rb.velocity, transform.up) < driftExit)
        {
            state = 0;
            foreach (TrailRenderer t in trails)
            {
                t.emitting = false;
            }
        }
        Debug.Log(Vector2.Angle(rb.velocity, transform.up));
    }
    void FixedUpdate()
    {
        rb.AddForce(transform.up * Input.GetAxis("Vertical") * drftState[state].speed, ForceMode2D.Impulse);
        rb.AddTorque(-Input.GetAxisRaw("Horizontal") * drftState[state].steer, ForceMode2D.Impulse);
        rb.velocity = Vector2.Lerp(rb.velocity, transform.up * rb.velocity.magnitude, drftState[state].traction);
    }
}

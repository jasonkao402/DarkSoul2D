using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct vehicleSpec
{
    public float accel, steer;
    public Vector2 traction, chargeBoost;
}
public class driftCtrl : MonoBehaviour
{
    public vehicleSpec vs;
    public float driftExit, driftEnter;
    [SerializeField]
    float driftAngle, driftTraction, nowCharge;
    int state;
    public Transform boostBar;
    public Rigidbody rb;
    TrailRenderer[] trails;
    void Start()
    {
        trails = GetComponentsInChildren<TrailRenderer>();
    }
    private void Update() {
        transform.position = rb.position;
        transform.rotation = rb.rotation;

        driftAngle = Vector3.Angle(rb.velocity, transform.forward);
        if(Input.GetKey(KeyCode.LeftShift) || driftAngle > driftEnter)
        {
            state = 1;
            foreach (TrailRenderer t in trails)
            {
                t.emitting = true;
            }
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift) || driftAngle < driftExit)
        {
            state = 0;
            foreach (TrailRenderer t in trails)
            {
                t.emitting = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartCoroutine(BoostState(boostBar));
        }
    }
    void FixedUpdate()
    {
        driftTraction = Mathf.Lerp(driftTraction, vs.traction[(int)state], 0.1f);
        rb.velocity = Vector3.Lerp(rb.velocity, transform.forward * rb.velocity.magnitude, driftTraction);
        rb.AddForce(transform.forward * Input.GetAxis("Vertical") * vs.accel, ForceMode.Impulse);
        rb.AddTorque(new Vector3(0, Input.GetAxis("Horizontal") * vs.steer * Mathf.Clamp01(rb.velocity.magnitude * 0.1f), 0), ForceMode.Impulse);
        if(state == 1)
        {
            nowCharge += vs.chargeBoost[0];
        }
        boostBar.localScale = new Vector3(nowCharge/vs.chargeBoost[1], 1, 1);
        //rb.AddForceAtPosition(transform.right * Input.GetAxis("Horizontal") * vs.steer * Mathf.Clamp(Vector3.Dot(rb.velocity, transform.forward) * 0.1f, -1, 1), transform.position+transform.forward, ForceMode.Impulse);
        //rb.AddTorque(-Input.GetAxis("Horizontal") * vs.steer * Mathf.Clamp01(rb.velocity.magnitude * 0.1f), ForceMode2D.Impulse);
        //rb.AddTorque(-Input.GetAxis("Horizontal") * vs.steer * Vector2.Dot(rb.velocity.normalized, transform.up), ForceMode2D.Impulse);
    }
    public IEnumerator BoostState(Transform tgt)
    {
        vs.accel *= 1.5f;
        Vector3 tempS = tgt.localScale, tgtscale = new Vector3(0, 1, 1);
        float n = 0;
        while(n < vs.chargeBoost[1])
        {
            tgt.localScale = Vector3.Lerp(tempS, tgtscale, n/vs.chargeBoost[1]);
            n += Time.deltaTime;
            yield return null;
        }
        tgt.localScale = tgtscale;
        vs.accel /= 1.5f;
        yield return null;
	}
}

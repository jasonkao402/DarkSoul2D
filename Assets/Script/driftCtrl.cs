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

        if(nowCharge == vs.chargeBoost[1] && Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartCoroutine(BoostState(boostBar));
        }
    }
    private void LateUpdate() {
        transform.position = rb.position;
        transform.rotation = rb.rotation;
    }
    void FixedUpdate()
    {
        driftTraction = Mathf.Lerp(driftTraction, vs.traction[(int)state], 0.1f);
        rb.velocity = Vector3.Lerp(rb.velocity, transform.forward * rb.velocity.magnitude, driftTraction);
        rb.AddForce(transform.forward * Input.GetAxis("Vertical") * vs.accel, ForceMode.Impulse);
        rb.AddTorque(new Vector3(0, Input.GetAxis("Horizontal") * vs.steer * Mathf.Clamp01(rb.velocity.magnitude * 0.1f), 0), ForceMode.Impulse);
        if(state == 1)
        {
            nowCharge = Mathf.Clamp(nowCharge + vs.chargeBoost[0] * Mathf.Abs(rb.angularVelocity.y) * Time.deltaTime, 0, vs.chargeBoost[1]) ;
            boostBar.localScale = new Vector3(nowCharge/vs.chargeBoost[1], 1, 1);
        }
    }
    public IEnumerator BoostState(Transform tgt)
    {
        float n = vs.chargeBoost[1];
        vs.accel *= 1.75f;
        while(n > 0)
        {
            tgt.localScale = new Vector3(n/vs.chargeBoost[1], 1, 1);
            n -= Time.deltaTime;
            nowCharge = 0;
            yield return null;
        }
        tgt.localScale = new Vector3(0, 1, 1);
        vs.accel /= 1.75f;
        yield return null;
	}
}

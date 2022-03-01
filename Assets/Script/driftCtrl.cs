using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct vehicleSpec
{
    public float accel, steer, driftExit, driftEnter;
    public Vector2 traction, chargeBoost;
}
public class driftCtrl : MonoBehaviour
{
    public vehicleSpec vs;
    public float pointerCoff;
    [SerializeField]
    float driftAngle, driftTraction, nowCharge, spdRead;
    int state;
    public Transform boostBar, speedometer;
    public ParticleSystem boostEff;
    public Rigidbody rb;
    public Text txt;
    TrailRenderer[] trails;
    void Start()
    {
        trails = GetComponentsInChildren<TrailRenderer>();
    }
    private void Update() {
        driftAngle = Vector3.Angle(rb.velocity, transform.forward);
        if(Input.GetKey(KeyCode.LeftShift) || driftAngle > vs.driftEnter)
        {
            state = 1;
            foreach (TrailRenderer t in trails)
            {
                t.emitting = true;
            }
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift) || driftAngle < vs.driftExit)
        {
            state = 0;
            foreach (TrailRenderer t in trails)
            {
                t.emitting = false;
            }
        }
        if(nowCharge == vs.chargeBoost[1] && Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartCoroutine(BoostState(boostBar, vs.chargeBoost[1]));
        }
    }
    private void LateUpdate() {
        transform.position = rb.position;
        transform.rotation = rb.rotation;
        spdRead = rb.velocity.magnitude + 1 - Mathf.Ceil(Mathf.Log(rb.velocity.magnitude+1, 2));
        speedometer.localEulerAngles = new Vector3(0, 0, -pointerCoff * spdRead);
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
        txt.text = Mathf.RoundToInt(rb.velocity.magnitude).ToString();
    }
    public IEnumerator BoostState(Transform tgt, float t)
    {
        float n = t;
        boostEff.Play();
        vs.accel *= 2f;
        while(n > 0)
        {
            tgt.localScale = new Vector3(n/t, 1, 1);
            n -= Time.deltaTime;
            nowCharge = 0;
            yield return null;
        }
        tgt.localScale = new Vector3(0, 1, 1);
        boostEff.Stop();
        vs.accel /= 2f;
        yield return null;
	}
}

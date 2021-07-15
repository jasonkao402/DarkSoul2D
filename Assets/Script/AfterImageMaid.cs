using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageMaid : MonoBehaviour
{
    public float dist;
    public string eff_ID;
    Vector3 lastPos;
    AftImgPool pooli;
    private void Start() {
        pooli = AftImgPool.Instance;
    }
    private void FixedUpdate() {
        if((transform.position - lastPos).sqrMagnitude > dist)
        {
            lastPos = transform.position;
            pooli.TakePool(eff_ID, transform.position, transform.rotation);
        }
    }
}

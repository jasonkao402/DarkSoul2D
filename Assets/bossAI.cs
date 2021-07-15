using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAI : MonoBehaviour
{
    public float dist, turn, topspeed;
    public Transform tgt;
    Vector3 lastPos, dir;
    AftImgPool pooli;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pooli = AftImgPool.Instance;
    }
    void FixedUpdate()
    {
        if((transform.position - lastPos).sqrMagnitude > dist)
        {
            lastPos = transform.position;
            pooli.TakePool("sword", transform.position, transform.rotation); 
        }
        MissileMove();
    }
    void MissileMove()
    {
        dir = tgt.position - transform.position;
        rb.AddForce(transform.up * topspeed);
        rb.angularVelocity = Vector3.Cross(transform.up, dir.normalized).z * turn;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAI : MonoBehaviour
{
    enum State{
        Missile,
        TriShot,
        Beam
    }
    public float dist, turn, topspeed, proj_maxCD, state_maxCD;
    float proj_nowCD, state_nowCD;
    public Transform tgt;
    Vector3 lastPos, dir;
    AftImgPool pooli;
    Rigidbody2D rb;
    State state;
    void Start()
    {
        state = State.TriShot;
        rb = GetComponent<Rigidbody2D>();
        pooli = AftImgPool.Instance;
    }
    private void FixedUpdate() {
        
        state_nowCD -= Time.deltaTime;
        if(state_nowCD < 0)
        {
            state_nowCD = state_maxCD;
            state = (State)Random.Range(0, 2);
        }
        
        switch (state)
        {
        case State.Missile:
            MissileLock();
            rb.AddForce(transform.right * topspeed);
            AfterImage();
            break;

        case State.TriShot:
            MissileLock();
            proj_nowCD -= Time.deltaTime;
            if(proj_nowCD < 0)
            {
                proj_nowCD = proj_maxCD;
                for(int i = 0; i < 3; i++)
                {
                    pooli.TakePool("swordProj", transform.position, transform.rotation*Quaternion.Euler(0, 0, i*5-5));
                }
            }
            break;

        default:
            break;
        }
    }
    void SpawnProj(Vector3 p, Quaternion q)
    {
        pooli.TakePool("swordProj", p, q);
    }
    void AfterImage(){
        if((transform.position - lastPos).sqrMagnitude > dist)
        {
            lastPos = transform.position;
            pooli.TakePool("swordImg", transform.position, transform.rotation);
        }
    }
    void MissileLock()
    {
        dir = tgt.position - transform.position;
        rb.angularVelocity = Vector3.Cross(transform.right, dir.normalized).z * turn;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAI : MonoBehaviour
{
    enum State{
        Missile,
        TriShot,
        Summon,
    }
    public float dist, turn, topspeed, proj_maxCD, state_maxCD;
    float proj_nowCD, state_nowCD;
    public Transform player;
    public static Transform playertgt {get; private set;}
    Vector3 lastPos, dir;
    AftImgPool pooli;
    Rigidbody2D rb;
    State state;
    void Start()
    {
        playertgt = player;
        state = State.TriShot;
        rb = GetComponent<Rigidbody2D>();
        pooli = AftImgPool.Instance;
    }
    private void FixedUpdate() {
        
        state_nowCD -= Time.deltaTime;
        if(state_nowCD < 0)
        {
            state_nowCD = state_maxCD;
            state = (State)Random.Range(0, 3);
        }
        
        switch (state)
        {
        case State.Missile:
            MissileLock(playertgt);
            rb.AddForce(transform.right * topspeed);
            AfterImage();
            break;

        case State.TriShot:
            MissileLock(playertgt);
            proj_nowCD -= Time.deltaTime;
            if(proj_nowCD < 0)
            {
                proj_nowCD = proj_maxCD;
                for(int i = 0; i < 3; i++)
                {
                    pooli.TakePool("swordProj_1", transform.position, transform.rotation*Quaternion.Euler(0, 0, i*8-8));
                }
            }
            break;
        case State.Summon:
            MissileLock(playertgt);
            proj_nowCD -= Time.deltaTime;
            if(proj_nowCD < 0)
            {
                proj_nowCD = proj_maxCD*2;
                Vector3 vtmp = new Vector3(15, 0, 0);
                Vector3 ptmp = new Vector3(0, playertgt.position.y, playertgt.position.x);
                Quaternion qtmp = Quaternion.Euler(0, 0, 0);
                //GameObject otemp;
                for(int i = 0; i < 6; i++)
                {
                    qtmp *= Quaternion.Euler(0, 0, -60);
                    pooli.TakePool("swordProj_2", playertgt.position + qtmp * vtmp, qtmp);
                }
            }
            break;
        default:
            break;
        }
    }
    void AfterImage(){
        if((transform.position - lastPos).sqrMagnitude > dist)
        {
            lastPos = transform.position;
            pooli.TakePool("swordImg", transform.position, transform.rotation);
        }
    }
    void MissileLock(Transform tgt)
    {
        dir = tgt.position - transform.position;
        rb.angularVelocity = Vector3.Cross(transform.right, dir.normalized).z * turn;
    }
}

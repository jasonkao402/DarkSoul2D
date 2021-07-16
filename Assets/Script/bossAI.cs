using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAI : MonoBehaviour
{
    public enum AtkPattern{
        Follow,
        TriShot,
        Summon,
        QuaDash,
        StarFlash,
        DiagFall_R,
        DiagFall_L,
        OrbitCannon,
        TouhouSpin,
        Idle,
        _Last,
    }
    public float dist, turn, topspeed, proj_maxCD, state_maxCD;
    float proj_nowCD, state_nowCD;
    public Transform player;
    public static Transform playertgt {get; private set;}
    Vector3 lastPos, dir, vtmp, destPos;
    Quaternion qtmp;
    AftImgPool pooli;
    Rigidbody2D rb;
    AtkPattern state;
    public List<AtkPattern> AtkPatternList;
    int nowState, altstate;
    void Start()
    {
        nowState = 0;
        playertgt = player;
        //state = AtkPattern.Idle;
        state_nowCD = state_maxCD;
        rb = GetComponent<Rigidbody2D>();
        pooli = AftImgPool.Instance;
        SoundManager.instance.PlayBGM(0);
    }
    private void FixedUpdate() {
        
        state_nowCD -= Time.deltaTime;
        proj_nowCD -= Time.deltaTime;

        if(state_nowCD < 0)
        {
            setState();
        }
        
        switch (state){
        case AtkPattern.Idle:
            break;
            
        case AtkPattern.Follow:
            MissileLock(playertgt.position);
            rb.AddForce(transform.right * topspeed);
            AfterImage();
            break;

        case AtkPattern.TriShot:
            MissileLock(playertgt.position);
            if(proj_nowCD < 0)
            {
                proj_nowCD = proj_maxCD;
                for(int i = 0; i < 3; i++)
                {
                    pooli.TakePool("swordProj_1", transform.position, transform.rotation*Quaternion.Euler(0, 0, i*8-8));
                    pooli.TakePool("hint", transform.position, transform.rotation*Quaternion.Euler(0, 0, i*8-8));
                }
            }
            break;

        case AtkPattern.Summon:
            MissileLock(playertgt.position);
            if(proj_nowCD < 0)
            {
                proj_nowCD = proj_maxCD*2;
                qtmp *= Quaternion.Euler(0, 0, 30);
                for(int i = 0; i < 6; i++)
                {
                    qtmp *= Quaternion.Euler(0, 0, -60);
                    pooli.TakePool("swordProj_2", playertgt.position + qtmp * vtmp, qtmp);
                }
            }
            break;

        case AtkPattern.StarFlash:
            if((transform.position - destPos).sqrMagnitude < 1f)
            {
                pooli.TakePool("swordProj_1", destPos, qtmp * Quaternion.Euler(0, 0, 180));
                qtmp *= Quaternion.Euler(0, 0, -144);
                destPos = playertgt.position + qtmp * vtmp;
                transform.right = destPos - transform.position;
            }
            transform.position = Vector3.Lerp(transform.position, destPos, 0.2f);
            AfterImage();
            break;

        case AtkPattern.DiagFall_R:
            if((transform.position - destPos).sqrMagnitude < 0.1f)
            {
                pooli.TakePool("swordProj_1", transform.position, qtmp);
                pooli.TakePool("hint", transform.position, qtmp);
                vtmp += new Vector3(3, 0, 0);
                destPos = playertgt.position + vtmp;
            }
            transform.position = Vector3.Lerp(transform.position, destPos, 0.3f);
            break;

        case AtkPattern.DiagFall_L:
            if((transform.position - destPos).sqrMagnitude < 0.1f)
            {
                pooli.TakePool("swordProj_1", transform.position, qtmp);
                pooli.TakePool("hint", transform.position, qtmp);
                vtmp += new Vector3(-3, 0, 0);
                destPos = playertgt.position + vtmp;
            }
            transform.position = Vector3.Lerp(transform.position, destPos, 0.3f);
            break;

        case AtkPattern.OrbitCannon:
            if((transform.position - destPos).sqrMagnitude < 0.5f)
            {
                transform.right = playertgt.position - transform.position;
                pooli.TakePool("swordProj_4", transform.position, transform.rotation);
                pooli.TakePool("hint", transform.position, transform.rotation);

                qtmp *= Quaternion.Euler(0, 0, -12);
                destPos = playertgt.position + qtmp * vtmp;
            }
            transform.position = Vector3.Lerp(transform.position, destPos, 0.333f);
            break;

        case AtkPattern.TouhouSpin:
            if(proj_nowCD < 0)
            {
                proj_nowCD = 0.08f;
                altstate += 2;
                transform.rotation *= Quaternion.Euler(0, 0, altstate);
                for(int i = 0; i < 3; i++)
                {
                    pooli.TakePool("swordProj_5", transform.position, transform.rotation * Quaternion.Euler(0, 0, i*120));
                    pooli.TakePool("hint", transform.position, transform.rotation * Quaternion.Euler(0, 0, i*120));
                }
            }
            break;

        default:
            break;
        }
    }
    void setState(){
        
        state = AtkPatternList[nowState++];
        if(nowState == AtkPatternList.Capacity) nowState = 0;

        proj_nowCD = 0;
        state_nowCD = state_maxCD;
        rb.angularVelocity = 0;

        switch (state)
        {
        case AtkPattern.Idle:
            state_nowCD = state_maxCD * 0.5f;
            break;

        case AtkPattern.Summon:
            qtmp = Quaternion.Euler(0, 0, 0);
            vtmp = new Vector3(15, 0, 0);
            break;

        case AtkPattern.QuaDash:
            transform.right = playertgt.position - transform.position;

            for(int i = 0; i < 4; i++)
            {
                pooli.TakePool("swordImg", transform.position + transform.up*(i-1.5f)*4, transform.rotation);
                pooli.TakePool("hint", transform.position + transform.up*(i-1.5f)*4, transform.rotation);
                pooli.TakePool("swordProj_3", transform.position + transform.up*(i-1.5f)*4, transform.rotation);
            }
            break;


        case AtkPattern.StarFlash:
            vtmp = new Vector3(24, 0, 0);
            destPos = playertgt.position + vtmp;
            qtmp = Quaternion.Euler(0, 0, 0);

            state_nowCD = state_maxCD * 2;
            break;

        case AtkPattern.DiagFall_R:
            qtmp = Quaternion.Euler(0, 0, -90);
            transform.rotation = qtmp;

            vtmp = new Vector3(-10, 16, 0);
            destPos = playertgt.position + vtmp;
            break;

        case AtkPattern.DiagFall_L:
            qtmp = Quaternion.Euler(0, 0, -90);
            transform.rotation = qtmp;

            vtmp = new Vector3(10, 16, 0);
            destPos = playertgt.position + vtmp;
            break;

        case AtkPattern.OrbitCannon:
            qtmp = Quaternion.Euler(0, 0, 0);
            transform.rotation = qtmp;

            vtmp = new Vector3(12, 0, 0);
            destPos = playertgt.position + vtmp;
            transform.right = playertgt.position - transform.position;

            state_nowCD = state_maxCD * 2;
            break;

        case AtkPattern.TouhouSpin:
            altstate = 5;

            state_nowCD = state_maxCD * 2;
            break;

        case AtkPattern._Last:
            state_nowCD = 0;
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
    void MissileLock(Vector3 tgt)
    {
        dir = tgt - transform.position;
        rb.angularVelocity = Vector3.Cross(transform.right, dir.normalized).z * turn;
    }
}

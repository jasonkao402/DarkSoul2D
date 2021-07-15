using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    const float DC_INTV = .2f;
    public float accel, maxSpeed, boost, boost_maxCD;
    float angle, boost_nowCD;
    List<float> lastTapTime = new List<float>{-1, -1, -1, -1};
    List<float> lastTapIntv = new List<float>{999, 999, 999, 999};
    Rigidbody2D rb;
    Vector3 dir;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if(rb.velocity.sqrMagnitude < maxSpeed)
            rb.AddForce(dir * accel);
        transform.right = target.position - transform.position;
        Debug.DrawLine(transform.position, target.position, Color.red);
    }
    void Update() {
        if(Input.GetButtonDown("ALL_UP"))
        {
            lastTapIntv[0] = Time.time - lastTapTime[0];
            if(lastTapIntv[0] < DC_INTV && boost_nowCD < 0)
            {
                rb.AddForce(Vector2.up * boost);
                boost_nowCD = boost_maxCD;
            }
            lastTapTime[0] = Time.time;
        }
        else if(Input.GetButtonDown("ALL_DOWN"))
        {
            lastTapIntv[1] = Time.time - lastTapTime[1];
            if(lastTapIntv[1] < DC_INTV && boost_nowCD < 0)
            {
                rb.AddForce(Vector2.down * boost);
                boost_nowCD = boost_maxCD;
            }
            lastTapTime[1] = Time.time;
        }
        if(Input.GetButtonDown("ALL_RIGHT"))
        {
            lastTapIntv[2] = Time.time - lastTapTime[2];
            if(lastTapIntv[2] < DC_INTV && boost_nowCD < 0)
            {
                rb.AddForce(Vector2.right * boost);
                boost_nowCD = boost_maxCD;
            }
            lastTapTime[2] = Time.time;
        }
        else if(Input.GetButtonDown("ALL_LEFT"))
        {
            lastTapIntv[3] = Time.time - lastTapTime[3];
            if(lastTapIntv[3] < DC_INTV && boost_nowCD < 0)
            {
                rb.AddForce(Vector2.left * boost);
                boost_nowCD = boost_maxCD;
            }
            lastTapTime[3] = Time.time;
        }
        boost_nowCD-=Time.deltaTime;
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");
        //mousepos = mycam.ScreenToWorldPoint(Input.mousePosition);
    }
    private void OnTriggerEnter2D(Collider2D other){
        
    }
}
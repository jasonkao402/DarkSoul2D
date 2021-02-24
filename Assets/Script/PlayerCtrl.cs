using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float accel, maxSpeed;
    float angle;
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
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");
        //mousepos = mycam.ScreenToWorldPoint(Input.mousePosition);
    }
    private void OnTriggerEnter2D(Collider2D other){
        
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
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
        rb.MovePosition(transform.position + (dir * speed));
        transform.right = target.position - transform.position;
        Debug.DrawLine(transform.position, target.position, Color.red);
    }
    void Update() {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");
        //mousepos = mycam.ScreenToWorldPoint(Input.mousePosition);
    }
}
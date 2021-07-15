using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class BulletPoolver : MonoBehaviour
{
    public int setDamage;
    public float timer, vel;
    public float lockAfter = -1, acc = 1;
    Vector2 accv;
    public GameObject effect;
    AftImgPool pooli;
    Rigidbody2D rb;
    private void Awake() {
        pooli = AftImgPool.Instance;
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable() {
        setSpeed();
        if(lockAfter >= 0)
            Invoke("lockOn", lockAfter);
        Invoke("recycle", timer);
    }
    private void FixedUpdate() {
        rb.velocity += accv * acc;
    }
    void recycle()
    {
        if(effect)
            Instantiate(effect, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
    void setSpeed()
    {
        accv = transform.right;
        rb.velocity = accv * vel;
    }
    void lockOn()
    {
        transform.right = bossAI.playertgt.position - transform.position;
        setSpeed();
    }
}

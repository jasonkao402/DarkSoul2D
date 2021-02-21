using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileAim : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Effect;
    public float topspeed = 5f, turn = 0.1f, life = 5f;
    Rigidbody2D rig;
    Vector3 dir, tgt, mpos;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        StartCoroutine(FuseTime(life));
    }

    // Update is called once per frame
    void Update()
    {
        tgt = Camera.main.ScreenToWorldPoint(mpos = Input.mousePosition);
        dir = tgt - transform.position;
        Debug.DrawRay(transform.position, dir);
    }

    void FixedUpdate()
    {
        rig.AddForce(transform.up * topspeed);
        rig.angularVelocity = Vector3.Cross(transform.up, dir.normalized).z * turn;
    }
    void Detonate(){
		if(Effect != null)
			Instantiate(Effect,transform.position,Quaternion.identity);
		Destroy(gameObject);
	}
    IEnumerator FuseTime(float t){
		yield return new WaitForSeconds(t);
		Detonate();
	}
}

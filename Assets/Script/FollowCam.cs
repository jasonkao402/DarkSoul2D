using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public GameObject tgt;
    public Vector3 os;
    public float l;
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        if(tgt)
            transform.position = Vector3.Lerp(transform.position, tgt.transform.position+os, l);
    }
    public static IEnumerator Shake (Transform tf, float t, float mag){
		Vector3 oPos = tf.localPosition;
		float nowt = 0;
		float progress = nowt/t;
		while(nowt < t){
			tf.localPosition = new Vector3(Random.Range(-1f,1f), Random.Range(-1f,1f)) * Mathf.Lerp(mag, 0, progress);
			nowt += Time.deltaTime;
			yield return null;
		}
		tf.localPosition = oPos;
	}
}

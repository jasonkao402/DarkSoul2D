using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ringMaid : MonoBehaviour
{
    public GameObject safezone;
    public float setcountdown;
    public List<float> radi = new List<float>();
    float nowcountdown;
    int lenall, lennow = 0;
    void Start()
    {
        lenall = radi.Count;
        nowcountdown = setcountdown;
    }

    // Update is called once per frame
    void Update()
    {
        nowcountdown -= Time.deltaTime;
        if(nowcountdown < 0)
        {
            lennow ++;
            nowcountdown = setcountdown;
        }
    }
    public IEnumerator ShrinkRing (Transform tf, float t, float sz){
		Vector3 oPos = tf.localPosition;
		float nowt = 0;
		float progress = nowt/t;
		tf.localPosition = oPos;
        yield return null;
	}
}

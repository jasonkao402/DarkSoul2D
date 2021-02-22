using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ringMaid : MonoBehaviour
{
    public Transform safezone, nextzone;
    public float setcountdown, setshrinktime, maxrad;
    public int allRounds;
    float nowshrinktime, nowrad;
    void Start()
    {
        nowshrinktime = setshrinktime;
        nowrad = maxrad;
        safezone.localScale = Vector3.one * maxrad;
        nextzone.localScale = Vector3.one * maxrad;
        StartCoroutine(SetNewRing(safezone.position));
    }
    IEnumerator SetNewRing (Vector3 center)
    {
        Vector2 offset;
        for(int i = 0 ; i < allRounds ; i++)
        {
            nowrad *= 0.5f;
            offset = Random.insideUnitCircle * nowrad;
            nextzone.localPosition += (Vector3)offset;
            nextzone.localScale = Vector3.one * nowrad;
            yield return new WaitForSeconds(setcountdown);
            StartCoroutine(ShrinkRing(nextzone.localPosition, Vector3.one * nowrad));
			yield return new WaitForSeconds(setshrinktime);
		}
	}
    IEnumerator ShrinkRing (Vector3 center, Vector3 scale)
    {
        Vector3 tempS = safezone.localScale, tempP = safezone.localPosition;
		float nowshrinktime = 0;
		while(nowshrinktime < setshrinktime){
			safezone.localScale = Vector3.Lerp(tempS, scale, nowshrinktime/setshrinktime);
            safezone.localPosition = Vector3.Lerp(tempP, center, nowshrinktime/setshrinktime);
			nowshrinktime += Time.deltaTime;
			yield return null;
		}
	}
}

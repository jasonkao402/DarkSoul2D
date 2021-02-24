using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;
    public static GameAssets i
    {
        get{
            if(_i == null)  _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _i;
        }
    }
    public Transform dmgPopup;
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
    public static IEnumerator autoDestroy(GameObject tgt, float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(tgt);
    }
    public static IEnumerator autoDestroy(Transform tgt, float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(tgt);
    }
}

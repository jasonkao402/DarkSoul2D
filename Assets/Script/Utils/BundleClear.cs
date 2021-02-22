using UnityEngine;

public class BundleClear : MonoBehaviour
{
    void Start()
    {
        BundleSelfDestruct(gameObject);
    }
    public static void BundleSelfDestruct(GameObject tgt){
		tgt.transform.DetachChildren();
		Destroy(tgt);
	}
}

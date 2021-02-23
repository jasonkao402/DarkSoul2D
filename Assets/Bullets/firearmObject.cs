using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class firearmData
{
    public Vector3 gunPos;
	public GameObject gunPreFab, Bullet;
	public AudioClip sEff, oaEff, rcEff;
	public int magSize, reloadTicks, burstSize;
	public float shootForce, shotCD, spread;
}

[CreateAssetMenuAttribute(fileName = "gun_data_01", menuName = "LoliSagiri/Create new gun", order = 1)]
public class firearmObject : ScriptableObject
{
  	public firearmData[] gun_data;

	public static void spawnBullet(PlayerType fromPlayer, firearmData heldGuns, int inHand)
	{
		int i = 0, j = heldGuns.burstSize;
		for (; i < j; i++)
		{
			rot = Quaternion.Euler(0, 0, fromPlayer.transform.localEulerAngles.z + Random.Range(-heldGuns[inHand].spread, heldGuns[inHand].spread));
			Rigidbody2D[] rs = Instantiate(heldGuns[inHand].Bullet, fromPlayer.transform.position, rot).GetComponentsInChildren<Rigidbody2D>();
			foreach (Rigidbody2D r in rs)
			{
				r.velocity = r.transform.right * heldGuns[inHand].shootForce;
			}
		}
	}
}

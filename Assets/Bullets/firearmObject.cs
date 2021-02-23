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
}

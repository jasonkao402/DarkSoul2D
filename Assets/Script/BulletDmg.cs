using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class BulletDmg : MonoBehaviour
{
    public int setDamage;
    public float timer;
    public bool isDetonate;
    public GameObject effect;
    PlayerType dmgsource;
    private void Start() {
        dmgsource = transform.GetComponentInParent<PlayerType>();
        transform.SetParent(null);
        StartCoroutine(GameAssets.autoDestroy(gameObject, timer));
    }
    void Detonate()
    {
        Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("PlayerType"))
        {
            if(dmgsource) Debug.Log( $"{dmgsource.PlayerName} hit {other.name}, {setDamage} dmg");
			other.GetComponent<PlayerType>().OnReceiveDmg(setDamage);
			if(isDetonate)  Detonate();
		}
	}
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("ClearZone") || other.CompareTag("GameArea")){
			Destroy(gameObject);
		}
    }
}

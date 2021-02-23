using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class BulletDmg : MonoBehaviour
{
    public int setDamage;
    public bool isDetonate;
    public GameObject effect;
    void Detonate()
    {
        Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("PlayerType"))
        {
            Debug.Log(setDamage + " dmg hit " + other.name);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDmg : MonoBehaviour
{
    public int setDamage;
    public bool isDetonate;
    void Start()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("PlayerType"))
        {
            Debug.Log(setDamage + " dmg hit " + other.name);
			other.GetComponent<PlayerType>().OnReceiveDmg(setDamage);
			//if(isDetonate)  Detonate();
		}
	}
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("ClearZone") || other.CompareTag("GameArea")){
			Destroy(gameObject);
		}
    }
}

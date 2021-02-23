using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunType : MonoBehaviour
{
    public firearmObject firearmData;
	firearmData[] myGuns;
    public int ammoInv, changeTicks;
    float nowCD;
	int nowMag = 0, inHand = 0, i, j;
	AudioSource auds;
	Quaternion rot;
    public Text ammo_In_inv, ammo_In_gun;
    void Start()
    {
		myGuns = firearmData.gun_data;
		auds = GetComponentInChildren<AudioSource>();
		j = myGuns[inHand].burstSize;
        reloadComplete();
    }

    // Update is called once per frame
    void Update()
    {
        //if(nowCD > 0){
		nowCD -= Time.deltaTime;
		//}
        if(Input.GetKey(KeyCode.Mouse0) && nowMag >= 1 && nowCD <= 0f)
		{
			for (i = 0; i < j; i++)
			{
				rot = Quaternion.Euler(0, 0, transform.localEulerAngles.z + Random.Range(-myGuns[inHand].spread, myGuns[inHand].spread));
				Rigidbody2D[] rs = Instantiate(myGuns[inHand].Bullet, transform.position + transform.right * myGuns[inHand].shootForce * 0.05f, rot).GetComponentsInChildren<Rigidbody2D>();
				foreach (Rigidbody2D r in rs)
				{
					r.velocity = r.transform.right * myGuns[inHand].shootForce;
				}
			}
			auds.PlayOneShot(myGuns[inHand].sEff);
			nowCD = myGuns[inHand].shotCD;
			nowMag --;
			updateHUD(ammoInv,nowMag);
		}
		else if(Input.GetAxisRaw("Mouse ScrollWheel") > 0)
		{
			if(inHand == 0)
				inHand = myGuns.Length-1;
			else
				inHand--;
			//StartCoroutine(sc.changeFireArm(changeTicks, inHand));
			ammoInv += nowMag;
			nowMag = 0;
			j = myGuns[inHand].burstSize;
			reloadComplete();
		}
		else if(Input.GetAxisRaw("Mouse ScrollWheel") < 0)
		{
			if(inHand == myGuns.Length-1)
				inHand = 0;
			else
				inHand++;
			//StartCoroutine(sc.changeFireArm(changeTicks, inHand));
			ammoInv += nowMag;
			nowMag = 0;
			j = myGuns[inHand].burstSize;
			reloadComplete();
		}
        else if(Input.GetKeyDown(KeyCode.R))
		{
			ammoInv += nowMag;
			nowMag = 0;
			updateHUD(ammoInv,nowMag);
			//StartCoroutine(sc.reloadFireArm(myGuns[inHand].reloadTicks));
			//StartCoroutine(delayAudio(myGuns[inHand].reloadTicks, myGuns[inHand].rcEff));
			reloadComplete();
		}
    }
    void updateHUD(int i, int g){
		if(ammo_In_inv &&　ammo_In_gun)
		ammo_In_inv.text = "" + i;
		ammo_In_gun.text = "" + g;
	}
    public void reloadComplete(){
		ammoInv -= myGuns[inHand].magSize;
		nowMag = myGuns[inHand].magSize;
		updateHUD(ammoInv, nowMag);
	}
    IEnumerator delayAudio(float t, AudioClip c){
		yield return new WaitForSeconds(t/60f);
		auds.PlayOneShot(c);
	}
}

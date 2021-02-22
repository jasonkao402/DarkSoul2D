using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerType : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxhp, maxsh;
    public float shakemag, dmgtime;
    int nowhp, nowsh, calcDmg;
    public Transform hpbar, shbar;
    void Start()
    {
        if(!hpbar)  hpbar = transform.Find("HP_BAR");
        if(!shbar)  shbar = transform.Find("SH_BAR");
        nowhp = maxhp;
        nowsh = maxsh;
        StartCoroutine(HP_animation(shbar, dmgtime, nowsh, maxsh));
        StartCoroutine(HP_animation(hpbar, dmgtime, nowhp, maxhp));
    }
    public void OnReceiveDmg(int dmg)
    {
        StartCoroutine(GameAssets.Shake(transform.GetChild(0), 0.1f, dmg * shakemag));
        if(nowsh>0)
        {
            calcDmg = Mathf.Min(dmg, nowsh);
            DmgPopup.spawn(transform.position, calcDmg, Color.cyan);
            nowsh -= calcDmg;
            StartCoroutine(HP_animation(shbar, dmgtime, nowsh, maxsh));
        }
        else
        {
            calcDmg = Mathf.Min(dmg, nowhp);
            DmgPopup.spawn(transform.position, calcDmg, Color.red);
            nowhp -= calcDmg;
            StartCoroutine(HP_animation(hpbar, dmgtime, nowhp, maxhp));
            
        }
    }
    public IEnumerator HP_animation (Transform tgt, float t, int tgtval, int maxval)
    {
        if(tgt)
        {
            Vector3 tempS = tgt.localScale, tgtscale = new Vector3((float)tgtval / maxval, 1, 1);
            float n = 0;
            while(n < t){
                tgt.localScale = Vector3.Lerp(tempS, tgtscale, n/t);
                n += Time.deltaTime;
                yield return null;
            }
            tgt.localScale = tgtscale;
        }
        
        yield return null;
	}
    
}

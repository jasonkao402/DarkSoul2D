using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerType : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxhp, maxsh;
    public float shakemag;
    int nowhp, nowsh;
    void Start()
    {
        nowhp = maxhp;
        nowsh = maxsh;
    }
    public void OnReceiveDmg(int dmg)
    {
        StartCoroutine(GameAssets.Shake(transform.GetChild(0), 0.1f, dmg * shakemag));
        if(nowsh>0)
        {
            DmgPopup.spawn(transform.position, Mathf.Min(dmg, nowsh), Color.cyan);
            nowsh -= Mathf.Min(dmg, nowsh);
        }
        else
        {
            DmgPopup.spawn(transform.position, Mathf.Min(dmg, nowhp), Color.red);
            nowhp -= Mathf.Min(dmg, nowhp);
        }
		//gui_Life.LifeUpdate(nowlife);
    }
}

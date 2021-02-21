using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DmgPopup : MonoBehaviour
{
    TextMeshPro txt;
    private void Awake() {
        txt = gameObject.GetComponentInChildren<TextMeshPro>();
    }
    void Init(int n)
    {
        txt.SetText(n.ToString());
        StartCoroutine(autoDestroy(1.5f));
    }
    void Init(int n, Color col)
    {
        txt.SetText(n.ToString());
        txt.color = col;
        StartCoroutine(autoDestroy(1.5f));
    }
    public static DmgPopup spawn(Vector3 pos, int dmg)
    {
        DmgPopup dmgPopup = Instantiate(GameAssets.i.dmgPopup, pos, Quaternion.identity).GetComponentInChildren<DmgPopup>();
        dmgPopup.Init(dmg);
        return dmgPopup;
    }
    public static DmgPopup spawn(Vector3 pos, int dmg, Color col)
    {
        DmgPopup dmgPopup = Instantiate(GameAssets.i.dmgPopup, pos, Quaternion.identity).GetComponentInChildren<DmgPopup>();
        dmgPopup.Init(dmg, col);
        return dmgPopup;
    }  
    IEnumerator autoDestroy(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(gameObject);
    }
}

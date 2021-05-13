using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DmgPopup : MonoBehaviour
{
    TextMeshPro txt;
    public float offset;
    private void Awake() {
        txt = gameObject.GetComponentInChildren<TextMeshPro>();
    }
    void Init(int n)
    {
        txt.SetText(n.ToString());
        Destroy(gameObject, 1.5f);
    }
    void Init(int n, Color col)
    {
        transform.position += (Vector3)Random.insideUnitCircle * offset;
        txt.SetText(n.ToString());
        txt.color = col;
        Destroy(gameObject, 1.5f);
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
}

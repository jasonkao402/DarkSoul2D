using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AftImgCtrl : MonoBehaviour
{
    SpriteRenderer sr;
    Color initc, fadec;
    public float fadeMax;
    float fadeNow;
    public string poolID;
    AftImgPool pooli;
    private void Awake() {
        sr = GetComponentInChildren<SpriteRenderer>();
        initc = sr.color;
        fadec = new Color(initc.r, initc.g, initc.b, 0);
        pooli = AftImgPool.Instance;
    }
    private void Update() {
        fadeNow -= Time.deltaTime;
        sr.color = Color.Lerp(fadec, initc, fadeNow/fadeMax);
    }
    private void OnEnable() {
        sr.color = initc;
        fadeNow = fadeMax;
        Invoke("recycle", fadeMax);
    }
    void recycle()
    {
        gameObject.SetActive(false);
    }
}

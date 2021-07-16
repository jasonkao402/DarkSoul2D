using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AftImgPool : MonoBehaviour{
    [System.Serializable]
    public class Pool{
        public string ID;
        public GameObject blueprint;
        public int amt;
        public bool allowGrow;
    }
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDict = new Dictionary<string, Queue<GameObject>>();
    public static AftImgPool Instance{get; private set;}
    private void Reset() {
        foreach(Pool cpool in pools)
        {
            Debug.Log("cleared " + cpool.ID);
            poolDict[cpool.ID].Clear();
        }
    }
    private void Awake() {
        Instance = this;
    }
    
    private void Start() {
        foreach(Pool cpool in pools)
        {
            Queue<GameObject> poolq = new Queue<GameObject>();
            for(int i = 0; i < cpool.amt; i++)
            {
                GameObject obj = Instantiate(cpool.blueprint, transform);
                obj.SetActive(false);
                poolq.Enqueue(obj);
            }
            poolDict.Add(cpool.ID, poolq);
        }
    }
    public GameObject TakePool(string ID, Vector3 p, Quaternion q)
    {
        if(!poolDict.ContainsKey(ID))
        {
            Debug.LogWarning("ID : " + ID + " not found");
            return null;
        }
        else if(poolDict[ID].Count == 0)
        {
            Debug.LogWarning("ID : " + ID + " not available");
            return null;
        }
        GameObject obj = poolDict[ID].Dequeue();
        obj.transform.position = p;
        obj.transform.rotation = q;
        obj.SetactivateForAllChildren(true);
        poolDict[ID].Enqueue(obj);
        return obj;
    }
    void SetActiveRecur(GameObject obj, bool state)
    {
        obj.SetActive(state);
        foreach (Transform child in obj.transform)
        {
            SetActiveRecur(child.gameObject, state);
        }
    }
}
public static class Extensions
{
    public static void SetactivateForAllChildren(this GameObject go, bool state)
    {
        DeactivateChildren(go, state);
    }
 
    public static void DeactivateChildren(GameObject go, bool state)
    {
        go.SetActive(state);
 
        foreach (Transform child in go.transform)
        {
            DeactivateChildren(child.gameObject, state);
        }
    }
}

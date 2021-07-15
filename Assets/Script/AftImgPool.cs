using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AftImgPool : MonoBehaviour{
    [System.Serializable]
    public class Pool{
        public string ID;
        public GameObject blueprint;
        public int amt;
    }
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDict = new Dictionary<string, Queue<GameObject>>();
    public static AftImgPool Instance{get; private set;}
    private void Start() {
        Instance = this;
        foreach(Pool cpool in pools)
        {
            Queue<GameObject> poolq = new Queue<GameObject>();
            for(int i = 0; i < cpool.amt; i++)
            {
                GameObject obj = Instantiate(cpool.blueprint);
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
        GameObject obj = poolDict[ID].Dequeue();
        obj.SetActive(true);
        obj.transform.position = p;
        obj.transform.rotation = q;

        poolDict[ID].Enqueue(obj);
        return obj;
    }
    public void retPool(GameObject obj){

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static void SetactivateForAllChildren(this GameObject gg, bool state)
    {
        setChildActive_recur(gg, state);
    }
 
    public static void setChildActive_recur(GameObject gg, bool state)
    {
        gg.SetActive(state);
 
        foreach (Transform child in gg.transform)
        {
            setChildActive_recur(child.gameObject, state);
        }
    }
}

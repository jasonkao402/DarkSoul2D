using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCursor : MonoBehaviour
{
    public Camera mycam;
    void Update()
    {
        transform.position = mycam.ScreenToWorldPoint(Input.mousePosition);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam3D : MonoBehaviour
{
    Collider col;
    public GameObject tgt;
    public Vector3 offset;
    public float panSpeed;
    bool focus;
    public float l;
    void FixedUpdate()
    {
        if(tgt)
        {
            transform.position = Vector3.Lerp(transform.position, 
            tgt.transform.position+
            tgt.transform.right * offset[0]+
            tgt.transform.up * offset[1]+
            tgt.transform.forward * offset[2]
            , l);
            transform.LookAt(tgt.transform);
        }
            
        if(!focus)
            transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))*panSpeed*Camera.main.fieldOfView;
    }
    private void Update() {
        Camera.main.fieldOfView *= (-Input.GetAxisRaw("Mouse ScrollWheel")+1);
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 10, 60);
        /*
        if(Input.GetMouseButtonDown(1))
        {
            col = Physics.OverlapSphere(Camera.main.ScreenToWorldPoint(Input.mousePosition), 2, 1|1<<8|1<<9|1<<10)[0];
            if(col != null){
                tgt = col.gameObject;
                focus = true;
            }
            else{
                tgt = null;
                focus = false;
            }
        }
        */
        if(Input.GetMouseButtonDown(0))
        {
            tgt = FindObjectOfType<driftCtrl>().gameObject;
            focus = true;
        }
        else if(Input.GetKeyDown(KeyCode.Q))
        {
            Time.timeScale = 0.5f;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Time.timeScale = 1f;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Time.timeScale = 2.5f;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            Time.timeScale = 5f;
        }
    }
}

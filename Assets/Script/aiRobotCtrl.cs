using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class aiRobotCtrl : MonoBehaviour
{
    public firearmObject firearmData;
    firearmData[] myGuns;
    public AudioSource auds;
    public PlayerType aiPlayer;
    public Transform tgt, visual, setastarget;
    public float wanderRange, reactTime;
    float nowreactTime, nowCD;
    NavMeshAgent pfmaid;
    // Start is called before the first frame update
    void Start()
    {
        myGuns = firearmData.gun_data;
        pfmaid = GetComponent<NavMeshAgent>();
        if(setastarget) setastarget.SetParent(tgt, false);
        pathWander();
        //if(tgt) pfmaid.SetDestination(tgt.position);
        pfmaid.updateRotation = false;
        pfmaid.updateUpAxis = false;
    }
    private void FixedUpdate()
    {
        if(pfmaid.velocity.sqrMagnitude > 1f)
        visual.right = pfmaid.velocity;
    }
    void SetDestinationToPosition()
    {
        if(!tgt)  pfmaid.SetDestination(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        else  pfmaid.SetDestination(tgt.position);
    }
    void pathWander()
    {
        pfmaid.SetDestination(transform.position + (Vector3)Random.insideUnitCircle * wanderRange);
        Debug.DrawLine(transform.position, pfmaid.destination, Color.blue, 2f);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("AI_NewDest"))
        {
            nowreactTime = reactTime;
            pfmaid.ResetPath();
            pfmaid.SetDestination(other.transform.position + (Vector3)Random.insideUnitCircle * wanderRange);
            Debug.DrawLine(transform.position, pfmaid.destination, Color.blue, 2f);
        }
    }
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
		if(other.CompareTag("PlayerType"))
        {
			Collider2D myCollider = other;
            Debug.Log(myCollider.name);
			//myCollider.GetComponent<MeshRenderer>().material = m;
		}
	}
    */
    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("PlayerType"))
        {
            nowreactTime -= Time.deltaTime;
            //nowCD
            if(nowreactTime < 0)
            {
                if(nowCD < 0)
                {
                    pfmaid.SetDestination(other.transform.position);
                    firearmObject.spawnBullet(aiPlayer, myGuns, 0);
                    Debug.Log(other.name);
                    nowCD = myGuns[0].shotCD;
                }
                else
                {
                    nowCD -= Time.deltaTime;
                }
            }
        }
    }
}

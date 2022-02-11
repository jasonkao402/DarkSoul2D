using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class qtest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation *= Quaternion.AngleAxis(0.6f, transform.forward);
        Debug.Log($"z{transform.rotation.z}, w{transform.rotation.w}, angle{transform.localEulerAngles.z}");
    }
}

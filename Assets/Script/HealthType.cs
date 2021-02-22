using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthType : MonoBehaviour
{
    public abstract bool Execute(HealthType ht);
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicObstacle : MonoBehaviour,IPooledObjects
{
    public void OnObjectSpawn()
    {
        float x = Random.Range(-14,14);
        transform.position = new Vector3(x,0,0);
        transform.rotation = new Quaternion(0,x,0,0);  
    }

}

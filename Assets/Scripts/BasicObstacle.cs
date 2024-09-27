using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicObstacle : MonoBehaviour,IPooledObjects
{
    public void OnObjectSpawn()
    {
        float x = Random.Range(-14,14);
        Vector3 position = new Vector3(x,0,0);
        transform.position = position;  
    }

}

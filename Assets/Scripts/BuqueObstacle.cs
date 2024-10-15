using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuqueObstacle : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position; 
        newPosition.z -= 0.01f;  
        transform.position = newPosition;  

    }
}

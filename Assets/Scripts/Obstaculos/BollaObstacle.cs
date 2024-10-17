using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BollaObstacle : MonoBehaviour, IPooledObjects
{


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.tag = "Untagged";
        }
    }

    public void OnObjectSpawn()
    {
        this.tag = "Bolla";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColliderController : MonoBehaviour
{
    private Collider borde;
    [SerializeField] private BarcoController boat;
    public Action BoatCrashed;

    void Start()
    {
        borde = GetComponent<Collider>();
    
    }

    void Update()
    {

    }

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            BoatCrashed.Invoke();
        }
    }
}

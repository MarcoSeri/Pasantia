using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColliderController : MonoBehaviour
{
    private Collider borde;
    [SerializeField] private BarcoController boat;

    void Start()
    {
        boat = GameObject.FindGameObjectWithTag("Player").GetComponent<BarcoController>();
        borde = GetComponent<Collider>();
    
    }

    void Update()
    {

    }

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            boat.BoatCrashed.Invoke();
        }
    }
}

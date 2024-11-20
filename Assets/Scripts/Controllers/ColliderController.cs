using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColliderController : MonoBehaviour
{
    private Collider borde;
    [SerializeField] private BarcoController boat;
    [SerializeField] private GameController gameController;

    void Start()
    {
        boat = GameObject.FindGameObjectWithTag("Player").GetComponent<BarcoController>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        borde = GetComponent<Collider>();
    
    }

    void Update()
    {

    }

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && gameController.OnGame)
        {
            boat.BoatCrashed.Invoke();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rompedor : MonoBehaviour
{
    private BarcoController controller;
    private CameraMovement cam;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            controller.CambiarMultiplicadorVelocidad(2);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //IEnumerator
    }


}

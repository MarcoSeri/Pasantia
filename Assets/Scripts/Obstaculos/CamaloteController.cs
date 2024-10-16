using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaloteController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        BarcoController boat = other.gameObject.GetComponent<BarcoController>();
        boat.bajarLaVelocidad = true;
        boat.CambiarMultiplicadorVelocidad(0.9f);
    }

    private void OnTriggerExit(Collider other)
    {
        BarcoController boat = other.gameObject.GetComponent<BarcoController>();
        boat.bajarLaVelocidad = false;
        boat.CambiarMultiplicadorVelocidad(1);
    }
}

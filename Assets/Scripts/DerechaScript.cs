using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DerechaScript : MonoBehaviour
{
    [SerializeField] private BarcoController barco;
    [SerializeField] private Rigidbody Rigidbody;
    private void OnTriggerEnter(Collider other)
    {
        float velocidadAlImpacto = Rigidbody.velocity.magnitude;

        if (other.CompareTag("Bolla"))
        {
            if (gameObject.name == "Derecha")
                barco.SideCollision(true);

            else if (gameObject.name == "Izquierda")
                barco.SideCollision(false);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DerechaScript : MonoBehaviour
{
    [SerializeField] private BarcoController barco;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bolla"))
        {
            Debug.Log("DERECHA");
            barco.SideCollision(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    private Collider borde;
    [SerializeField] private GameObject other;
    public bool crashed = false;

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
            crashed = true;
        }
    }
}

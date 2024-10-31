using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rompedor : MonoBehaviour
{
    [SerializeField] private BarcoController controller;
    [SerializeField] private CameraMovement cam;


    void Start()
    {
        controller = FindObjectOfType<BarcoController>();
        cam = FindObjectOfType<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            controller.SeMueveSolo();
        }
    }
}

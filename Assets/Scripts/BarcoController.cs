using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;

public class BarcoController : MonoBehaviour{
    private Rigidbody rb;

    [SerializeField] private float Vel_Tanque = 15f;
    [SerializeField] private float Vel_Rotacion = 120f;
    [SerializeField] private float aceleracion = 30f;
    [SerializeField] private float desaceleracion = 150f;

    private float mov_input;
    private float rot_input;
    private float CurrentSpeed = 0;
    private float CurrentDireccion = 1;
    private bool reverse = false;
    private float rotacion=0;

    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update(){
        mov_input = Input.GetAxisRaw("Vertical");
        rot_input = Input.GetAxisRaw("Horizontal");

        if (mov_input < 0)
            reverse = true;
        else if (mov_input > 0)
            reverse = false;
    }
    void FixedUpdate(){
        movertanque(mov_input);
        rotartanque(rot_input);
    }

    void movertanque(float input){
        rb.AddForce(transform.forward* input * Vel_Tanque);
        
    }
    void rotartanque(float Rot_input){
        // Solo rotar si hay entrada de rotación (Rot_input != 0)
       if (Rot_input != 0){
            float direccionRotacion = reverse ? -1f : 1f;
            rotacion = Rot_input * Vel_Rotacion /** direccionRotacion*/ * Time.fixedDeltaTime;
            Quaternion rotar = Quaternion.Euler(0f, rotacion, 0f);
            rb.MoveRotation(rb.rotation * rotar);
        }
    }
}
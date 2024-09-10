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

    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update(){
        mov_input = Input.GetAxisRaw("Vertical");
        if(CurrentSpeed < 0) 
            reverse = true;
        else
            reverse = false;
        rot_input = Input.GetAxis("Horizontal");
    }
    void FixedUpdate(){
        movertanque(mov_input);
        rotartanque(rot_input);

    }

    void movertanque(float input){


        if(input != 0){
            CurrentSpeed += input * Time.deltaTime * aceleracion;
            CurrentSpeed = Mathf.Clamp(CurrentSpeed,-Vel_Tanque,Vel_Tanque);
        }
        else{
            if(CurrentSpeed > 0){
                CurrentSpeed -= desaceleracion * Time.fixedDeltaTime;
                if (CurrentSpeed < 0)
                    CurrentSpeed = 0;
            }
            else if(CurrentSpeed < 0){
                CurrentSpeed += desaceleracion * Time.fixedDeltaTime;
                if (CurrentSpeed > 0)
                    CurrentSpeed = 0;
            }
        }

        Vector3 Direccion = transform.forward * CurrentSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + Direccion);
    }

    void rotartanque(float Rot_input){
        // Solo rotar si hay entrada de rotación (Rot_input != 0)
       if (Rot_input != 0){
            float newRotSpeed = CurrentSpeed != 0? Mathf.Abs(CurrentSpeed / Vel_Tanque) * Vel_Rotacion : Vel_Rotacion;
            float direccionRotacion = reverse ? -1f : 1f;
            float rotacion = Rot_input * newRotSpeed * direccionRotacion * Time.fixedDeltaTime;
            Quaternion rotar = Quaternion.Euler(0f, rotacion, 0f);
            rb.MoveRotation(rb.rotation * rotar);
        }
    }
}
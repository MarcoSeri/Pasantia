using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;
using TMPro;

public class BarcoController : MonoBehaviour{
    private Rigidbody rb;

    [SerializeField] private float Vel_Tanque = 15f;
    [SerializeField] private float Vel_Rotacion = 120f;
    [SerializeField] private float aceleracion = 30f;
    [SerializeField] private float desaceleracion = 150f;
    [SerializeField] private TMP_Text distancia;
    private float mov_input;
    private float rot_input;
    private bool reverse = false;
    private float rotacion = 0;

    private string textin;

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
        float pos_ant = float.Parse(distancia.text);
        distancia.text = score(transform.position.z*10,pos_ant);
    }

    void movertanque(float input){
        rb.AddForce(transform.forward* input * Vel_Tanque);
        
    }
    void rotartanque(float Rot_input){
       if (Rot_input != 0){
            float direccionRotacion = reverse ? -1f : 1f;
            rotacion = Rot_input * Vel_Rotacion /** direccionRotacion*/ * Time.fixedDeltaTime;
            Quaternion rotar = Quaternion.Euler(0f, rotacion, 0f);
            rb.MoveRotation(rb.rotation * rotar);
        }
    }

    public string score(float pos, float pos_ant){
        if(pos > pos_ant){
            pos = Mathf.Round(pos / 10) * 10;
            return pos.ToString();
        }
        else if(pos < pos_ant)
            return pos_ant.ToString();
        return "0";

    }

}
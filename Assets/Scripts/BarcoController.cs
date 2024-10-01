using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;
using TMPro;

public class BarcoController : MonoBehaviour {
    private Rigidbody rb;

    [SerializeField] private float Vel_Tanque = 15f;
    [SerializeField] private float Vel_Rotacion = 120f;
    [SerializeField] private GameController GameController;
    [SerializeField] private Animator animator;
    
    public float distance;
    private float mov_input;
    private float rot_input;
    private float rotacion = 0;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        if (GameController.OnGame == true)
        {
            mov_input = Input.GetAxisRaw("Vertical");
            rot_input = Input.GetAxisRaw("Horizontal");
        }
        distance = transform.position.z;
    }
        void FixedUpdate() {
            if (GameController.OnGame == true)
            {
                movertanque(mov_input);
                rotartanque(rot_input);
            }

            if(mov_input != 0 && rot_input != 0)
            {
            if (rot_input < 0)
                animator.SetFloat("DerSpeed", 2);
            else
                animator.SetFloat("IzqSpeed", 2);
            }
        else
        {
            animator.SetFloat("IzqSpeed", 1);
            animator.SetFloat("DerSpeed", 1);
        }

    }

        void movertanque(float input) {
            if (input < 0)
                rb.AddForce(transform.forward * input * Vel_Tanque * 0.6f);
            else
                rb.AddForce(transform.forward * input * Vel_Tanque);

            animator.SetInteger("Movimiento", (int)input);
        }
        void rotartanque(float Rot_input) {
            if (Rot_input != 0) {
                rotacion = Rot_input * Vel_Rotacion * Time.fixedDeltaTime;
                Quaternion rotar = Quaternion.Euler(0f, rotacion, 0f);
                rb.MoveRotation(rb.rotation * rotar);
            }

            animator.SetBool("RotateRight", Rot_input > 0 ? true : false);
            animator.SetBool("RotateLeft", Rot_input < 0 ? true : false);
        }
        
        public void DeleteForce()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
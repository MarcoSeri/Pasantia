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

    public Action BoatCrashed;
    public Action<bool> SideCollision;
    public bool bajarLaVelocidad = false;
    public int distance;

    private float mov_input;
    private float rot_input;
    private float rotacion = 0;
    private float modifier = 1;

    Coroutine startRotation;

    private void HandleSideCollision(bool side)
    {
        if (side)
        {
            StartRotationCoroutine(-1, 0.5f);

        }
        else
        {
            StartRotationCoroutine(-0.5f, 1);
        }

    }

    public void StartRotationCoroutine(float min, float max)
    {
        startRotation = StartCoroutine(onSideCollision(min,max));
    }

    IEnumerator onSideCollision(float min, float max)
    {
        Math.Clamp(rot_input, min, max);
        yield return new WaitForSeconds(5);

            Debug.Log("Ya termino");
    }

    void Start() {
        SideCollision += HandleSideCollision;
        rb = GetComponent<Rigidbody>();
    }
    
    void Update() {
        if (GameController.OnGame == true)
        {
            mov_input = Input.GetAxisRaw("Vertical");
            rot_input = Input.GetAxisRaw("Horizontal");
        }
        distance = Mathf.RoundToInt(transform.position.z);
    }
        void FixedUpdate() {
            if(bajarLaVelocidad)
                rb.velocity = Vector3.SmoothDamp(rb.velocity, Vector3.one, ref currentVelocity, 0.5f);

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
                rb.AddForce(transform.forward * input * Vel_Tanque * modifier);

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

    private Vector3 currentVelocity;

    public void CambiarMultiplicadorVelocidad(float multiplicador)
    {
        modifier = multiplicador;
    }


}
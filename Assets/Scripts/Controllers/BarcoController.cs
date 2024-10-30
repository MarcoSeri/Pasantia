using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;
using TMPro;

public class BarcoController : MonoBehaviour {
    private Rigidbody rb;

    [SerializeField] private float MinMax;
    [SerializeField] private float Vel_Tanque = 15f;
    [SerializeField] private float Vel_Rotacion = 120f;
    [SerializeField] private GameController GameController;
    [SerializeField] private CameraMovement cam;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject[] remos;

    public Action BoatCrashed;
    public Action<bool> SideCollision;
    public bool bajarLaVelocidad = false;
    public int distance;

    [SerializeField] private Material[] materiales;
    private bool seMueveSolo;
    private float mov_input;
    private float rot_input;
    private float rotacion = 0;
    private float modifier = 1;


    Lado modRotInput = Lado.No;
    public enum Lado
    {
        Derecha,
        Izquierda,
        No
    }

    Coroutine startRotation;
    Coroutine startMoverse;

    private void HandleSideCollision(bool side)
    {
        if (side)
        {
            startRotation = StartCoroutine(onSideCollision(Lado.Izquierda,remos[0]));
        }
        else
        {
           startRotation = StartCoroutine(onSideCollision(Lado.Derecha,remos[1]));

        }
    }

    public void SeMueveSolo()
    {
            startMoverse = StartCoroutine(seMueveSoloCorrutina());
    }

    public void StopCorroutine()
    {
        seMueveSolo = false;
        StopCoroutine(startMoverse);
    }

    IEnumerator seMueveSoloCorrutina()
    {
        seMueveSolo = true;
        yield return new WaitForSeconds(7.5f);
        cambiarMaterial(false);
        Physics.IgnoreLayerCollision(6, 7, false);
        seMueveSolo = false;
    }

    IEnumerator onSideCollision(Lado side,GameObject remo)
    {
        remo.SetActive(false);
        modRotInput = side;
        yield return new WaitForSeconds(5);
        modRotInput = Lado.No;
        remo.SetActive(true);
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

            if (modRotInput != Lado.No)
                rot_input = modifyRotInput(modRotInput, rot_input, MinMax);

            MoverSolo(seMueveSolo);
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

    public void MoverSolo(bool seMueve)
    {
        if (seMueve)
        {
            cambiarMaterial(true);
            Physics.IgnoreLayerCollision(6, 7, true);
            cam.MoveCamera(20f);
            rb.AddForce(transform.forward * 550 * Time.deltaTime);
        }
    }

    private float modifyRotInput (Lado side,float rot_input,float MinMax)
    {
        if (side == Lado.Derecha)
            return Mathf.Clamp(rot_input, -1, MinMax);
        else
            return Mathf.Clamp(rot_input, -MinMax, 1);
    }

    private void cambiarMaterial(bool aTransparente)
    {
        Renderer cubeRenderer = transform.Find("BarcoNoAnimations2/Cube").GetComponent<Renderer>();
        Renderer remoRenderer = transform.Find("BarcoNoAnimations2/Remo 1").GetComponent<Renderer>();
        Renderer remo1Renderer = transform.Find("BarcoNoAnimations2/Remo 1.001").GetComponent<Renderer>();
        Material[] cubeMaterials = cubeRenderer.materials;

        if (aTransparente)
        {
            cubeMaterials[0] = materiales[1];
            cubeMaterials[1] = materiales[3];

            cubeRenderer.materials = cubeMaterials;

            remoRenderer.material = materiales[5];

            remo1Renderer.material = materiales[5];
        }
        else 
        {
            cubeMaterials[0] = materiales[0];
            cubeMaterials[1] = materiales[2];

            cubeRenderer.materials = cubeMaterials;

            remoRenderer.material = materiales[4];

            remo1Renderer.material = materiales[4];
        }


    }
}
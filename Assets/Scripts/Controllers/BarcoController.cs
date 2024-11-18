using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;
using DG.Tweening;


public class BarcoController : MonoBehaviour {
    private Rigidbody rb;

    [SerializeField] private float MinMax;
    [SerializeField] private float Vel_Tanque = 15f;
    [SerializeField] private float Vel_Rotacion = 120f;

    [SerializeField] private GameController GameController;
    [SerializeField] private CameraMovement cam;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject[] remos;
    [SerializeField] private Material[] materiales;

    public Action BoatCrashed;
    public Action<bool> SideCollision;
    public float modifier = 1;
    public int distance;
    public bool bajarLaVelocidad = false;
    private bool _bajarLaVelocidad = false;

    private bool seMueveSolo;
    private float mov_input;
    private float rot_input;
    private float rotacion = 0;

    Vector3 initialPos;


    Lado modRotInput = Lado.No;
    public enum Lado
    {
        Derecha,
        Izquierda,
        No
    }

    Coroutine startRotation;
    Coroutine startMoverse;

    private void Awake()
    {
        initialPos = transform.position;
    }

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

    IEnumerator seMueveSoloCorrutina()
    {
        seMueveSolo = true;
        yield return new WaitForSeconds(5);
        volverNormal();
    }

    public void StopCorroutine()
    {
        seMueveSolo = false;
        if(startMoverse != null)
        {
            StopCoroutine(startMoverse);
            volverNormal();
        }
    }

    private void volverNormal()
    {
        bajarVelocidadBarco();
        cam.ReinicioCamaraSuave();
        cambiarMaterial(false);
        Physics.IgnoreLayerCollision(6, 7, false);
        seMueveSolo = false;
    }


    IEnumerator onSideCollision(Lado side,GameObject remo)
    {
        //SoundManager.PlaySound();
        remo.SetActive(false);
        modRotInput = side;
        yield return new WaitForSeconds(5);
        modRotInput = Lado.No; //Reiniciar REmos cuando pierde
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
            if(bajarLaVelocidad != _bajarLaVelocidad && bajarLaVelocidad == true)
            {
            SoundManager.PlaySound(SoundManager.Sound.EntrarCamalote, 1);
            }
            _bajarLaVelocidad = bajarLaVelocidad;
            
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

    bool _seMueve = false;
    public void MoverSolo(bool seMueve)
    {

        if (seMueve)
        {
            if(seMueve != _seMueve)
            {
                cambiarMaterial(true);
            }

            Physics.IgnoreLayerCollision(6, 7, true);
            cam.MoveCamera(20f);
            rb.AddForce(transform.forward * 550 * Time.deltaTime);
        }

        _seMueve = seMueve;     
    }

    private float modifyRotInput (Lado side,float rot_input,float MinMax)
    {
        if (side == Lado.Derecha)
            return Mathf.Clamp(rot_input, -1, MinMax);
        else
            return Mathf.Clamp(rot_input, -MinMax, 1);
    }

    private void bajarVelocidadBarco()
    {
        rb.AddForce(transform.forward * -700);
    }

    private void cambiarMaterial(bool aTransparente)
    {
        Renderer cubeRenderer = transform.Find("BarcoNoAnimations2/Cube").GetComponent<Renderer>();
        Renderer remoRenderer = transform.Find("BarcoNoAnimations2/Remo 1").GetComponent<Renderer>();
        Renderer remo1Renderer = transform.Find("BarcoNoAnimations2/Remo 1.001").GetComponent<Renderer>();
        Material[] cubeMaterials = cubeRenderer.materials;

        if (aTransparente)
        {
            SoundManager.PlaySound(SoundManager.Sound.PowerUpVelocidad,1);

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

    public void ResetCamera()
    {
        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, 0);
        animator.SetBool("Undir", false);
    }

    public void SeUndioElBarco()
    {
        rb.Sleep();
        rb.isKinematic = true;
        rb.useGravity = false;
        transform.DOMoveY(-1, .8f).OnComplete(()=> 
        {                     
            transform.position = initialPos;
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            Physics.SyncTransforms();
        });
    }

    public void ResetPhysics()
    {
                rb.isKinematic = false;
                rb.WakeUp();
                rb.useGravity = true;
                GameController.SetUp();
    }

    private void OnCollisionEnter(Collision collision)
    {
        float fuerzaGolpe = collision.relativeVelocity.magnitude;
        if (collision.gameObject.name == "tronco")
        {
            SoundManager.PlaySound(SoundManager.Sound.GolpeTronco, .07f * fuerzaGolpe);
        }

        if (collision.gameObject.name == "LifeBuoy")
        {
            SoundManager.PlaySound(SoundManager.Sound.Rebote, 1);
        }

        if (collision.gameObject.name == "Boya2.1")
        {
            SoundManager.PlaySound(SoundManager.Sound.GolpeBolla, .07f * fuerzaGolpe);
        }
    }
}
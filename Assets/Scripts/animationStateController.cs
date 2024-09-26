using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isGirandoDerecha = animator.GetBool("GiraDerecha");
        bool GiraDerecha = Input.GetKey("d");

        bool isGirandoIzquierda = animator.GetBool("GiraIzquierda");
        bool GiraIzquierda = Input.GetKey("a");

        bool isAdelante = animator.GetBool("Adelante");
        bool Adelante = Input.GetKey("w");

        if(!isGirandoDerecha && GiraDerecha)
        {
            //animator.SetFloat("GiraDerecha", true);
        }
        if(isGirandoDerecha && !GiraDerecha)
        {
            animator.SetBool("GiraDerecha", false);
        }

        if(!isGirandoIzquierda && GiraIzquierda)
        {
            animator.SetBool("GiraIzquierda", true);
        }
        if(isGirandoIzquierda && !GiraIzquierda)
        {
            animator.SetBool("GiraIzquierda", false);
        }

        if(!isAdelante && Adelante)
        {
            animator.SetBool("Adelante", true);
        }
        if(isAdelante && !Adelante)
        {
            animator.SetBool("Adelante", false);
        }
        
    }
}

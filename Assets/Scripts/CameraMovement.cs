using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraMovement : MonoBehaviour{
    [SerializeField]private float velocidad = 1f;
    [SerializeField]private float VelTransicion = 2f;
    public bool DisplayMap = false;
    private float currentDificult = 1;

    void Start(){
    }

    void FixedUpdate(){
    }

    public void MoveCamera(float dificulty){ 
            currentDificult = Mathf.Lerp(currentDificult, dificulty, VelTransicion * Time.deltaTime);
            transform.Translate(new Vector3(0, 0, velocidad * currentDificult * Time.deltaTime), Space.World); 
    }

    public void OnTriggerEnter(Collider other)
    {
        DisplayMap = true;
    }
}


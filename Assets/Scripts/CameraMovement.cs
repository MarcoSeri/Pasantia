using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraMovement : MonoBehaviour{
    [SerializeField]private float velocidad = 1f;

    void Start(){
    }

    void FixedUpdate(){
    }
    public void MoveCamera(float dificulty){
            transform.Translate(new Vector3(0,0,velocidad*dificulty*Time.deltaTime), Space.World);
    }
}


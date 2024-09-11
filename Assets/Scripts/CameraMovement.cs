using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraMovement : MonoBehaviour{
    [SerializeField]private float velocidad = 1f;

    void Start(){
    }

    void FixedUpdate(){
        transform.Translate(new Vector3(0,0,velocidad), Space.World);
    }
}

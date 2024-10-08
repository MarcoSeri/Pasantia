using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraMovement : MonoBehaviour{
    [SerializeField]private float velocidad = 1f;
    public bool DisplayMap = false;
    private float currentDificult = 1;
    void Start(){
    }

    void FixedUpdate(){
    }
    public void MoveCamera(float dificulty){
        if (dificulty != currentDificult)
        {
            currentDificult += 0.5f;
            transform.Translate(new Vector3(0,0,velocidad*currentDificult*Time.deltaTime), Space.World);
        }
        else if(dificulty == currentDificult)
            transform.Translate(new Vector3(0, 0, velocidad * dificulty * Time.deltaTime), Space.World);
    }

    public void OnTriggerEnter(Collider other)
    {
        DisplayMap = true;
    }
}


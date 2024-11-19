using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraMovement : MonoBehaviour{
    [SerializeField]private float velocidad = 1f;
    [SerializeField]private float VelTransicion = 2f;
    [SerializeField]private float VelTransicionObstaculo = 1f;
    [SerializeField]private float[] VelocidadesPorDificultad;

    float velocidadSuave;
    float que;

    public bool DisplayMap = false;
    public float currentDificult = 1;

    void Start(){
    }
    void FixedUpdate(){
    }

    public void MoveCamera(int dificulty){
            que = VelocidadesPorDificultad[dificulty - 1];
            currentDificult = Mathf.Lerp(currentDificult, VelocidadesPorDificultad[dificulty-1], VelTransicion * Time.deltaTime);
            transform.Translate(new Vector3(0, 0, velocidad * currentDificult * Time.deltaTime), Space.World); 
    }
    public void MoveCamera(float velocidadObjetivo)
    {
        if (Mathf.Abs(velocidadSuave - velocidadObjetivo) > 0.01f)
        {
            velocidadSuave = Mathf.Lerp(velocidadSuave, velocidadObjetivo, VelTransicionObstaculo * Time.deltaTime);
        }
        else
        {
            velocidadSuave = velocidadObjetivo;
        }

        transform.Translate(new Vector3(0, 0, velocidadSuave * Time.deltaTime), Space.World);
    }

    public void ReinicioCamaraSuave()
    {
        velocidadSuave = 0;
    }


    public void OnTriggerEnter(Collider other)
    {
        DisplayMap = true;
    }
}


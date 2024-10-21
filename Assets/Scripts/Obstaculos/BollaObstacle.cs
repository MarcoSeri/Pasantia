using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BollaObstacle : MonoBehaviour, IPooledObjects
{
    public float amplitude = 0.5f;  // Amplitud del movimiento vertical
    public float frequency = 1.0f;  // Frecuencia del movimiento vertical
    public float rotationAmplitude = 10f;  // Amplitud del movimiento de rotaci�n
    public float rotationFrequency = 0.5f;  // Frecuencia del movimiento de rotaci�n

    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        // Guardamos la posici�n inicial y la rotaci�n inicial
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void Update()
    {
        // Movimiento vertical
        float newY = startPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);

        // Rotaci�n oscilante alrededor de los ejes X e Z, manteniendo la orientaci�n hacia arriba (sin cambiar el eje Y)
        float newRotationX = Mathf.Sin(Time.time * rotationFrequency) * rotationAmplitude;
        float newRotationZ = Mathf.Cos(Time.time * rotationFrequency) * rotationAmplitude;

        // Aplicamos la rotaci�n sin afectar el eje Y (mantener la punta hacia arriba)
        transform.rotation = startRotation * Quaternion.Euler(newRotationX, 0f, newRotationZ);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.tag = "Untagged";
        }
    }

    public void OnObjectSpawn()
    {
        this.tag = "Bolla";
    }
}

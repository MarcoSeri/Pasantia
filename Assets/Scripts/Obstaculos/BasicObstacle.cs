using UnityEngine;
using System.Collections;

public class BasicObstacle : MonoBehaviour, IPooledObjects
{
    public float amplitude = 1f;  // Amplitud del movimiento
    public float frequency = 1.0f;  // Frecuencia del movimiento
    private Vector3 startPosition;
    private float x = 0;
    private int[] izqOrDerList = new int[] { 1, -1 };
    private int izqOrDer = 1;

    void Start()
    {
    }

    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        transform.Rotate(Vector3.up, 0.1f * Time.deltaTime * izqOrDer);
    }

    public void OnObjectSpawn()
    {
        izqOrDer = izqOrDerList[Random.Range(0,izqOrDerList.Length)];
    }
}

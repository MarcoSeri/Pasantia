using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public ObjectPooler objectPol;

    public void Start() {
    InvokeRepeating("spawnObstacle", 2.0f, 5f);
    }
    public void spawnObstacle()
    {
        objectPol.SpawnFromPool("Basic",transform.position, Quaternion.identity);
    }
}
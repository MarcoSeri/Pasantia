using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    public ObjectPooler objectPol;
    public float timeToSpawn = 5f;
    [SerializeField] private float timeToStartSpawning = 1f;
    [SerializeField] private GameController gamecontrol;
    [SerializeField] private BarcoController boat;
    [SerializeField] private string[] mapTags;

    Coroutine spawnRoutine;

    public void Start() {
        spawnRoutine = StartCoroutine(SpawnCoroutine());
    }

    private void Update()
    {
        if(boat.distance % 25 == 0)
        {
            Debug.Log("mapa");
            SpawnMap();
        }
    }
    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(timeToStartSpawning);
        while (gamecontrol.OnGame == true) //game is running
        {
            SpawnSingleRock();
            yield return new WaitForSeconds(timeToSpawn);            
        }
    }
    public void spawnObstacle()
    {
        //string tag = basicTags[Random.Range(0, basicTags.Length)];
        objectPol.SpawnFromPool("Basic",transform.position, Quaternion.identity);
    }

    public void SpawnSingleRock()
    {
        float x = Random.Range(boat.transform.position.x-7,boat.transform.position.x+7);
        x = Mathf.Clamp(x, -15, 15);
        SpawnSingleRock(new Vector3(x, transform.position.y, transform.position.z));
    }

    public void SpawnMap()
    {
        string tag = mapTags[Random.Range(0, mapTags.Length-1)];
        objectPol.SpawnFromPool(tag, new Vector3(0,0,20), Quaternion.identity);
    }

    public void SpawnSingleRock(Vector3 position)
    {
        objectPol.SpawnFromPool("Basic", position, Quaternion.identity);
    }

    public void StopSpawner()
    {
        StopCoroutine(spawnRoutine);
    }
}
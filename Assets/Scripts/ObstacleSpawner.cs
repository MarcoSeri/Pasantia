using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class ObstacleSpawner : MonoBehaviour
{
    public ObjectPooler objectPol;
    public float timeToSpawn = 5f;
    [SerializeField] private float timeToStartSpawning = 1f;
    [SerializeField] private GameController gamecontrol;
    [SerializeField] private BarcoController boat;
    [SerializeField] private string[] mapTags;
    private bool mapWasDisplayed = false;

    Coroutine spawnRoutine;

    private void Update()
    {
        if(boat.distance % 25 == 0 && mapWasDisplayed == false)
        {
            SpawnMap();
            mapWasDisplayed = true;
        }
        else if(boat.distance % 25 != 0)
            mapWasDisplayed = false;
    }

    public void StartBasicCoroutine(){
        spawnRoutine = StartCoroutine(SpawnCoroutine());
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

    public void SpawnSingleRock(Vector3 position)
    {
        objectPol.SpawnFromPool("Basic", position, Quaternion.identity);
    }

    public void SpawnMap()
    {
        string tag = mapTags[Random.Range(0, mapTags.Length)];
        //objectPol.SpawnFromPool(tag, new Vector3(0,0,boat.distance+31.27f), Quaternion.identity);
    }
    public void StopSpawner()
    {
        StopCoroutine(spawnRoutine);
        objectPol.DesPawnAll();
    }
}
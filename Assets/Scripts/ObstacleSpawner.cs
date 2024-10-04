using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class ObstacleSpawner : MonoBehaviour
{
    public ObjectPooler objectPol;
    public float realtimetoSpawn = 5f;
    public float timeToSpawn = 5f;
    [SerializeField] private float timeToStartSpawning = 1f;
    [SerializeField] private GameController gamecontrol;
    [SerializeField] private BarcoController boat;
    [SerializeField] private CameraMovement camera;
    [SerializeField] private string[] mapTags;
    private bool mapWasDisplayed = false;

    Coroutine spawnBasicCoroutine;
    Coroutine spawnLifebuoyCoroutine;

    private void Update()
    {
        if(camera.DisplayMap == true)
        {
            SpawnMap();
            camera.DisplayMap = false;
        }
    }

    public void StartBasicCoroutine(){
        objectPol.DesPawnAll();
        //spawnLifebuoyCoroutine = StartCoroutine(SpawnLifebuoyCoroutine());
        spawnBasicCoroutine = StartCoroutine(SpawnCoroutine());
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

    IEnumerator SpawnLifebuoyCoroutine()
    {
        yield return new WaitForSeconds(2);
        while (gamecontrol.OnGame == true) //game is running
        {
            SpawnSingleLifebuoy();
            yield return new WaitForSeconds(10);
        }
    }

    public void spawnObstacle()
    {
        //string tag = basicTags[Random.Range(0, basicTags.Length)];
        objectPol.SpawnFromPool("Basic",transform.position, Quaternion.identity);
    }

    public void SpawnSingleRock()
    {
        float x = Random.Range(boat.transform.position.x-9,boat.transform.position.x+9);
        x = Mathf.Clamp(x, -15, 15);
        SpawnSingleRock(new Vector3(x, transform.position.y, transform.position.z));
    }

    public void SpawnSingleRock(Vector3 position)
    {
        objectPol.SpawnFromPool("Basic", position, Quaternion.identity);
    }

    public void SpawnSingleLifebuoy()
    {
        float x = Random.Range(boat.transform.position.x - 7, boat.transform.position.x + 7);
        x = Mathf.Clamp(x, -15, 15);
        objectPol.SpawnFromPool("Lifebuoy", new Vector3(x, transform.position.y+0.4f, transform.position.z), Quaternion.identity);
    }

    public void SpawnMap()
    {
        string tag = mapTags[Random.Range(0, mapTags.Length)];
        objectPol.SpawnFromPool(tag, new Vector3(0,0,camera.transform.position.z+50.4f), Quaternion.identity);
    }
    public void StopSpawner()
    {
        StopCoroutine(spawnBasicCoroutine);
        //StopCoroutine(spawnLifebuoyCoroutine);
        timeToSpawn = realtimetoSpawn;
    }
}
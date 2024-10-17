using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

[System.Serializable]
public class TagProbability
{
    public string tag;
    public float probability;
}

public class ObstacleSpawner : MonoBehaviour
{
    public ObjectPooler objectPol;
    [SerializeField] private float timeToStartSpawning = 1f;
    [SerializeField] private GameController gamecontrol;
    [SerializeField] private BarcoController boat;
    [SerializeField] private new CameraMovement camera;
    [SerializeField] private string[] mapTags;
    [SerializeField] private TagProbability[] troncoTags;
    [SerializeField] private float[] timeToSpawn;

    public int TimeToSpawnRockIndex;

    Coroutine spawnBasicCoroutine;
    Coroutine spawnLifebuoyCoroutine;
    Coroutine spawnCamaloteCoroutine;

    private void Update()
    {
        TimeToSpawnRockIndex = gamecontrol.Dificulty - 1;
        if (camera.DisplayMap == true)
        {
            SpawnMap();
            camera.DisplayMap = false;
        }
    }

    public void StartBasicCoroutine() {
        objectPol.DesPawnAll();
        spawnBasicCoroutine = StartCoroutine(SpawnCoroutine());
        spawnCamaloteCoroutine = StartCoroutine(SpawnCamaloteCoroutine());
    }

    public void StarLifebuoyCoroutine() {
        spawnLifebuoyCoroutine = StartCoroutine(SpawnLifebuoyCoroutine());
    }
    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(timeToStartSpawning);
        while (gamecontrol.OnGame == true) //game is running
        {
            SpawnSingleRock();
            float randomTimer = Random.Range(0.5f, 1.1f);
            yield return new WaitForSeconds(timeToSpawn[TimeToSpawnRockIndex] * randomTimer);
        }
    }

    IEnumerator SpawnLifebuoyCoroutine()
    {
        yield return new WaitForSeconds(2f);
        while (gamecontrol.OnGame == true) //game is running
        {
            SpawnSingleLifebuoy();
            yield return new WaitForSeconds(10);
        }
    }

    IEnumerator SpawnCamaloteCoroutine()
    {
        yield return new WaitForSeconds(0.8f);
        while (gamecontrol.OnGame == true) //game is running
        {
            SpawnSingleCamalote();
            yield return new WaitForSeconds(15);
        }
    }

    public void spawnObstacle()
    {
        //string tag = basicTags[Random.Range(0, basicTags.Length)];
        objectPol.SpawnFromPool("Basic", transform.position, Quaternion.identity);
    }

    public void SpawnSingleRock()
    {
        float x = Random.Range(boat.transform.position.x - 10, boat.transform.position.x + 10);
        x = Mathf.Clamp(x, -15, 15);
        Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        SpawnSingleRock(new Vector3(x, transform.position.y, transform.position.z), randomRotation);
    }

    public void SpawnSingleRock(Vector3 position, Quaternion randomRotation)
    {
        objectPol.SpawnFromPool(selectTag(troncoTags), position, randomRotation);
    }

    public void SpawnSingleLifebuoy()
    {
        float x = Random.Range(boat.transform.position.x - 5, boat.transform.position.x + 5);
        x = Mathf.Clamp(x, -15, 15);
        objectPol.SpawnFromPool("Lifebuoy", new Vector3(x, transform.position.y + 0.4f, transform.position.z), Quaternion.identity);
    }

    public void SpawnSingleCamalote() {
        float x = Random.Range(boat.transform.position.x - 5, boat.transform.position.x + 5);
        x = Mathf.Clamp(x, -15, 15);
        objectPol.SpawnFromPool("Boya", new Vector3(x, transform.position.y + 0.4f, transform.position.z + 15), Quaternion.identity);
        objectPol.SpawnFromPool("Buque", new Vector3(-x, transform.position.y + 0.4f, transform.position.z + 50), Quaternion.identity);
        objectPol.SpawnFromPool("Camalote", new Vector3(x, transform.position.y + 0.4f, transform.position.z + 5), Quaternion.identity);
    }

    public void SpawnMap()
    {
        string tag = mapTags[Random.Range(0, mapTags.Length)];
        objectPol.SpawnFromPool(tag, new Vector3(0, 0, camera.transform.position.z + 50.4f), Quaternion.identity);
    }
    public void StopSpawner()
    {
        StopCoroutine(spawnBasicCoroutine);
        if (spawnLifebuoyCoroutine != null)
            StopCoroutine(spawnLifebuoyCoroutine);
    }

    private string selectTag(TagProbability[] tags)
    {
        int posibility = Random.Range(0, 100);
        float cumulativeProbability = 0f;

        foreach (TagProbability tag in tags)
        {
            cumulativeProbability += tag.probability;

            if (posibility <= cumulativeProbability)
                return tag.tag;
        }

        return tags[0].tag;
    }

}
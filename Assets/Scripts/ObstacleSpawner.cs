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
    
    private int TimeToSpawnRockIndex;

    Coroutine spawnBasicCoroutine;
    Coroutine spawnLifebuoyCoroutine;
    Coroutine spawnCamaloteCoroutine;
    Coroutine spawnBuoyCoroutine;

    private void Update()
    {
        TimeToSpawnRockIndex = gamecontrol.Dificulty - 1;
        if (camera.DisplayMap == true)
        {
            SpawnMap();
            camera.DisplayMap = false;
        }
    }



    #region Boya
    public void StartBuoyCoroutine()
    {
        spawnBuoyCoroutine = StartCoroutine(SpawnBuoyCoroutine());
    }

    IEnumerator SpawnBuoyCoroutine()
    {
        yield return new WaitForSeconds(2f);
        while (gamecontrol.OnGame == true) //game is running
        {
            Debug.Log("Spawnea dale");
            SpawnSingleBuoy();
            float randomTimer = Random.Range(8, 15);
            yield return new WaitForSeconds(randomTimer);
        }
    }

    private void SpawnSingleBuoy()
    {
        float x = Random.Range(boat.transform.position.x - 5, boat.transform.position.x + 5);
        x = Mathf.Clamp(x, -15, 15);
        objectPol.SpawnFromPool("Boya", new Vector3(x, transform.position.y + 1.3f, transform.position.z), Quaternion.identity);
    }
    #endregion

    #region Basico
    public void StartBasicCoroutine() {
        objectPol.DesPawnAll();
        spawnBasicCoroutine = StartCoroutine(SpawnCoroutine());
        spawnCamaloteCoroutine = StartCoroutine(SpawnCamaloteCoroutine());
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
    IEnumerator SpawnCamaloteCoroutine()
    {
        yield return new WaitForSeconds(5f);
        while (gamecontrol.OnGame == true) //game is running
        {
            SpawnSingleCamalote();
            float randomTimer = Random.Range(1f, 2.5f);
            yield return new WaitForSeconds(5 * randomTimer);
        }
    }
    #endregion

    #region SalvaVidas
    public void StarLifebuoyCoroutine() {
        spawnLifebuoyCoroutine = StartCoroutine(SpawnLifebuoyCoroutine());
    }
    IEnumerator SpawnLifebuoyCoroutine()
    {
        yield return new WaitForSeconds(2f);
        while (gamecontrol.OnGame == true) //game is running
        {
            SpawnSingleLifebuoy();
            yield return new WaitForSeconds(8);
        }
    }
    #endregion


    public void spawnObstacle()
    {
        //string tag = basicTags[Random.Range(0, basicTags.Length)];
        objectPol.SpawnFromPool("Basic", transform.position, Quaternion.identity);
    }

    public void SpawnSingleRock()
    {
        float x = Random.Range(boat.transform.position.x - 13, boat.transform.position.x + 13);
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
        objectPol.SpawnFromPool("Camalote", new Vector3(x, transform.position.y + 0.4f, transform.position.z + 5), Quaternion.identity);
    }

    public void SpawnMap()
    {
        string tag = mapTags[Random.Range(0, mapTags.Length)];
        objectPol.SpawnFromPool(tag, new Vector3(20, 0, camera.transform.position.z + 32.4f), Quaternion.identity);
    }
    public void StopSpawner()
    {
        StopCoroutine(spawnBasicCoroutine);
        StopCoroutine(spawnCamaloteCoroutine);

        if (spawnLifebuoyCoroutine != null)
            StopCoroutine(spawnLifebuoyCoroutine);

        if (spawnBuoyCoroutine != null)
            StopCoroutine(spawnBuoyCoroutine);
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
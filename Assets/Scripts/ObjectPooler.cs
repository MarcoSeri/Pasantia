using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

   /* #region Singleton
    public static ObjectPooler Instance;
    private void Awake() {
        
    }

    #endregion
*/

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Start()
    {
     poolDictionary = new Dictionary<string, Queue<GameObject>>();

     foreach (Pool pool in pools)
     {
        Queue<GameObject> objectPool = new Queue<GameObject>();

        for(int i = 0; i < pool.size; i++)
        {
            GameObject obj = Instantiate(pool.prefab);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }

        poolDictionary.Add(pool.tag, objectPool);
     }
    }

    public GameObject SpawnFromPool (string tag, Vector3 position, Quaternion rotation)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesnt exist");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);

        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.transform.position = position;

        IPooledObjects pooledObj = objectToSpawn.GetComponentInChildren<IPooledObjects>();
        
        if(pooledObj != null){
            pooledObj.OnObjectSpawn();
        }

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public void DesPawnAll(){
        
        GameObject[] AllObstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        GameObject[] Mapas = GameObject.FindGameObjectsWithTag("Mapa");

        for (int i = 0; i < Mapas.Length; i++)
        {
            Mapas[i].SetActive(false);
        }

        for (int i = 0; i < AllObstacles.Length; i++)
        {
            AllObstacles[i].SetActive(false);
        }
    }
}

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
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDict;


    public static ObjectPooler instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }


    void Start()
    {
        poolDict = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            poolDict.Add(pool.tag, objectPool);
        }
    }

    /// <summary>
    /// Method called to get the object from the pool 
    /// </summary>
    /// <param name="tag">the tag of the object we want to spawn</param>
    /// <returns></returns>
    public GameObject SpawnFromPool(string tag)
    {
        GameObject objectToSpawn;
        if (poolDict[tag].Count>0)
            objectToSpawn = poolDict[tag].Dequeue();
        else
        {
            foreach(Pool pool in pools)
            {
                if(pool.tag.Equals(tag))
                {
                    objectToSpawn = Instantiate(pool.prefab);
                    objectToSpawn.SetActive(false);
                    return objectToSpawn;
                }
            }
        }
            
        return null;
    }

    public GameObject SpawnFromPool(string tag, Vector2 position, Quaternion rotation)
    {
        GameObject objectToSpawn=null;
        if (poolDict[tag].Count > 0)
        {
            objectToSpawn = poolDict[tag].Dequeue();
        }   
        else
        {
            foreach (Pool pool in pools)
            {
                if (pool.tag.Equals(tag))
                {
                    objectToSpawn = Instantiate(pool.prefab);
                    break;
                }
            }
        }       
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);
        return objectToSpawn;
    }


    /// <summary>
    /// Method called to place an object back to the pool
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="objToPlace"></param>
    public void PlaceInPool(string tag, GameObject objToPlace)
    {
        poolDict[tag].Enqueue(objToPlace);
    }
}

using UnityEngine;
using System.Collections.Generic;

// Creates a pool of objects that can be reused, instead of creating and destroying them repeatedly. This can improve performance and reduce memory usage in games.
public class ObjectPooler : MonoBehaviour
{ 
    [SerializeField] GameObject objectToPool; // The prefab of the object to pool
    [SerializeField] int amountToPool; // The number of objects to pool
    private List<GameObject> pooledObjects; // The array of pooled objects

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetPooledObject()
    {
        foreach (GameObject pooledObject in pooledObjects)
        {
            if (!pooledObject.activeInHierarchy)
            {
                return pooledObject;
            }
        }

        GameObject newObject = Instantiate(objectToPool);
        newObject.SetActive(false);
        pooledObjects.Add(newObject);
        return newObject;
    }
}

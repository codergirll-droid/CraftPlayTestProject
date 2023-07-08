using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling Instance;

    List<List<GameObject>> pooledObjects;
    [Space]

    public List<string> objectNames;

    public GameObject[] objectsToPool;

    public int[] amountsToPool;

    GameObject parentObject;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(this);
        }

        parentObject = new GameObject("parentObject");
        pooledObjects = new List<List<GameObject>>();
        GameObject tmp;

        for (int k = 0; k < objectsToPool.Length; k++)
        {
            pooledObjects.Add(new List<GameObject>());


            for (int j = 0; j < amountsToPool[k]; j++)
            {
                tmp = Instantiate(objectsToPool[k], parentObject.transform);
                tmp.SetActive(false);
                pooledObjects[k].Add(tmp);
            }
        }

    }

    public GameObject GetPooledObject(string objectName)
    {
        for (int i = 0; i < objectNames.Count; i++)
        {
            if (objectNames[i] == objectName)
            {
                Debug.Log(pooledObjects.Count);

                for (int j = 0; j < amountsToPool[i]; j++)
                {
                    Debug.Log(pooledObjects[i][j].name);

                    if (!pooledObjects[i][j].activeInHierarchy)
                    {

                        return pooledObjects[i][j];
                    }
                }
            }
        }

        return null;
    }
}

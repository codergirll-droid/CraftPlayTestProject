using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling Instance;

    List<List<GameObject>> pooledBullets;
    [Space]

    public List<Weapon> weapons;

    GameObject[] bulletsToPool;

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

        parentObject = new GameObject("bulletsParentObject");
        pooledBullets = new List<List<GameObject>>();
        bulletsToPool = new GameObject[weapons.Count];

        for (int i = 0; i < weapons.Count; i++)
        {

            bulletsToPool[i] = weapons[i].bulletObject;
        }

        GameObject tmp;

        for (int k = 0; k < weapons.Count; k++)
        {
            pooledBullets.Add(new List<GameObject>());


            for (int j = 0; j < amountsToPool[k]; j++)
            {
                tmp = Instantiate(bulletsToPool[k], parentObject.transform);
                tmp.SetActive(false);
                pooledBullets[k].Add(tmp);
            }
        }

    }

    public GameObject GetPooledObject(Weapon weapon)
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i] == weapon)
            {
                for (int j = 0; j < amountsToPool[i]; j++)
                {
                    if (!pooledBullets[i][j].activeInHierarchy)
                    {

                        return pooledBullets[i][j];
                    }
                }
            }
        }

        return null;
    }
}

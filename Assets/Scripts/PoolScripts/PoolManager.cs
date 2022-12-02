using UnityEngine;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;
    public List<ObjectPool> decorationPool = new List<ObjectPool>(),
        treesPool = new List<ObjectPool>(),
        rocksPool = new List<ObjectPool>(),
        grassPool = new List<ObjectPool>();
    public ObjectPool scorePool = new ObjectPool(), monsterPool = new ObjectPool(), hitPool = new ObjectPool();
    public List<Material> randomMaterials = new List<Material>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StoreInstance(GameObject clone)
    {
        string fixedName = clone.name.Replace("(Clone)", "");
        if (fixedName.Contains("Rock"))
        {
            foreach (ObjectPool objectPool in rocksPool)
            {
                if (objectPool.newInstance.name.Equals(fixedName))
                {
                    objectPool.StoreInstance(clone);
                    break;
                }
            }
        }
        else if (fixedName.Contains("Tree"))
        {
            foreach (ObjectPool objectPool in treesPool)
            {
                if (objectPool.newInstance.name.Equals(fixedName))
                {
                    objectPool.StoreInstance(clone);
                    break;
                }
            }
        }
        else if (fixedName.Contains("Grass"))
        {
            foreach (ObjectPool objectPool in grassPool)
            {
                if (objectPool.newInstance.name.Equals(fixedName))
                {
                    objectPool.StoreInstance(clone);
                    break;
                }
            }
        }
        else if (fixedName.Contains("Score"))
        {
            scorePool.StoreInstance(clone);
        }
        else if (fixedName.Contains("Chick"))
        {
            monsterPool.StoreInstance(clone);
        }
        else if (fixedName.Contains("Hit"))
        {
            hitPool.StoreInstance(clone);
        }
        else
        {
            foreach (ObjectPool objectPool in decorationPool)
            {
                if (objectPool.newInstance.name.Equals(fixedName))
                {
                    objectPool.StoreInstance(clone);
                    break;
                }
            }
        }
    }

    public GameObject GetInstance(string type)
    {
        GameObject newInstance;
        ObjectPool pool;
        if("Tree".Equals(type))
        {
            pool = treesPool[Random.Range(0, treesPool.Count)];
        }
        else if ("Rock".Equals(type))
        {
            pool = rocksPool[Random.Range(0, rocksPool.Count -1)];
        }
        else if ("Special Rock".Equals(type))
        {
            pool = rocksPool[rocksPool.Count - 1];
        }
        else if ("Grass".Equals(type))
        {
            pool = grassPool[Random.Range(0, grassPool.Count)];
        }
        else if ("Score".Equals(type))
        {
            pool = scorePool;
        }
        else if ("Toon Chick".Equals(type))
        {
            pool = monsterPool;
        }
        else if ("Hit".Equals(type))
        {
            pool = hitPool;
        }
        else
        {
            pool = decorationPool[Random.Range(0, decorationPool.Count)];
        }
        newInstance = pool.GetInstance();
        if (type.Contains("Rock")) newInstance = RandomMaterial(newInstance);
        newInstance.transform.position = pool.fixPosition;
        if (!"Toon Chick".Equals(type) && !"Hit".Equals(type)) newInstance.transform.Rotate(new Vector3(0, Random.Range(0, 360), 0), Space.Self);
        return newInstance;
    }

    private GameObject RandomMaterial(GameObject newInstance)
    {
        newInstance.GetComponent<Renderer>().material = randomMaterials[Random.Range(0, randomMaterials.Count)];
        return newInstance;
    }
}

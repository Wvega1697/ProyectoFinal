using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public string namePool;
    public List<GameObject> objects = new List<GameObject>();
    private Queue<GameObject> availableInstances = new Queue<GameObject>();
    private List<GameObject> inUseInstances = new List<GameObject>();
    private List<GameObject> availableInstancesList = new List<GameObject>();

    public GameObject GetInstance()
    {
        GameObject clone;
        if (availableInstances.Count > 0)
        {
            clone = availableInstances.Dequeue();
        }
        else
        {
            clone = Instantiate(objects[Random.Range(0, objects.Count)]);
        }
        clone.SetActive(true);
        return clone;
    }

    public GameObject GetRandomInstance()
    {
        GameObject clone;
        if (availableInstancesList.Count > 0)
        {
            int i = Random.Range(0, availableInstancesList.Count);
            clone = availableInstancesList[i];
            availableInstancesList.RemoveAt(i);
        }
        else
        {
            clone = Instantiate(objects[Random.Range(0, objects.Count)]);
        }
        clone.SetActive(true);
        return clone;
    }

    public void StoreInstance(GameObject clone)
    {
        inUseInstances.Remove(clone);
        availableInstances.Enqueue(clone);
        clone.SetActive(false);

        //availableInstancesList.Add(clone);
    }

    public void StoreRandomInstance(GameObject clone)
    {
        availableInstancesList.Add(clone);
        inUseInstances.Remove(clone);
        clone.SetActive(false);
    }
}

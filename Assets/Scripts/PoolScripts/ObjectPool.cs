using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject newInstance;
    Queue<GameObject> availableInstances = new Queue<GameObject>();
    List<GameObject> inUseInstances = new List<GameObject>();
    public Vector3 fixPosition;
  
    public GameObject GetInstance()
    {
        GameObject clone;
        if (availableInstances.Count > 0)
        {
            clone = availableInstances.Dequeue();
        }
        else
        {
            clone = Instantiate(newInstance);
        }
        clone.SetActive(true);
        inUseInstances.Add(clone);
        return clone;
    }

    public void StoreInstance(GameObject clone)
    {
        inUseInstances.Remove(clone);
        availableInstances.Enqueue(clone);
        clone.SetActive(false);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public ObjectPool instance;
    public GameObject newInstance;
    Queue<GameObject> availableInstances = new Queue<GameObject>();
    List<GameObject> inUseInstances = new List<GameObject>();
    public Vector3 fixPosition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        string _name = newInstance.name;
        int instances = (_name.Equals("Toon Chick") || _name.Contains("Boss")) ? 1 : 5;
        for (int i = 0; i < instances; i++)
        {
            GameObject clone = Instantiate(newInstance);
            clone.SetActive(false);
            availableInstances.Enqueue(clone);
            clone.transform.parent = gameObject.transform;
        }
    }

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
            clone.transform.parent = gameObject.transform;
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

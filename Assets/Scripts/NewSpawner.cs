using System.Collections.Generic;
using UnityEngine;

public class NewSpawner : MonoBehaviour
{

    //Public
    public float decorationRestarter = 2f, treesRestarter = 2f, rocksRestarter = 2f;
    public List<Transform> lanesTransforms = new List<Transform>();
    public List<Transform> treesTransforms = new List<Transform>();
    public Transform spawnTransform;
    //Private
    float decorationTimer, treesTimer, rocksTimer;

    private void Start()
    {
        decorationTimer = decorationRestarter;
        treesTimer = treesRestarter;
        rocksTimer = rocksRestarter;
    }

    private void Update()
    {
        if(GameManager.instance.live)
        {
            TimersLogic();
        }
    }

    private void TimersLogic()
    {
        if(decorationTimer > 0)
        {
            decorationTimer -= Time.deltaTime;
        }
        else
        {
            decorationTimer = decorationRestarter;
            SpawnerLogic("decoration");
        }
        if (treesTimer > 0)
        {
            treesTimer -= Time.deltaTime;
        }
        else
        {
            treesTimer = treesRestarter;
            SpawnerLogic("tree");
        }
        if (rocksTimer > 0)
        {
            rocksTimer -= Time.deltaTime;
        }
        else
        {
            rocksTimer = rocksRestarter;
            SpawnerLogic("rock");
        }
    }
    private void SpawnerLogic(string type)
    {
        GameObject clone;
        switch (type)
        {
            case "tree":
                clone = GameManager.instance.treesPool.GetInstance();
                clone.transform.position = treesTransforms[Random.Range(0, lanesTransforms.Count)].position;
                break;
            case "rock":
                clone = GameManager.instance.rocksPool.GetInstance();
                clone.transform.position = lanesTransforms[Random.Range(0, lanesTransforms.Count)].position;
                break;
            default:
                clone = GameManager.instance.decorationPool.GetInstance();
                clone.transform.position = lanesTransforms[Random.Range(0, lanesTransforms.Count)].position;
                break;
        }
        clone.transform.parent = spawnTransform;
    }
}


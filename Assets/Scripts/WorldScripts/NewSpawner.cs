using System.Collections.Generic;
using UnityEngine;

public class NewSpawner : MonoBehaviour
{

    //Public
    public float decorationRestarter = 0.1f, grassRestarter = 0.05f, treesRestarter = 1f, rocksRestarter = 2f;
    public List<Transform> lanesTransforms = new List<Transform>();
    public List<Transform> treesTransforms = new List<Transform>();
    public Transform grassTransform, dangerousTransform, treesTransform, decorationTransform, scoreTransform;
    //Private
    float grassTimer, decorationTimer, treesTimer, rocksTimer;

    private void Start()
    {
        decorationTimer = decorationRestarter;
        treesTimer = treesRestarter;
        rocksTimer = rocksRestarter;
        grassTimer = grassRestarter;
    }

    private void Update()
    {
        if(GameManager.instance.live)
        {
            if (grassTimer > 0)
            {
                grassTimer -= Time.deltaTime;
            }
            else
            {
                grassTimer = grassRestarter;
                SpawnerLogic("Grass");
                SpawnerLogic("Grass");
            }
            if (decorationTimer > 0)
            {
                decorationTimer -= Time.deltaTime;
            }
            else
            {
                decorationTimer = decorationRestarter;
                SpawnerLogic("Decoration");
                SpawnerLogic("Tree", 0);
                SpawnerLogic("Tree", 5);
            }
            if (treesTimer > 0)
            {
                treesTimer -= Time.deltaTime;
            }
            else
            {
                treesTimer = treesRestarter;
                SpawnerLogic("Tree");
            }
            if (rocksTimer > 0)
            {
                rocksTimer -= Time.deltaTime;
            }
            else
            {
                rocksTimer = rocksRestarter;
                SpawnerLogic("Rock");
                SpawnerLogic("Rock");
                SpawnerLogic("Special Rock");
            }
        }
    }

    private void SpawnerLogic(string type)
    {

        GameObject clone = PoolManager.instance.GetInstance(type);
        if ("Special Rock".Equals(type)) type = "Rock";
        switch (type)
        {
            case "Tree":
                clone.transform.position += treesTransforms[Random.Range(1, treesTransforms.Count - 1)].position;
                clone.transform.parent = treesTransform;
                break;
            case "Rock":
                clone.transform.position += lanesTransforms[Random.Range(0, lanesTransforms.Count)].position;
                clone.transform.parent = dangerousTransform;
                GameObject scoreClone = PoolManager.instance.GetInstance("Score");
                scoreClone.transform.position = clone.transform.position;
                scoreClone.transform.parent = scoreTransform;
                break;
            default:
                clone.transform.position += lanesTransforms[Random.Range(0, lanesTransforms.Count)].position;
                clone.transform.position = new Vector3(Random.Range(-2.875f, 3f), clone.transform.position.y, clone.transform.position.z);
                if ("Grass".Equals(type)) clone.transform.parent = grassTransform;
                else clone.transform.parent = decorationTransform;
                break;
        }
        
    }

    private void SpawnerLogic(string type, int transformId)
    {
        GameObject clone = PoolManager.instance.GetInstance(type);
        if ("Special Rock".Equals(type)) type = "Rock";
        switch (type)
        {
            case "Tree":
                clone.transform.position += treesTransforms[transformId].position;
                clone.transform.parent = treesTransform;
                break;
            case "Rock":
                clone.transform.position += lanesTransforms[transformId].position;
                clone.transform.parent = dangerousTransform;
                break;
            default:
                clone.transform.position += lanesTransforms[transformId].position;
                clone.transform.position = new Vector3(Random.Range(-2.875f, 3f), clone.transform.position.y, clone.transform.position.z);
                clone.transform.parent = decorationTransform;
                break;
        }
    }
}


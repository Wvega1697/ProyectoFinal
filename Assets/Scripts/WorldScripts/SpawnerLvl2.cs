using System.Collections.Generic;
using UnityEngine;

public class SpawnerLvl2 : MonoBehaviour
{
    //Public
    public float decorationRestarter = 0.1f, grassRestarter = 0.05f, treesRestarter = 1f, rocksRestarter = 2f, monsterRestarter = 1.3f;
    public List<Transform> lanesTransforms = new List<Transform>();
    public List<Transform> treesTransforms = new List<Transform>();
    public Transform grassTransform, dangerousTransform, treesTransform, decorationTransform, chickTransform;
    //Private
    float grassTimer, decorationTimer, treesTimer, rocksTimer, chickTimer, monsterTimer;

    private void Start()
    {
        decorationTimer = decorationRestarter;
        treesTimer = treesRestarter;
        rocksTimer = rocksRestarter;
        grassTimer = grassRestarter;
        monsterTimer = monsterRestarter;
        chickTimer = 5;
    }

    private void Update()
    {
        if (GameManager.instance.live && !GameManager.instance.pause)
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
                if (Random.value > 0.7) SpawnerLogic("Special Rock");
                else
                {
                    SpawnerLogic("Rock");
                    SpawnerLogic("Score");
                }
            }
            if (monsterTimer > 0)
            {
                monsterTimer -= Time.deltaTime;
            }
            else
            {
                monsterTimer = monsterRestarter;
                SpawnerLogic("Monster");
            }
            if (chickTimer > 0)
            {
                chickTimer -= Time.deltaTime;
            }
            else
            {
                chickTimer = Random.Range(4, 8);
                SpawnerLogic("Toon Chick");
            }
        }
        if (gameObject.transform.position.z < 21) gameObject.transform.position += new Vector3(0, 0, (5f * Time.deltaTime));
    }

    private void SpawnerLogic(string type)
    {
        GameObject clone = PoolManager.instance.GetInstance(type);
        if ("Special Rock".Equals(type)) type = "Rock";
        switch (type)
        {
            case "Tree":
                clone.transform.position += treesTransforms[Random.Range(1, 3)].position;
                GameObject clone2 = PoolManager.instance.GetInstance(type);
                clone2.transform.position += treesTransforms[Random.Range(3, treesTransforms.Count - 1)].position;
                break;
            case "Rock":
                clone.transform.position += lanesTransforms[Random.Range(0, lanesTransforms.Count)].position;
                break;
            case "Monster":
                clone.transform.position += lanesTransforms[Random.Range(0, lanesTransforms.Count)].position;
                break;
            case "Toon Chick":
                clone.transform.position += lanesTransforms[Random.Range(0, lanesTransforms.Count)].position;
                break;
            default:
                clone.transform.position += lanesTransforms[Random.Range(0, lanesTransforms.Count)].position;
                clone.transform.position = new Vector3(Random.Range(-2.875f, 3f), clone.transform.position.y, clone.transform.position.z);
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
                break;
            case "Rock":
                clone.transform.position += lanesTransforms[transformId].position;
                break;
            default:
                clone.transform.position += lanesTransforms[transformId].position;
                clone.transform.position = new Vector3(Random.Range(-2.875f, 3f), clone.transform.position.y, clone.transform.position.z);
                break;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class SpawnerLvl3 : MonoBehaviour
{
    //Public
    public float decorationRestarter = 0.1f, grassRestarter = 0.05f, rocksRestarter = 1f, bossRestarter = 10f;
    public int chickMin, chickMax;
    public List<Transform> lanesTransforms = new List<Transform>();
    public Transform grassTransform, dangerousTransform, treesTransform, decorationTransform, chickTransform;
    //Private
    float grassTimer, decorationTimer, rocksTimer, chickTimer, bossTimer;

    private void Start()
    {
        decorationTimer = decorationRestarter;
        rocksTimer = rocksRestarter;
        grassTimer = grassRestarter;
        chickTimer = Random.Range(chickMin, chickMax);
        bossTimer = Random.Range(bossRestarter, bossRestarter*3); ;
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
            if (rocksTimer > 0)
            {
                rocksTimer -= Time.deltaTime;
            }
            else
            {
                rocksTimer = rocksRestarter;
                SpawnerLogic("Rock");
                SpawnerLogic("Rock");
                SpawnerLogic("Score");
            }
            if (chickTimer > 0)
            {
                chickTimer -= Time.deltaTime;
            }
            else
            {
                chickTimer = Random.Range(chickMin, chickMax);
                SpawnerLogic("Toon Chick");
            }
            if (bossTimer > 0)
            {
                bossTimer -= Time.deltaTime;
            }
            else
            {
                bossTimer = Random.Range(bossRestarter, bossRestarter * 1.5f) * Time.timeScale;
                WolfLevel3Script.instance.BossSounds(true);
                SpawnerLogic("Boss");
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
            case "Rock":
                clone.transform.position += lanesTransforms[Random.Range(0, lanesTransforms.Count)].position;
                break;
            case "Boss":
                clone.transform.position += lanesTransforms[Random.Range(0, lanesTransforms.Count)].position;
                clone.transform.position = new Vector3(Random.Range(-2f, 2f), clone.transform.position.y, clone.transform.position.z);
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

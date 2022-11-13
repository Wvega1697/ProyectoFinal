using UnityEngine;

public class SpawnerScript : MonoBehaviour
{

    //Private
    GameObject player;
    GameObject[] players;
    Animator animator;
    float timer, spawnTimer = 2f;
    float decorationTimer, decorationSpawnTimer = 0.25f;
    //Public
    public GameObject log, rock1, rock2, rock3, rock4, rock5, rock6, rock7;
    public GameObject tree1, tree2, tree3, tree4, tree5, tree6;
    public GameObject grass1, grass2, grass3, flower1, flower2, flower3, flower4, flower5, mushroom1, mushroom2, mushroom3, mushroom4, mushroom5, mushroom6, mushroom7, mushroom8, mushroom9, mushroom10;
    public GameObject centralSpawner, leftSpawner, rightSpawner, separator1, separator2;
    public GameObject ND1, ND2, ND3, ND4, ND5, ND6;
    public Transform spawnTransform;
    public float destructionTimer = 5f;

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerI in players)
        {
            player = playerI;
        }
        animator = player.GetComponent<Animator>();
        timer = spawnTimer;
        decorationTimer = decorationSpawnTimer;
        decorationTimer = decorationSpawnTimer;
    }

    private void Update()
    {
        if (animator.GetBool("Live"))
        {
            timer -= Time.deltaTime;
            decorationTimer -= Time.deltaTime;
            if (timer <= 0)
            {
                Spawn2();
                Spawn();
                Spawn();
                Spawn();
                timer = spawnTimer;
            }
            if (decorationTimer <= 0)
            {
                SpawnDecorations(ND1);
                SpawnDecorations(ND6);
                SpawnDecoration();
                SpawnFloorDecoration();
                decorationTimer = decorationSpawnTimer;
            }
        }
    }
	
	void Spawn()
	{
        GameObject spawnpoint = SelectDangerousSpawner(Random.Range(0, 3));
        GameObject newObject = Instantiate(SelectDangerous(Random.Range(1, 7)), spawnpoint.transform.position, spawnpoint.transform.rotation) as GameObject;
        newObject.transform.parent = spawnTransform;
		Destroy(newObject, destructionTimer);
	}

    void Spawn2()
	{
        GameObject spawnpoint = SelectDangerousSpawner(Random.Range(0, 3));
        GameObject newObject = Instantiate(SelectDangerous(0), spawnpoint.transform.position, spawnpoint.transform.rotation) as GameObject;
        newObject.transform.parent = spawnTransform;
		Destroy(newObject, destructionTimer);
	}

    void SpawnDecorations(GameObject spawnpoint)
	{
        GameObject newObject = Instantiate(SelectNotDangerous(Random.Range(0, 6)), spawnpoint.transform.position, spawnpoint.transform.rotation) as GameObject;
        newObject.transform.parent = spawnTransform;
		Destroy(newObject, destructionTimer);
	}

    void SpawnDecoration()
	{
        GameObject spawnpoint = SelectNotDangerousSpawner(Random.Range(3, 6));
        GameObject newObject = Instantiate(SelectNotDangerous(Random.Range(0, 6)), spawnpoint.transform.position, spawnpoint.transform.rotation) as GameObject;
        newObject.transform.parent = spawnTransform;
		Destroy(newObject, destructionTimer);
        
        GameObject spawnpoint2 = SelectNotDangerousSpawner(Random.Range(1, 3));
        GameObject newObject2 = Instantiate(SelectNotDangerous(Random.Range(0, 6)), spawnpoint2.transform.position, spawnpoint2.transform.rotation) as GameObject;
        newObject2.transform.parent = spawnTransform;
		Destroy(newObject2, destructionTimer);
	}
    
    void SpawnFloorDecoration()
	{
        GameObject spawnpoint = SelectNotDangerousSpawner(Random.Range(3, 6));
        Vector3 newPosition = new Vector3(Random.Range((spawnpoint.transform.position.x) -0.5f, (spawnpoint.transform.position.x) +0.6f),spawnpoint.transform.position.y, spawnpoint.transform.position.z);
        GameObject newObject = Instantiate(SelectFloorDecoration(Random.Range(0, 18)), newPosition, spawnpoint.transform.rotation) as GameObject;
        newObject.transform.parent = spawnTransform;
		Destroy(newObject, destructionTimer);

        GameObject spawnpoint2 = SelectNotDangerousSpawner(Random.Range(1, 3));
        Vector3 newPosition2 = new Vector3(Random.Range((spawnpoint2.transform.position.x) -0.5f, (spawnpoint2.transform.position.x) +0.6f),spawnpoint2.transform.position.y, spawnpoint2.transform.position.z);
        GameObject newObject2 = Instantiate(SelectFloorDecoration(Random.Range(0, 18)), newPosition2, spawnpoint2.transform.rotation) as GameObject;
        newObject2.transform.parent = spawnTransform;
		Destroy(newObject2, destructionTimer);
	}

    private GameObject SelectDangerous(int x)
    {
        switch (x)
        {
            case 1:
                return rock1;
            case 2:
                return rock2;
            case 3:
                return rock3;
            case 4:
                return rock4;
            case 5:
                return rock5;
            case 6:
                return rock6;
            default:
                return rock7;
        }
    }

    private GameObject SelectNotDangerous(int x)
    {
        switch (x)
        {
            case 1:
                return tree1;
            case 2:
                return tree2;
            case 3:
                return tree3;
            case 4:
                return tree4;
            case 5:
                return tree5;
            default:
                return tree6;
        }
    }

    private GameObject SelectDangerousSpawner(int x)
    {
        switch (x)
        {
            case 1:
                return rightSpawner;
            case 2:
                return leftSpawner;
            default:
                return centralSpawner;
        }
    }

    private GameObject SelectNotDangerousSpawner(int x)
    {
        switch (x)
        {
            case 1:
                return ND1;
            case 2:
                return ND2;
            case 3:
                return ND3;
            case 4:
                return ND4;
            case 5:
                return ND5;
            default:
                return ND6;
        }
    }

    private GameObject SelectFloorDecoration(int x)
    {
        switch (x)
        {
            case 1:
                return grass1;
            case 2:
                return grass2;
            case 3:
                return grass3;
            case 4:
                return flower1;
            case 5:
                return flower2;
            case 6:
                return flower3;
            case 7:
                return flower4;
            case 8:
                return flower5;
            case 9:
                return mushroom1;
            case 10:
                return mushroom2;
            case 11:
                return mushroom3;
            case 12:
                return mushroom4;
            case 13:
                return mushroom5;
            case 14:
                return mushroom6;
            case 15:
                return mushroom7;
            case 16:
                return mushroom8;
            case 17:
                return mushroom9;
            default:
                return mushroom10;
        }
    }
}

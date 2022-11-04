using UnityEngine;

public class SpawnerScript : MonoBehaviour
{

    //Private
    GameObject player;
    GameObject[] players;
    Animator animator;
    float timer, spawnTimer = 3f;
    float decorationTimer, decorationSpawnTimer = 1f;
    float floorDecorationTimer, floorDecorationSpawnTimer = 0.25f;
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
        floorDecorationTimer = floorDecorationSpawnTimer;
    }

    private void Update()
    {
        if (animator.GetBool("Live"))
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Spawn();
                timer = spawnTimer;
            }
            decorationTimer -= Time.deltaTime;
            if (decorationTimer <= 0)
            {
                SpawnDecoration();
                SpawnDecoration();
                decorationTimer = decorationSpawnTimer;
            }
            floorDecorationTimer -= Time.deltaTime;
            if (floorDecorationTimer <= 0)
            {
                SpawnFloorDecoration();
                floorDecorationTimer = floorDecorationSpawnTimer;
            }
        }
    }
	
	void Spawn()
	{
        GameObject spawnpoint = SelectDangerousSpawner(Random.Range(0, 3));
        GameObject newObject = Instantiate(SelectDangerous(Random.Range(0, 7)), spawnpoint.transform.position, spawnpoint.transform.rotation) as GameObject;
        newObject.transform.parent = spawnTransform;
		Destroy(newObject, destructionTimer);
	}

    void SpawnDecoration()
	{
        GameObject spawnpoint = SelectNotDangerousSpawner(Random.Range(0, 6));
        GameObject newObject = Instantiate(SelectNotDangerous(Random.Range(0, 6)), spawnpoint.transform.position, spawnpoint.transform.rotation) as GameObject;
        newObject.transform.parent = spawnTransform;
		Destroy(newObject, destructionTimer);
	}
    
    void SpawnFloorDecoration()
	{
        GameObject spawnpoint = centralSpawner;
        GameObject newObject = Instantiate(SelectFloorDecoration(Random.Range(0, 18)), new Vector3(Random.Range(-2.8f, 2.9f), spawnpoint.transform.position.y, spawnpoint.transform.position.z), spawnpoint.transform.rotation) as GameObject;
        newObject.transform.parent = spawnTransform;
		Destroy(newObject, destructionTimer);
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

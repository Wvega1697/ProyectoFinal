using UnityEngine;

public class SpawnerScript : MonoBehaviour
{

    public GameObject rock1, log, rock2, rock3, rock4, rock5, rock6, rock7;
    public GameObject centralSpawner, leftSpawner, rightSpawner;
    public GameObject ND1, ND2, ND3, ND4, ND5, ND6;
    public Transform spawnTransform;
    public float shootTimer = 3f, destructionTimer = 5f;
	private float timer = 3f;

    void Start()
    {
        timer = shootTimer;
    }

    private void Update()
    {
		timer -= Time.deltaTime;

        if (timer <= 0)
		{
			Spawn();
			timer = shootTimer;
		}
    }
	
	private void Spawn()
	{
        GameObject spawnpoint = centralSpawner;
		Debug.Log("The Cannon has been shoot!");
        GameObject newObject = Instantiate(rock1, spawnpoint.transform.position, spawnpoint.transform.rotation) as GameObject;
        newObject.transform.parent = spawnTransform;
		Destroy(newObject, destructionTimer);
	}
}

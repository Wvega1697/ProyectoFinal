using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewSpawner : MonoBehaviour
{

    //Public
    public List<GameObject> lista = new List<GameObject>();
    public GameObject[] array = new GameObject[7];
    public Transform ND1, ND2, ND3;
    public float destructionTimer = 5f;
    public Text textList, textArray;
    int arrayPos = 0, listPos = 0;
    public GameObject self;

    private void Update()
    {
        listLogic();
        arrayLogic();
    }

    void listLogic()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            if (listPos > 0)
            {
                listPos -= 1;
            }
            else
            {
                listPos = 0;
            }
            textList.text = "" + listPos;
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            Spawn(lista[listPos]);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            if (listPos < (lista.Count - 1))
            {
                listPos += 1;
            }
            textList.text = "" + listPos;
        }
    }

    void arrayLogic()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (arrayPos > 0)
            {
                arrayPos -= 1;
            }
            else
            {
                arrayPos = 0;
            }
            textArray.text = "" + arrayPos;
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            Spawn(array[arrayPos]);
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (arrayPos < 6)
            {
                arrayPos += 1;
            }
            textArray.text = "" + arrayPos;
        }
    }

    void Spawn(GameObject _gameObject)
	{
        Transform spawnpoint = SelectSpawner();
        GameObject newObject = Instantiate(_gameObject, spawnpoint.position, spawnpoint.rotation) as GameObject;
        newObject.transform.parent = self.transform;
		Destroy(newObject, destructionTimer);
	}

    private Transform SelectSpawner()
    {
        int x = Random.Range(0, 3);
        switch (x)
        {
            case 1:
                return ND2;
            case 2:
                return ND3;
            default:
                return ND1;
        }
    }
	
	/*void Spawn2()
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
    }*/
}


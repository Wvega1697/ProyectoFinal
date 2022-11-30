using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    //Private
    Vector3 direction;

    void Start()
    {
        direction = new Vector3(0, 0, -1f);
    }

    void Update()
    {
        transform.position += direction * GameManager.instance.objectSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Finish") || col.CompareTag("Player")) 
        {
            PoolManager.instance.StoreInstance(gameObject);
            if(gameObject.CompareTag("Score"))
            {
                GameManager.instance.score += 5;
            }
        }
        
    }
}

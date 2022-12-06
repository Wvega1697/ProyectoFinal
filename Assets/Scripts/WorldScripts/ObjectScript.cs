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
        if (name.Contains("Chick")) ChickLogic();
        else transform.position += direction * GameManager.instance.objectSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Finish") || col.CompareTag("Player")) 
        {
            if(gameObject.name.Contains("Rock7") || gameObject.name.Contains("Score"))
            {
                GameManager.instance.score += 5;
            }
            PoolManager.instance.StoreInstance(gameObject);
        }

    }

    void ChickLogic()
    {
        transform.position += direction * (GameManager.instance.objectSpeed - (GameManager.instance.objectSpeed * 0.66f)) * Time.deltaTime;
        gameObject.GetComponent<Animator>().SetBool("Run", GameManager.instance.live);
        gameObject.GetComponent<Animator>().SetBool("Eat", !GameManager.instance.live);
        if (!GameManager.instance.live) gameObject.GetComponentInChildren< ParticleSystem > ().Stop();
        else gameObject.GetComponentInChildren<ParticleSystem>().Play();
    }
}

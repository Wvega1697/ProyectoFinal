using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    //Private
    GameObject player;
    GameObject[] players;
    Animator animator;
    Vector3 direction;
    //Public
    public float speed = 10f;

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerI in players)
        {
            player = playerI;
        }
        animator = player.GetComponent<Animator>();
        direction = new Vector3(0, 0, -1f);
        speed = 10f;
    }

    void Update()
    {
        if (animator.GetBool("Live"))
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Finish"))
        {
            Destroy(this.gameObject);
        }
    }
}

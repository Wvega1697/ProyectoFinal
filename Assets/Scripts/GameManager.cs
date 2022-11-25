using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ObjectPool decorationPool, treesPool, rocksPool;
    public int score = 0;
    public Text scoreText;
    public bool live;
    public float speedRestarter = 10f;

    public float objectSpeed;
    float scoreTimer = 0, scoreRestartTimer = 2.1f;
    Animator animator;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        score = 0;
        scoreTimer = 6f;
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        live = true;
        objectSpeed = speedRestarter;
    }

    // Update is called once per frame
    void Update()
    {
        live = animator.GetBool("Live");
        if (live)
        {
            scoreText.text = "Score:" + score;
            if (scoreTimer <= 0)
            {
                score += 10;
                scoreTimer = scoreRestartTimer;
            }
            else scoreTimer -= Time.deltaTime;
            
            if (objectSpeed != speedRestarter) objectSpeed = speedRestarter;
        }
        else
        {
            scoreTimer = 6f;
            score = 0;
            objectSpeed = 0;
        }
    }

    public void StoreInstance(GameObject clone)
    {
        if (clone.CompareTag("Dangerous"))
        {
            GameManager.instance.rocksPool.StoreInstance(clone);
        }
        else if (clone.CompareTag("Decoration"))
        {
            GameManager.instance.treesPool.StoreInstance(clone);
        }
        else
        {
            GameManager.instance.decorationPool.StoreInstance(clone);
        }
    }
}

using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText;
    float scoreTimer = 0, scoreRestartTimer = 2.1f;
    Animator animator;
    void Start()
    {
        score = 0;
        scoreTimer = 6f;
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("Live"))
        {
            scoreText.SetText("Score: " + score);
            if (scoreTimer <= 0)
            {
                score += 10;
                scoreTimer = scoreRestartTimer;
            }
            else
            {
                scoreTimer -= Time.deltaTime;
            }
        }
        else
        {
            scoreTimer = 6f;
            score = 0;
        }
    }
}

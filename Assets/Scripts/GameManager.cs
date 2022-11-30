using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score = 0;
    public Text scoreText;
    public bool live, hurt;
    public float speedRestarter = 10f;
    public int totalLives;
    public List<GameObject> livesImages = new List<GameObject>();
    public float objectSpeed;

    Animator animator;
    Vector3 cameraPosition = new Vector3(0, 6.05f, -11.72f);

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
        //scoreTimer = 6f;
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        live = true;
        hurt = false;
        objectSpeed = speedRestarter;
        RestartLives();
    }

    // Update is called once per frame
    void Update()
    {
        live = animator.GetBool("Live");
        if (live)
        {
            scoreText.text = "Score:" + score;
            if (objectSpeed != speedRestarter) objectSpeed = speedRestarter;
            if (hurt) Time.timeScale = 0.5f;
            else ScoreLogic();
        }
        else
        {
            score = 0;
            objectSpeed = 0;
        }
    }

    private void ScoreLogic()
    {
        if (score < 300) Time.timeScale = 1f;
        if (score >= 300 && score < 500) Time.timeScale = 1.15f;
        if (score >= 500 && score < 750) Time.timeScale = 1.25f;
        if (score >= 750 && score < 1000) Time.timeScale = 1.5f;
        if (score >= 1000 && score < 1300) Time.timeScale = 1.75f;
        if (score >= 1300 && score < 1500) Time.timeScale = 2f;
        if (score >= 1500 && score < 1750) Time.timeScale = 2.15f;
        if (score >= 1750 && score < 2000) Time.timeScale = 2.25f;
        if (score >= 2000 && score < 2300) Time.timeScale = 2.5f;
        if (score >= 2300 && score < 2500) Time.timeScale = 2.75f;
        if (score >= 2500) Time.timeScale = 3f;
    }

    public bool Hurt()
    {
        totalLives -= 1;
        livesImages[totalLives].GetComponent<Animator>().SetTrigger("Hurt");
        if (totalLives < 1) live = false;
        return live;
    }

    public void RestartLives()
    {
        totalLives = livesImages.Count;
        for (int i = 0; i < livesImages.Count; i++)
        {
            livesImages[i].SetActive(false);
            livesImages[i].GetComponent<Animator>().ResetTrigger("Hurt");
            livesImages[i].SetActive(true);
        }
    }
}

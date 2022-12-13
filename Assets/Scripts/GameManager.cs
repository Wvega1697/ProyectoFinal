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
    public bool pause;
    public GameObject restartImage, pauseImage;
    public int level;
    public Button yes, no;

    Animator animator;
    bool pausePosition;
    //Vector3 cameraPosition = new Vector3(0, 6.05f, -11.72f);

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
        pause = false;
        pausePosition = false;
    }

    // Update is called once per frame
    void Update()
    {
        live = animator.GetBool("Live");
        if (pause)
        {
            objectSpeed = 0;
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                if (pausePosition)
                {
                    yes.Select();
                    pausePosition = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if (!pausePosition)
                {
                    no.Select();
                    pausePosition = true;
                }
            }
        }
        else if(live)
        {
            scoreText.text = "Score:" + score;
            if (objectSpeed != speedRestarter) objectSpeed = speedRestarter;
            if (hurt)
            {
                Time.timeScale = 0.25f;
            }
            else ScoreLogic();
            restartImage.SetActive(false);
        }
        else
        {
            score = 0;
            objectSpeed = 0;
            restartImage.SetActive(true);
            if (Input.GetKeyDown(KeyCode.X))
            {
                exit();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) && live)
        {
            pause = !pause;
            pauseImage.SetActive(pause);
            if (pause)
            {
                yes.Select();
                pausePosition = false;
            }
        }
    }

    public void noPause()
    {
        pause = false;
        ScoreLogic();
    }

    public void exit()
    {
        pause = false;
        Time.timeScale = 1f;
        objectSpeed = 0;
        TransitionScript.instance.LoadSpecificLevel(0);
    }

    private void ScoreLogic()
    {
        if (score < 250) Time.timeScale = 1f;
        if (score >= 250 && score < 500) Time.timeScale = 1.25f;
        if (score >= 500 && score < 750) Time.timeScale = 1.5f;
        if (score >= 750 && score < 1000) Time.timeScale = 1.75f;
        if (score >= 1000 && score < 1300) Time.timeScale = 2f;
        if (score >= 1300 && score < 1500) Time.timeScale = 2.25f;
        if (score >= 1500 && score < 1750) Time.timeScale = 2.5f;
        if (score >= 1750 && score < 2000) Time.timeScale = 2.75f;
        if (score >= 2000 && score < 2300) Time.timeScale = 3f;
        if (score >= 2300 && score < 2500) Time.timeScale = 3.25f;
        if (score >= 2500) Time.timeScale = 3.5f;
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

    public void AddLive()
    {
        if (totalLives < livesImages.Count)
        {
            totalLives += 1;
            for (int i = 0; i < totalLives; i++)
            {
                livesImages[i].SetActive(false);
                livesImages[i].GetComponent<Animator>().ResetTrigger("Hurt");
                livesImages[i].SetActive(true);
            }
        }
    }
}

using System;
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
        else if (name.Contains("Boss")) BossLogic();
        else if(!gameObject.CompareTag("MonsterBody")) transform.position += direction * GameManager.instance.objectSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider col)
    {
        if (gameObject.name.Contains("Monster"))
        {
            if (col.CompareTag("Finish"))
            {
                PoolManager.instance.StoreInstance(gameObject);
            }
            else if (col.CompareTag("Player"))
            {
                gameObject.GetComponent<Animator>().SetFloat("Multi", 1/(Time.timeScale!=0? Time.timeScale:1));
                gameObject.GetComponent<Animator>().SetBool("Attack2", true);
            }
        }
        else if (gameObject.name.Contains("Boss"))
        {
            if (col.CompareTag("Finish"))
            {
                PoolManager.instance.StoreInstance(gameObject);
                WolfLevel3Script.instance.BossSounds(false);
            }
            else if (col.CompareTag("Player"))
            {
                gameObject.GetComponent<Animator>().SetFloat("Multi", 1 / (Time.timeScale != 0 ? Time.timeScale : 1));
                gameObject.GetComponent<Animator>().SetBool("Cast Spell", true);
                WolfLevel3Script.instance.BossSounds(false);
            }
        }
        else if(gameObject.name.Contains("Rock") || gameObject.name.Contains("Score"))
        {
            if (col.CompareTag("Finish") || col.CompareTag("Player"))
            {
                if (gameObject.name.Contains("Rock7") || gameObject.name.Contains("Score"))
                {
                    if (GameManager.instance.level != 2) GameManager.instance.score += (15 / GameManager.instance.level);
                    else GameManager.instance.score += 10;
                }
                PoolManager.instance.StoreInstance(gameObject);
            }
            else if (col.CompareTag("Boss"))
            {
                PoolManager.instance.StoreInstance(gameObject);
            }
        }
        else if (col.CompareTag("Finish") || col.CompareTag("Player"))
        {
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

    private void BossLogic()
    {
        transform.position += direction * GameManager.instance.objectSpeed * 2.5f * Time.deltaTime;
        gameObject.GetComponent<Animator>().SetBool("Run Backward", GameManager.instance.live);
        gameObject.GetComponent<Animator>().SetBool("Defend", !GameManager.instance.live);
    }
}

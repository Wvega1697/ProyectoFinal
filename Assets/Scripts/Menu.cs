using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public bool hawl;
    public GameObject magic;
    public List<Button> buttons = new List<Button>();
    public Animator animator;
    float timer = 1.5f;
    void Start()
    {
        animator = GetComponent<Animator>();
        hawl = false;

    }

    void Update()
    {
        hawl = magic.activeSelf;
        animator.SetBool("Hawl", hawl);
        if (hawl)
        {
            timer -= Time.deltaTime;
            buttons[0].interactable = false;
        }
        if (timer <= 0) Debug.Log("FINISH!"); timer = 100f;
    }
}

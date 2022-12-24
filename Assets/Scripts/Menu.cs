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
    public Button start, exit;
    bool position;
    void Start()
    {
        animator = GetComponent<Animator>();
        hawl = false;
        start.Select();
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
        if(Time.timeScale != 1) Time.timeScale = 1f;
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (position)
            {
                start.Select();
                position = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (!position)
            {
                exit.Select();
                position = true;
            }
        }
    }
}

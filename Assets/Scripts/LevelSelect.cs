using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public Animator lobo1;
    public Animator lobo2;
    public Animator lobo3;
    public Animator selector1;
    public Animator selector2;
    public Animator selector3;

    int position;

    private void Start()
    {
        position = 1;
    }

    void Update()
    {
        PositionLogic();
        LevelSelectLogic();
    }

    private void LevelSelectLogic()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TransitionScript.instance.LoadSpecificLevel(SceneManager.GetActiveScene().buildIndex + position);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TransitionScript.instance.LoadSpecificLevel(0);
        }
    }

    private void PositionLogic()
    {
        if (position == 2)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                position = 1;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                position = 3;
            }
            lobo1.SetBool("Selected", false);
            lobo2.SetBool("Selected", true);
            lobo3.SetBool("Selected", false);
            selector1.SetBool("Selected", false);
            selector2.SetBool("Selected", true);
            selector3.SetBool("Selected", false);
        }
        if (position == 3)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                position = 2;
            }
            lobo1.SetBool("Selected", false);
            lobo2.SetBool("Selected", false);
            lobo3.SetBool("Selected", true);
            selector1.SetBool("Selected", false);
            selector2.SetBool("Selected", false);
            selector3.SetBool("Selected", true);
        }
        if (position == 1)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                position = 2;
            }
            lobo1.SetBool("Selected", true);
            lobo2.SetBool("Selected", false);
            lobo3.SetBool("Selected", false);
            selector1.SetBool("Selected", true);
            selector2.SetBool("Selected", false);
            selector3.SetBool("Selected", false);
        }
    }
}

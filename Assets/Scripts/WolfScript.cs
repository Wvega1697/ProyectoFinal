using UnityEngine;

public class WolfScript : MonoBehaviour
{
    //Private
    Animator animator;
    bool jump = true;
    float jumpTimer = 0, jumpRestartTimer = 1f;
    float dodgeTimer = 0, dodgeRestartTimer = 1f;
    bool dodge = false;
    //Public
    public Rigidbody rb;
    public GameObject _magic;
    public int pos;
    public Vector3 _position  = new Vector3(-0.1f, 0.61f, -1.5f);

    void Start()
    {
        animator = GetComponent<Animator>();
        pos = 0;
    }

    void Update()
    {
        JumpLogic();
        if (jump) MoveLogic();
        if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            animator.SetBool("Die", true);
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            animator.SetBool("Die", false);
        }
    }

    void DodgeLogic()
    {
        if (dodgeTimer > 0)
        {
            if (dodgeTimer < 0.85f)
            {
                dodge = false;
                animator.SetBool("DodgeLeft", dodge);
                animator.SetBool("DodgeRight", dodge);
            }
            dodgeTimer -= Time.deltaTime;
        }
        else 
        {
            if (jump) MoveLogic();
        }
    }

    void MoveLogic()
    {
        if (pos > -1)
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("Left");
                pos -= 1;
                dodge = true;
                animator.SetBool("DodgeLeft", dodge);
                dodgeTimer = dodgeRestartTimer;
            }
        }

        if (pos < 1)
        {
            if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("Right");
                pos += 1;
                dodge = true;
                animator.SetBool("DodgeRight", dodge);
                dodgeTimer = dodgeRestartTimer;
            }
        }

        if (pos < -1) pos = -1;
        if (pos > 1) pos = 1;

        if(dodge)
        {
            switch (pos)
            {
            case -1:
                _position.x = -2f;
                break;
            case 1:
                _position.x = 2f;
                break;
            default:
                _position.x = -0.1f;
                break;
            }
            transform.position = _position;
        }
        dodge = false;
    }

    void JumpLogic()
    {
        if(jumpTimer <= 0)
        {
            jump = true;
            _magic.SetActive(jump);
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                jumpTimer = jumpRestartTimer;
                animator.SetBool("Jump", jump);
            }
        }
        else 
        {
            jump = false;
            if (jumpTimer < 0.73f) animator.SetBool("Jump", jump);
            jumpTimer -= Time.deltaTime;
            _magic.SetActive(jump);
        }
    }
    
}

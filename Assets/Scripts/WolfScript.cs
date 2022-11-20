using UnityEngine;

public class WolfScript : MonoBehaviour
{
    //Private
    Animator animator;
    bool jump = false;
    float jumpTimer = 0, jumpRestartTimer = 0.7f;
    float respawnTimer = 0, respawnRestartTimer = 3f;
    float inmortalTimer = 0, inmortalRestartTimer = 0.5f;
    bool dodge = false;
    //Public
    public Rigidbody rb;
    public GameObject _magic;
    public int pos;
    public Vector3 _position  = new Vector3(-0.1f, 0.61f, -1.5f);
    public AudioSource audioBark, audioDoubleBark, music, audioDie;
    public GameObject ReintentarImage;
    public TrailRenderer trail;
    void Start()
    {
        animator = GetComponent<Animator>();
        pos = 0;
        respawnTimer = respawnRestartTimer;//Time.timeScale = 0;
        trail.emitting = true;
    }

    void Update()
    {
        if (animator.GetBool("Live"))
        {
            JumpLogic();
            //if (!jump)
            MoveLogic();
        }
        else if (respawnTimer <= 0)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                inmortalTimer = inmortalRestartTimer;
                RespawnLogic();
            }
        }
        else
        {
            respawnTimer -= Time.deltaTime;
            ReintentarImage.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Time.timeScale += 0.25f;
        }
        if (inmortalTimer > 0)
        {
            inmortalTimer -= Time.deltaTime;
        }
    }

    void MoveLogic()
    {
        if (pos > -1)
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                pos -= 1;
                dodge = true;//animator.SetBool("DodgeLeft", dodge);dodgeTimer = dodgeRestartTimer;
                audioBark.Play();
            }
        }

        if (pos < 1)
        {
            if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                pos += 1;
                dodge = true;//animator.SetBool("DodgeRight", dodge); dodgeTimer = dodgeRestartTimer;
                audioBark.Play();
            }
        }

        if (pos < -1) pos = -1;
        if (pos > 1) pos = 1;

        if(dodge)
        {
            SetPosition();
        }
        dodge = false;
    }

    void SetPosition()
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

    void JumpLogic()
    {
        if(jumpTimer <= 0)
        {
            jump = false;
            trail.emitting = true;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                jump = true;
                jumpTimer = jumpRestartTimer;
                audioDoubleBark.Play();
                trail.emitting = false;
            }
            animator.SetBool("Jump", jump);
            _magic.SetActive(!jump);
        }
        else 
        {
            jumpTimer -= Time.deltaTime;
        }
    }
    
    void OnTriggerEnter(Collider col)
    {
        if(col.transform.gameObject.CompareTag("Dangerous") &&
            animator.GetBool("Live") &&
            inmortalTimer <= 0)
        {
            animator.SetBool("Live", false);
            animator.SetBool("Jump", false);
            music.Stop();
            audioDie.Play();
            jump = false;
            _magic.SetActive(false);
            Time.timeScale = 1;
        }
    }

    void RespawnLogic()
    {
        animator.SetBool("Live", true);
        music.Play();
        audioDoubleBark.Play();
        audioDie.Stop();
        ReintentarImage.SetActive(false);
        respawnTimer = respawnRestartTimer;
    }
}

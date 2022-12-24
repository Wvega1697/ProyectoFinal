using UnityEngine;

public class WolfScript : MonoBehaviour
{
    //Private
    Animator animator;
    bool jump = false;
    //float jumpTimer = 0, jumpRestartTimer = 0.7f;
    int audioJumpInt = 1;
    float respawnTimer = 0, respawnRestartTimer = 3f;
    float inmortalTimer = 0, inmortalRestartTimer = 0.3f;
    bool dodge = false;
    //Public
    public Rigidbody rb;
    public int pos;
    public Vector3 _position = new Vector3(-0.1f, 0.61f, -1.5f);
    public AudioSource audioMove, audioJump, music, audioDie, audioHurt, audioChick, audioScore, audioBark, audioDoubleBark;
    public GameObject ReintentarImage, _magic, starB, heart1, heart2, grass, chick1, chick2, chick3;
    public TrailRenderer trail;
    void Start()
    {
        animator = GetComponent<Animator>();
        pos = 0;
        respawnTimer = respawnRestartTimer;
        trail.emitting = true;
        starB.SetActive(false);
    }

    void Update()
    {
        if (GameManager.instance.live)
        {
            JumpLogic();
            if (!jump) MoveLogic();
            if (!music.isPlaying && inmortalTimer <= 0) music.Play();
        }
        else if (respawnTimer <= 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                heart1.GetComponent<ParticleSystem>().Play();
                heart2.GetComponent<ParticleSystem>().Play();
                inmortalTimer = inmortalRestartTimer;
                RespawnLogic();
            }
        }
        else
        {
            grass.GetComponent<ParticleSystem>().Stop();
            respawnTimer -= Time.deltaTime;
            ReintentarImage.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Time.timeScale += 0.25f;
            Debug.Log("New time scale: " + Time.timeScale);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            audioChick.Play();
        }
        if (inmortalTimer > 0)
        {
            SetPosition();
            inmortalTimer -= Time.deltaTime;
        }
        else
        {
            GameManager.instance.hurt = false;
            starB.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameManager.instance.totalLives += 1;
            Debug.Log("Lives: " + GameManager.instance.totalLives);
        }
    }

    void MoveLogic()
    {
        if (pos > -1)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                pos -= 1;
                dodge = true;//animator.SetBool("DodgeLeft", dodge);dodgeTimer = dodgeRestartTimer;
                audioMove.Play();
            }
        }

        if (pos < 1)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                pos += 1;
                dodge = true;//animator.SetBool("DodgeRight", dodge); dodgeTimer = dodgeRestartTimer;
                audioMove.Play();
            }
        }

        if (pos < -1) pos = -1;
        if (pos > 1) pos = 1;

        if (dodge)
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
        jump = !IsTouchingGround();
        if (!jump)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if(!audioJump.isPlaying) audioJump.Play();
                grass.GetComponent<ParticleSystem>().Play();
                animator.SetBool("Jump", true);
            }
        }
        else
        {
            grass.GetComponent<ParticleSystem>().Stop();
            animator.SetBool("Jump", false);
        }
        trail.emitting = !jump;
        _magic.SetActive(!jump);
    }

    bool IsTouchingGround()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out RaycastHit hitInfo, 0.15f))
        {
            return hitInfo.collider.gameObject.CompareTag("Floor");
        }
        else
        {
            return false;
        }
    }
    
    void OnTriggerEnter(Collider col)
    {
        if(col.transform.gameObject.CompareTag("Dangerous") &&
            animator.GetBool("Live") &&
            inmortalTimer <= 0)
        {
            GameManager.instance.hurt = true;
            starB.SetActive(true);
            jump = false;
            _magic.SetActive(false);
            inmortalTimer = inmortalRestartTimer;
            if (!GameManager.instance.Hurt())
            {
                animator.SetBool("Live", false);
                animator.SetBool("Jump", jump);
                music.Stop();
                audioDie.Play();
            }
            else
            {
                animator.SetTrigger("Hurt");
                animator.SetBool("Jump", jump);
                music.Pause();
                audioHurt.Play();
            }
        }
        if (col.transform.gameObject.CompareTag("Chick"))
        {
            chick1.GetComponent<ParticleSystem>().Play();
            chick2.GetComponent<ParticleSystem>().Play();
            chick3.GetComponent<ParticleSystem>().Play();
            audioChick.Play();
            audioScore.Play();
            audioBark.Play();
            GameManager.instance.score += 25;
        }
    }

    void RespawnLogic()
    {
        animator.SetBool("Live", true);
        music.Play();
        audioJump.Play();
        audioDie.Stop();
        ReintentarImage.SetActive(false);
        respawnTimer = respawnRestartTimer;
        GameManager.instance.RestartLives();
    }
}

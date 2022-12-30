using UnityEngine;

public class WolfLevel3Script : MonoBehaviour
{
    public static WolfLevel3Script instance;
    //Private
    Animator animator;
    bool jump = false;
    float respawnTimer = 0, respawnRestartTimer = 3f;
    float inmortalTimer = 0, inmortalRestartTimer = 0.5f;
    bool dodge = false;
    //Public
    public Rigidbody rb;
    public int pos;
    public Vector3 _position = new Vector3(-0.1f, 0.61f, -1.5f);
    public AudioSource audioMove, audioJump, music, audioDie, audioHurt, audioChick, audioScore, audioBark, audioMonster, audioRocks;
    public GameObject _magic, starB, heart1, heart2, grass, chick1, chick2, chick3, destroyer2;
    public TrailRenderer trail;

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
        animator = GetComponent<Animator>();
        pos = 0;
        respawnTimer = respawnRestartTimer;
        trail.emitting = true;
        starB.SetActive(false);
    }

    void Update()
    {
        if (GameManager.instance.live && !GameManager.instance.pause)
        {
            if (!GameManager.instance.hurt) JumpLogic();
            MoveLogic();
            if (inmortalTimer > 0)
            {
                SetPosition();
                inmortalTimer -= Time.deltaTime;
                destroyer2.transform.localScale = new Vector3(4.5f, 3f, 25f);
            }
            else
            {
                starB.SetActive(false);
                if (!music.isPlaying) music.Play();
                GameManager.instance.hurt = false;
                destroyer2.transform.localScale = new Vector3(4.5f, 3f, 5f);
            }
        }
        else if (respawnTimer <= 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                heart1.GetComponent<ParticleSystem>().Play();
                heart2.GetComponent<ParticleSystem>().Play();
                inmortalTimer = inmortalRestartTimer;
                RespawnLogic();
                destroyer2.transform.localScale = new Vector3(4.5f, 3f, 55f);
            }
            starB.SetActive(false);
        }
        else
        {
            grass.GetComponent<ParticleSystem>().Stop();
            respawnTimer -= Time.deltaTime;
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
        if(jump) _position.y = transform.position.y;
        else _position.y = 0.61f;
        transform.position = _position;
    }

    void JumpLogic()
    {
        jump = !IsTouchingGround();
        if (!jump)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (!audioJump.isPlaying) audioJump.Play();
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
        if ((col.transform.gameObject.CompareTag("Dangerous") || col.transform.gameObject.CompareTag("Boss") || 
            col.transform.gameObject.CompareTag("MonsterBody")) && animator.GetBool("Live") && inmortalTimer <= 0)
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
        else if (col.transform.gameObject.CompareTag("Chick"))
        {
            chick1.GetComponent<ParticleSystem>().Play();
            chick2.GetComponent<ParticleSystem>().Play();
            chick3.GetComponent<ParticleSystem>().Play();
            audioChick.Play();
            audioScore.Play();
            audioBark.Play();
            GameManager.instance.score += 25;
            GameManager.instance.AddLive();
        }
    }

    void RespawnLogic()
    {
        animator.SetBool("Live", true);
        music.Play();
        audioJump.Play();
        audioDie.Stop();
        respawnTimer = respawnRestartTimer;
        GameManager.instance.RestartLives();
    }

    public void BossSounds(bool play)
    {
        if (play)
        {
            audioMonster.Play();
            audioRocks.Play();
        }
        else
        {
            audioMonster.Stop();
            audioRocks.Stop();
        }
        
    }
}
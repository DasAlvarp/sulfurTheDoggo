using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
	public enum PlayerState {
		DEFAULT,
        DODGESTART,
        DODGING,
        DODGEREC,
        DAMAGED,
	}

    //character states, in case we want to add stuff like a dash having invul, etc.
    public PlayerState charState = PlayerState.DEFAULT;

    public float speed;
    public float dodgeSpeed = 10f;
    public float statelock = 0;
    public float dodgeTime = .3f;
    public float damageInvul = .5f;

    public int playerHP = 3;

    public Material playerMat;

    public Color glowColor;
    public Color dodgeColor;
    public Color invulColor;

    Rigidbody rigid;
    InputManager inputs;

    //dodge vector
    private Vector3 _dodgeVec;


    // Use this for initialization
    void Start ()
    {
        playerMat.color = glowColor;
        rigid = GetComponent<Rigidbody>();
        inputs = GetComponent<InputManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        dodgeVector = dodgeVector;
        switch(charState)
        {
            case PlayerState.DEFAULT:
                Move();
                break;
            case PlayerState.DODGESTART:
                DodgeInit();
                break;
            case PlayerState.DODGING:
                Dodge();
                break;
            case PlayerState.DODGEREC:
                DodgeRec();
                break;
            case PlayerState.DAMAGED:
                TakeDamage();
                break;
        }
		
	}

    //Set up movement for dodge. 
    void DodgeInit()
    {
        playerMat.color = dodgeColor;
        charState = PlayerState.DODGING;
        statelock = dodgeTime;

        rigid.velocity = dodgeVector ;
    }

    //dodge animation and state
    void Dodge()
    {
        statelock -= Time.deltaTime;
        if (statelock < 0f)
        {
            charState = PlayerState.DODGEREC;
        }
    }

    //startup, recovery and dodge. If I were doing this in a project with a larger moveset, a spreadsheet would be doing the thinking here. Rn recover and start is 1f, but adding in those is pretty easy.
    void DodgeRec()
    {
        playerMat.color = glowColor;
        rigid.velocity = Vector3.zero;
        charState = PlayerState.DEFAULT;
    }

    //Damage taking and statelock
    void TakeDamage()
    {
        playerMat.color = invulColor;
        statelock -= Time.deltaTime;
        Move();
        if (statelock < 0f)
        {
            playerMat.color = glowColor;
            charState = PlayerState.DEFAULT;
        }
    }

    //Standard movement
    void Move()
    {
        rigid.velocity = (new Vector3(inputs.GetAxis("Horizontal"), 0, inputs.GetAxis("Vertical"))) * speed;

        if(Input.GetButtonDown("Dodge"))
        {
            charState = PlayerState.DODGESTART;
        }
    }

    //"handling" damage
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Bullet")
        {
            if(charState != PlayerState.DODGING && charState != PlayerState.DAMAGED)
            {
                if (playerHP <= 0)
                {
                    print("You are already dead");
                    SceneManager.LoadScene(2);
                }
                else
                {
                    print("Damaged");
                    statelock = damageInvul;
                    charState = PlayerState.DAMAGED;
                    playerHP--;
                }
            }
            else
            {
                print("Dodged/Invul'd");
            }
        }
    }


    Vector3 dodgeVector
    {
        get
        {
            Vector3 move = new Vector3(inputs.GetAxis("Horizontal"), 0, inputs.GetAxis("Vertical")).normalized * dodgeSpeed;
            if (move.magnitude < speed)
            {
                return _dodgeVec;
            }
            else
            {
                _dodgeVec = move;
                return move;
            }
        }

        set
        {
            _dodgeVec = value;
        }
    }
}

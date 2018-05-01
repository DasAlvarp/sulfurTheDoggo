/* Author: Alvaro Gudiswitz
 * Date Created: 3/ /2018
 * Date Modified: 4/3/2018
 * Modified By: Alvaro Gudiswitz
 * Description: Player Control Behavior
 */
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
		HITBOSS,
	}

    //character states, in case we want to add stuff like a dash having invul, etc.
    public PlayerState charState = PlayerState.DEFAULT;

    public float speed;
    public float dodgeSpeed = 10f;
	public float resetSpeed = 20f;
    public float statelock = 0;
    public float dodgeTime = .3f;
    public float dodgeRecovery = 1f;
    public float damageInvul = .5f;

	public Vector3 resetPoint;

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
			case PlayerState.HITBOSS:
				MoveToReset();
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
            statelock = dodgeRecovery;
        }
    }

    //startup, recovery and dodge. If I were doing this in a project with a larger moveset, a spreadsheet would be doing the thinking here. Rn recover and start is 1f, but adding in those is pretty easy.
    void DodgeRec()
    {
        playerMat.color = glowColor;
        statelock -= Time.deltaTime;
        Move();
        if (statelock < 0f)
        {
            charState = PlayerState.DEFAULT;
        }
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

        if(Input.GetButtonDown("Dodge") && charState != PlayerState.DODGEREC)
        {
            charState = PlayerState.DODGESTART;
        }
    }

	void MoveToReset() 
	{
		playerMat.color = glowColor;
		resetPoint.y = transform.position.y;
		rigid.velocity = (resetPoint - transform.position).normalized * resetSpeed;

		if (Vector3.Distance(transform.position, resetPoint) < 2) 
		{
			rigid.velocity = Vector3.zero;
			charState = PlayerState.DEFAULT;
		}
	}

    //"handling" damage
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Bullet")
        {
            if(charState != PlayerState.DODGING && charState != PlayerState.DAMAGED && charState != PlayerState.HITBOSS)
            {
                if (playerHP <= 0)
                {
                    print("You are already dead");
                    SceneManager.LoadScene(2);
                    EnemyHP.bossHP = 7;
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

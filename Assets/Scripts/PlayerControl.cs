﻿using System.Collections;
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
	}

    //character states, in case we want to add stuff like a dash having invul, etc.
    public PlayerState charState = PlayerState.DEFAULT;

    public float speed;
    public float statelock = 0;
    public float dodgeTime = .3f;
    public int playerHP = 3;

    public Material playerMat;

    public Color glowColor;
    public Color dodging;
    public Color invul;

    Rigidbody rigid;
    InputManager inputs;


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
        }
		
	}

    //Set up movement for dodge. 
    void DodgeInit()
    {
        playerMat.color = dodging;
        charState = PlayerState.DODGING;
        statelock = dodgeTime;

        //dodge to constant speed
        if (rigid.velocity.magnitude > speed)
        {
            rigid.velocity = rigid.velocity.normalized * speed;
        }
        else
        {
            rigid.velocity = (new Vector3(inputs.GetAxis("Horizontal"), 0, inputs.GetAxis("Vertical"))).normalized * speed;
        }
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

    //Standard movement
    void Move()
    {
        if (rigid.velocity.magnitude > speed)
        {
            rigid.velocity = rigid.velocity.normalized * speed;
        }
        else
        {
            rigid.velocity = (new Vector3(inputs.GetAxis("Horizontal"), 0, inputs.GetAxis("Vertical"))) * speed;
        }

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
            if(charState != PlayerState.DODGING)
            {
                if (playerHP <= 0)
                {
                    print("You are already dead");
                    SceneManager.LoadScene(2);
                }
                else
                {
                    print("Damaged");
                    playerHP--;
                }
            }
            else
            {
                print("Dodged");
            }
        }
    }
}

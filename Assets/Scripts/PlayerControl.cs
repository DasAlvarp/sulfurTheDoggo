using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	public enum PlayerState {
		DEFAULT
	}
    //character states, in case we want to add stuff like a dash having invul, etc.
    public PlayerState charState = PlayerState.DEFAULT;

    public float speed;

    Rigidbody rigid;
    InputManager inputs;


    // Use this for initialization
    void Start ()
    {
        rigid = GetComponent<Rigidbody>();
        inputs = GetComponent<InputManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        switch(charState)
        {
            case 0:
                Move();
                break;
        }
		
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
    }
}

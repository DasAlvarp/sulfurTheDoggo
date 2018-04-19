/* Author: Alexander Tang
 * Date Created: 3/31/2018
 * Date Modified: 4/5/2018
 * Modified By: Alexander Tang
 * Description: Gallium Boss Behavior
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GalliumMovement : MonoBehaviour {

    public enum GalliumState
    {
        SEARCH,
        PURSUE,
        CHANGEFORM
    }

    public GalliumState galliumState = GalliumState.SEARCH;
    public Transform player;
    public float searchRadius = 50f;
    public float speed = 5f;
    public Vector3 moveDir;
    public LayerMask wall;
    public float distFromWall = 0f;

    Rigidbody galliumRigid;
	// Use this for initialization
	void Start () {
        galliumRigid = GetComponent<Rigidbody>();
        moveDir = ChooseDirection();
        transform.rotation = Quaternion.LookRotation(moveDir);
    }
	
	// Update is called once per frame
	void Update () {
        switch (galliumState)
        {
            case GalliumState.SEARCH:
                Search();
                break;
            case GalliumState.PURSUE:
                Pursue();
                break;
            case GalliumState.CHANGEFORM:
                ChangeForm();
                break;
        }
    }
    
    void Search()
    {
        float playerDistance = Vector3.Distance(transform.position, player.position);
        if (playerDistance < searchRadius)
        {
            galliumState = GalliumState.PURSUE;
        }
        else
        {
            galliumRigid.velocity = moveDir * speed;
            if (Physics.Raycast(transform.position, transform.forward, distFromWall, wall))
            {
                moveDir = ChooseDirection();
                transform.rotation = Quaternion.LookRotation(moveDir);
            }
        }
    }

    void Pursue()
    {
        float playerDistance = Vector3.Distance(transform.position, player.position);
        if (playerDistance < searchRadius)
        {
            Vector3 playerDir = player.position - transform.position;
            float angle = Mathf.Atan2(playerDir.y, playerDir.x) * Mathf.Rad2Deg - 90f;
            Quaternion turn = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, turn, 180);
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        else
        {
            galliumState = GalliumState.SEARCH;
        }
    }

    void ChangeForm()
    {
        galliumState = GalliumState.SEARCH;
    }

    Vector3 ChooseDirection()
    {
        System.Random ran = new System.Random();
        int i = ran.Next(0, 3);
        Vector3 temp = new Vector3();

        if (i == 0)
        {
            temp = transform.forward;
        }
        else if (i == 1)
        {
            temp = -transform.forward;
        }
        else if (i == 2)
        {
            temp = transform.right;
        }
        else if (i == 3)
        {
            temp = -transform.right;
        }
        return temp;
    }
}

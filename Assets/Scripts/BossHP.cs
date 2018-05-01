using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Author: Javier Bernal    
 * Date Created: 
 * Date Modified: 5/1/18 by Alvaro Gudiswitz
 * Description: This script controls a boss's hp as well as the progress into
 *              different stages of the boss.
 */
public class BossHP : MonoBehaviour {

    public static int HP = 5;  //boss default HP
	private bool stage1;        //stage1-4 are set true if that stage is active. only one is true at a time
	private bool stage2;        
	private bool stage3;
	private bool stage4;
    private bool stage5;
    public float speed = 1;
    public GameObject prompt;

    public Transform player;

    private bool canBeHit = false;
	public GameObject[] stages = new GameObject[5];
	// Use this for initialization
	void Start () {
        prompt.SetActive(false);
		stage1 = true;
		stage2 = false;
		stage3 = false;
		stage4 = false;
        stage5 = false;
		SelectStage (0);
	}
	
	// Update is called once per frame
	void Update () {
        print(HP);
        // when the boss takes enough damage check and see if the stage needs to change
        if (HP <= 5 && stage1)
        {
            stage1 = false;
            stage2 = true;
            SelectStage(1);
            print("st1");
        }
        else if (HP <= 4 && stage2)
        {
            stage2 = false;
            stage3 = true;
            print("st2");
            SelectStage(2);
        }
        else if (HP <= 3 && stage3)
        {
            stage3 = false;
            stage4 = true;
            print("st3");
            SelectStage(3);
        }
        else if (HP <= 2 && stage4)
        {
            stage4 = false;
            stage5 = true;
            print("st4");
            SelectStage(4);
        }
        else if(HP <= 1 && stage5)
        {
            print("st5");
            stages[5].SetActive(true);
        }

        if(canBeHit)
        {
            if(Input.GetButton("HitBoss"))
            {
				canBeHit = false;
                print("BANG!");
                HP--;
				player.GetComponent<PlayerControl>().charState = PlayerControl.PlayerState.HITBOSS;
            }
        }

	}
    // Use this to change the stage to the appropriate one.
	public void SelectStage(int stg){
		for (int i = 0; i < stages.Length; i++) {
			if (i == stg) {
				stages [i].SetActive (true);
			} else {
				stages [i].SetActive (false);
			}
		}
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            player = other.transform;
            prompt.SetActive(true);
            canBeHit = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.transform.tag == "Player")
        {
            canBeHit = false;
            prompt.SetActive(false);
        }
    }
}

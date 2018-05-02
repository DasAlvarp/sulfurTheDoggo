using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Author: Javier Bernal    
 * Date Created: 
 * Date Modified: 5/1/18 by Aric Hasting
 * Description: This script controls a boss's hp as well as the progress into
 *              different stages of the boss.
 */
public class BossHP : MonoBehaviour {

	private int currentStage = 0;
    public float speed = 1;
    public GameObject prompt;

    public Transform player;

    private bool canBeHit = false;
	public GameObject[] stages = new GameObject[5];
	// Use this for initialization
	void Start () {
        prompt.SetActive(false);
		SelectStage(currentStage);
	}
	
	// Update is called once per frame
	void Update () { 
        if(canBeHit)
        {
            if(Input.GetButton("HitBoss"))
            {
				GetComponent<AudioSource>().Play();
				canBeHit = false;
				currentStage++;
				SelectStage(currentStage);
				player.GetComponent<PlayerControl>().charState = PlayerControl.PlayerState.HITBOSS;
            }
        }
	}

    // Use this to change the stage to the appropriate one.
	public void SelectStage(int stg){
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach(GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
		for (int i = 0; i < stages.Length; i++) {
			if (i == stg) {
				stages[i].SetActive(true);
			} else {
				stages[i].SetActive(false);
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

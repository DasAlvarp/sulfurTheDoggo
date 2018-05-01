using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Author: Javier Bernal    
 * Date Created: 
 * Date Modified: 
 * Description: This script controls a boss's hp as well as the progress into
 *              different stages of the boss.
 */
public class BossHP : MonoBehaviour {

    public static int HP = 15;  //boss default HP
	private bool stage1;        //stage1-4 are set true if that stage is active. only one is true at a time
	private bool stage2;        
	private bool stage3;
	private bool stage4;
	public GameObject[] stages = new GameObject[5];
	// Use this for initialization
	void Start () {
        HP = 15;
		stage1 = true;
		stage2 = false;
		stage3 = false;
		stage4 = false;
		SelectStage (0);
	}
	
	// Update is called once per frame
	void Update () {
        // when the boss takes enough damage check and see if the stage needs to change
		if (HP <= 10 && stage1) {
			stage1 = false;
			stage2 = true;
			SelectStage (1);
		}
		else if(HP<=6 && stage2){
			stage2 = false;
			stage3 = true;
			SelectStage (2);
		}
		else if(HP<=2 && stage3){
			stage3 = false;
			stage4 = true;
			SelectStage (3);
		}
        else if(HP<=0 && stage4)
        {
            stages[4].SetActive(true); 
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
    
}

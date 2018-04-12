﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHP : MonoBehaviour {

	public static int HP = 15;
	private bool stage1;
	private bool stage2;
	private bool stage3;
	private bool stage4;
	public GameObject[] stages = new GameObject[4];
	// Use this for initialization
	void Start () {
		stage1 = true;
		stage2 = false;
		stage3 = false;
		stage4 = false;
		SelectStage (0);
	}
	
	// Update is called once per frame
	void Update () {
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
	}

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

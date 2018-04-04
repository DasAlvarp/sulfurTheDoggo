﻿/* Author: Alvaro Gudiswitz
 * Date Created: 3/ /2018
 * Date Modified:
 * Modified By: Alvaro Gudiswitz
 * Description: Basic Player Camera Behavior
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform player;

	private float camY;

	// Use this for initialization
	void Start () {
		camY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = player.position;
		pos.y = camY;
		transform.position = pos;
	}
}

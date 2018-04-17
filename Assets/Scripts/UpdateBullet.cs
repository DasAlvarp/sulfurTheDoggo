﻿/* Author: Aric Hasting
 * Date Created: 3/ /2018
 * Date Modified: 
 * Modified By: 
 * Description: Bullet update
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateBullet : MonoBehaviour {

	public Vector3 movement;
	
	// Update is called once per frame
	void Update () {
		transform.Translate(movement * Time.deltaTime);
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Obstacle") {
			Destroy(gameObject);
		}
	}
}

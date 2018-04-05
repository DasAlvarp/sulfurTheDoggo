﻿/* Author: Alexander Tang
 * Date Created: 4/5/2018
 * Date Modified: 
 * Modified By: 
 * Description: Spiral Emitter but only within a set radius
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralOnEnter : EnemyShot {

    public Transform player;

    public float searchRadius = 50f;

    public float rotationSpeed;
	public float bulletSpeed;
	public float emissionRate;

	private float rotation = 0f;

	//controls rotation over time.
    public float FirePattern()
    {
        return Time.deltaTime * rotationSpeed;
    }

    //fireRate doesn't have to be constant.
    override
    public bool FireRate()
    {
        count += Time.deltaTime;
        if (count > emissionRate && firing)
        {
            count = 0;
            return true;
        }
        return false;
    }

	private void Update() {
		rotation += FirePattern();
        FireOnEnter();
		if (FireRate() && firing) {
			Fire();
		}
	}

	//actually shoots it. Will send vector3 over too.
	override
    public void Fire()
    {
        Transform newBull = Instantiate(bullet);
        newBull.transform.position = transform.position;
        newBull.transform.rotation = Quaternion.identity;

        //rotates in the proper direction, gets where it's facing, then rotates it.
        newBull.transform.Rotate(0, rotation, 0);
        Vector3 movement = newBull.transform.forward;
        newBull.Translate(0, 0, 1);

        newBull.transform.Rotate(0, -rotation, 0);

        
        newBull.GetComponent<UpdateBullet>().movement = movement * bulletSpeed;
    }

    public void FireOnEnter()
    {
        float playerDistance = Vector3.Distance(transform.position, player.position);
        if (playerDistance < searchRadius)
        {
            firing = true;
        }
        else
        {
            firing = false;
        }
    }
}

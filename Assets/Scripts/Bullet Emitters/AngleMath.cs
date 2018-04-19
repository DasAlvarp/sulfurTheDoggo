﻿/* Author: Aric Hasting
 * Date Created: 3/ /2018
 * Date Modified: 
 * Modified By: 
 * Description: Angular calculations
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleMath : MonoBehaviour {
	public static Vector3 AngleToVector3(float angle) {
		Vector3 vec = Vector3.zero;
		vec.x = Mathf.Cos(angle);
		vec.z = Mathf.Sin(angle);
		return vec.normalized;
	}
}

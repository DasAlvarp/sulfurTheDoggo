/* Author: Aric Hasting
 * Date Created: 3/ /2018
 * Date Modified: 
 * Modified By: 
 * Description: Abstract class for enemy and bullet behavior
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyShot : MonoBehaviour
{
    //bullet prefab
    public Transform bullet;

	protected float count;

	public bool firing = true;

	//Returns true when firing on this frame
	public abstract bool FireRate();

	//actually shoots it. Will send vector3 over too.
	public abstract void Fire();

}
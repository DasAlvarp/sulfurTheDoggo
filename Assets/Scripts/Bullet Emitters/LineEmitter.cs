using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineEmitter : EnemyShot {

	public float lineWidth;
	public float lineAngle;
	public int bulletsPerLine;
	public float bulletSpeed;

	public float fireRate;

	public Transform bulletPrefab;
	
	public override void Fire() {
		float radAngle = lineAngle * Mathf.Deg2Rad;
		for (int i = 0; i < bulletsPerLine; i++) {
			Transform newBullet = Instantiate(bulletPrefab);
			Vector3 pos = (lineWidth / bulletsPerLine) * (i - bulletsPerLine/2) * new Vector3(Mathf.Cos(radAngle), 0, Mathf.Sin(radAngle));
			pos = transform.position + pos;
			newBullet.position = pos;
			newBullet.GetComponent<UpdateBullet>().movement = new Vector3(Mathf.Cos(radAngle + 90 * Mathf.Deg2Rad), 0, Mathf.Sin(radAngle + 90 * Mathf.Deg2Rad)) * bulletSpeed;
		}
	}

	public override bool FireRate() {
		count += Time.deltaTime;
		if (count > fireRate) {
			count = 0;
			return true;
		}
		return false;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (FireRate()) {
			Fire();
		}
	}
}

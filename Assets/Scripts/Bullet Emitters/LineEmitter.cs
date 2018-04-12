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
		for (int i = 0; i < bulletsPerLine; i++) {
			Transform newBullet = Instantiate(bulletPrefab);
			Vector3 pos = (lineWidth / bulletsPerLine) * i * new Vector3(Mathf.Cos(lineAngle), 0, Mathf.Sin(lineAngle));
			pos = transform.position + pos;
			newBullet.position = pos;
			newBullet.GetComponent<UpdateBullet>().movement = new Vector3(Mathf.Cos(lineAngle + 90), 0, Mathf.Sin(lineAngle + 90)) * bulletSpeed;
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

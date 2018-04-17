using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineEmitter : EnemyShot {

	public float lineWidth;
	public float lineAngle;
	public int bulletsPerLine;
	public float bulletSpeed;

	public float fireRate;
	public float fireOffset;

	private bool isOffset = false;
	
	public override void Fire() {
		float radAngle = Mathf.Deg2Rad * lineAngle;
		for (int i = 0; i < bulletsPerLine; i++) {
			Transform newBullet = Instantiate(bullet);
			Vector3 pos = transform.forward + (lineWidth / bulletsPerLine) * (i - bulletsPerLine/2) * new Vector3(Mathf.Cos(radAngle), 0, Mathf.Sin(radAngle));
			pos = transform.position + pos;
			newBullet.position = pos;
			newBullet.GetComponent<UpdateBullet>().movement = transform.forward + new Vector3(Mathf.Cos(radAngle + 0.5f * Mathf.PI), 0, Mathf.Sin(radAngle + 0.5f * Mathf.PI)) * bulletSpeed;
		}
	}

	public override bool FireRate() {
		count += Time.deltaTime;
		if (count > fireOffset && !isOffset && fireOffset != 0) {
			isOffset = true;
			count = 0;
			return false;
		}
		if (count > fireRate && (isOffset || fireOffset == 0)) {
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

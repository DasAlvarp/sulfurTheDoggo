using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform player;
    public Transform laser;

	private float camY;

	// Use this for initialization
	void Start () {
		camY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = (player.position * 3 + laser.position) / 4;
		pos.y = camY;
		transform.position = pos;
	}
}

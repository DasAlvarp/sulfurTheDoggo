/* Author: Alvaro Gudiswitz
 * Date Created: 3/ /2018
 * Date Modified:
 * Modified By: Alvaro Gudiswitz
 * Description: Basic Player Camera Behavior
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Created by Aric Hasting 3-29-18
 * Desc: camera follows player and laser
 * Modified: Alvaro Gudiswitz 4-17-18
 */

public class CameraFollow : MonoBehaviour {

	public Transform player;
    public Transform laser;
    public Camera cam;

	private float camY;

    private float minSize = 12;
    private float maxSize = 20;

	// Use this for initialization
	void Start () {
		camY = transform.position.y;
        cam = transform.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        float magnitude = (player.position - laser.position).magnitude;
        Vector3 pos;
        if (magnitude > 8)
        {
            pos = (player.position * 3 + laser.position) / 4;
        }
        else if(magnitude > 2)
        {
            Vector3 pos1 = (player.position * 3 + laser.position) / 4;
            Vector3 pos2 = (player.position);

            float weight = (magnitude - 2) / 6 ;
            pos = pos1 * weight + pos2 * (1 - weight);
        }
        else
        {
            pos = player.position;
        }

		pos.y = camY;
		transform.position = pos;

        float xDist = Mathf.Abs(player.position.x - laser.position.x);
        float zDist = Mathf.Abs(player.position.z - laser.position.z);


        float targetSize = minSize;

        //if the camera is out of the range of effect
        if(zDist > 8 || xDist > 20)
        {
            targetSize = Mathf.Max(zDist * 7 / 8, xDist * 7/16);

            //targetSize = Mathf.Sqrt((player.position - laser.position).magnitude) + minSize;
            if(targetSize < minSize)
            {
                targetSize = minSize;
            }
        }
        else
        {
            targetSize = minSize;
        }

        if (xDist > 20)
        {
            if (targetSize > cam.orthographicSize)
            {
                cam.orthographicSize += .25f;
            }
            if (targetSize < cam.orthographicSize)
            {
                cam.orthographicSize -= .25f;
            }
        }
        else if(zDist > 8)
        {
            if (targetSize > cam.orthographicSize)
            {
                cam.orthographicSize += .25f;
            }
            if (targetSize < cam.orthographicSize)
            {
                cam.orthographicSize -= .25f;
            }
        }
        else
        {
            if (targetSize > cam.orthographicSize)
            {
                cam.orthographicSize += .25f;
            }
            if (targetSize < cam.orthographicSize)
            {
                cam.orthographicSize -= .25f;
            }
        }

    }
}

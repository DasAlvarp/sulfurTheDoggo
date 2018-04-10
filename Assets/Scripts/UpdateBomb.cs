using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author:Javier Bernal 
 * Date Created: 4/3/18
 * Date Modified: 
 * Detail: 
 */

public class UpdateBomb : MonoBehaviour {

    public Vector3 movement;
   // public GameObject explosion;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(movement * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }
}


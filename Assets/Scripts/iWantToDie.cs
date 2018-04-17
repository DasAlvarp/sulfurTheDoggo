using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
 * Author: Javier Bernal
 * Date Created: 4/17/18
 * Date Modified: 4/17/18
 * Description: this script is soley to end the boss level
 */

public class iWantToDie : MonoBehaviour {
    public GameObject boss;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(boss);
        }
    }
}

/* Author: Alexander Tang
 * Date Created: 4/5/2018
 * Date Modified: 
 * Modified By: 
 * Description: Change between game over or you win
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
    public Text endText;

	// Use this for initialization
	void Start () {
        endText = GetComponent<Text>();
        if (EnemyHP.bossHP == 0)
        {
            endText.text = "You Win";
        }
        else
        {
            endText.text = "Game Over";
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

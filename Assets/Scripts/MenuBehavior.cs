/* Author: Alexander Tang
 * Date Created: 3/31/2018
 * Date Modified: 4/3/2018
 * Modified By: Alexander Tang
 * Description: Game UI
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour {

    public void LoadByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void quit()
    {
        print("Quitting Game");
        Application.Quit();
    }
}

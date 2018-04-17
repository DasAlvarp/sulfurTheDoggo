using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Created 4-3-18 by Alvaro Gudiswitz
 * Easy script for buttons to jump between scenes
 * Updates 4-17-18 by Alvaro Gudiswitz
 */

public class MenuBehavior : MonoBehaviour
{

    public void LoadByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadByName(string name)
    {
        SceneManager.LoadScene(name);
    }
}

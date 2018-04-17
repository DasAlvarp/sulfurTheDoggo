using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* Created by Alvaro Gudiswitz 4-17-18
 * Navigate menus with joystick
 * Updated:
 */

public class MenuNav : MonoBehaviour
{
    public string lastMenu;

    public Button[] buttonsRight;
    public Button[] buttonsLeft;
    int selectedHeight = 0;
    int selectedSide = 0;
    Button[,] buttonsToSelect;
    DpadConversion buttonner;

    // Use this for initialization
    void Start()
    {
        buttonsToSelect = new Button[Mathf.Max(buttonsRight.Length, buttonsLeft.Length), 2];
        gameObject.AddComponent<DpadConversion>();

        //fill buttons
        for (int x = 0; x < buttonsRight.Length; x++)
        {
            buttonsToSelect[x, 0] = buttonsLeft[x];
            buttonsToSelect[x, 1] = buttonsRight[x];
        }
    }

    //Selecting b/t things
    void Update()
    {
        selectedHeight -= gameObject.GetComponent<DpadConversion>().upPress;
        if (selectedHeight == -1)
            selectedHeight += buttonsRight.Length;
        selectedHeight %= buttonsRight.Length;

        selectedSide -= gameObject.GetComponent<DpadConversion>().sidePress;
        if (selectedSide == -1)
            selectedSide += 2;
        selectedSide %= 2;

        buttonsToSelect[selectedHeight, selectedSide].Select();
        if (Input.GetButtonDown("MenuSelect"))
        {
            buttonsToSelect[selectedHeight, selectedSide].onClick.Invoke();
        }

        if (Input.GetButtonDown("MenuBack"))
        {
            if (lastMenu != "")
            {
                SceneManager.LoadScene(lastMenu);
            }
            else
            {
                print("exit");
                Application.Quit();
            }
        }
    }
}
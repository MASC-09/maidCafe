using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game Close");//This is to check if the button is working
    }

    public void StartGame()
    {
        SceneManager.LoadScene("RestaurantScene");
    }
}

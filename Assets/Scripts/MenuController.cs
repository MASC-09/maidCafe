using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;

public class MenuController : MonoBehaviour
{
    public AudioSource source;
    public AudioClip failSound;
    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = failSound;
        source.Play();
    }
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game Close");//This is to check if the button is working
    }

    public void StartGame()
    {
        SceneManager.LoadScene("demoscene");
    }
}

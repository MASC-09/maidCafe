using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    [SerializeField] int min, seg;
    [SerializeField] Text tiempo;

    private float restante;
    private bool enMarcha;

    public AudioSource source;
    public AudioClip chillClip;
    public AudioClip noChillClip;
    public AudioClip finJuego;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = chillClip;
        source.loop = true;
        source.Play();
    }

    private void Awake()
    {
        restante = (min * 60) + seg;
        enMarcha = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (enMarcha)
        {
            restante -= Time.deltaTime;
            if (restante <= 30)
            {
                source.Pause();
                source.PlayOneShot(noChillClip);
            }

            //Juego termina
            if(restante <= 0)
            {
                enMarcha = false;
                //Fail.mp3 file is playing once 'EndGame' is load
                SceneManager.LoadScene("EndGame");
                //aqu� se pone toda la l�gica que se quiere cuando se acaba el tiempo
            }

            int tempMin = Mathf.FloorToInt(restante / 60);
            int tempSeg = Mathf.FloorToInt(restante % 60);
            tiempo.text = string.Format("{00:00}:{01:00}", tempMin, tempSeg);
        }
    }

    public void addTime(int segAdd)
    {
        Debug.Log("Estoy sumando tiempo");
        restante += segAdd;
    }

    public void restTime(int segRest)
    {
        Debug.Log("Estoy restando tiempo");
        restante -= segRest;

    }
}

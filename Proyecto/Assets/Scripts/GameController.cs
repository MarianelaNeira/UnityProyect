using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public int puntaje;
    public int vidas = 3;
    public float time = 30;
    public int vitalidadPorVida = 1;
    public int vitalidad = 10;
    public int puntosPorItem = 123;

    public GameObject panelFinal;
    public GameObject panelPrincipal;

    public AudioClip gameOverAudio;
    public AudioClip itemAudio;
    public AudioClip weakenAudio;

    Text textPuntaje;
    Text textVidas;
    Text textTiempo;

    private void Awake()
    {
        vitalidad = vitalidadPorVida;
        puntaje = 0;

        textPuntaje = panelPrincipal.transform.Find("Panel").Find("Puntaje").gameObject.GetComponent<Text>();
        textVidas= panelPrincipal.transform.Find("Panel").Find("Vidas").gameObject.GetComponent<Text>();
        textTiempo = panelPrincipal.transform.Find("Panel").Find("Tiempo").gameObject.GetComponent<Text>();

    }

    void Start()
    {
        textPuntaje.text = puntaje.ToString();
        textVidas.text = vidas.ToString();
    }


    void Update()
    {
        if (Time.timeScale != 0)
        {
            time -= Time.deltaTime;
            textTiempo.text = (Mathf.Round(time * 10) / 10).ToString();

            if (Input.GetKeyDown(KeyCode.Z))
                time++;

            if (time <= 0 || vidas == 0)
                End();
        }
    }

    public void End()
    {
        if (Time.timeScale != 0)
        {
            GetComponent<AudioSource>().PlayOneShot(gameOverAudio);
            Time.timeScale = 0;
            panelFinal.transform.Find("Panel").Find("Panel").Find("TxtName").gameObject.GetComponent<Text>().text = PlayerPrefs.GetString("Name") + " tu puntaje es";
            panelFinal.transform.Find("Panel").Find("Panel").Find("Puntaje").gameObject.GetComponent<Text>().text = puntaje.ToString();
            panelFinal.SetActive(true);
            
            DataBase.OpenDB();
            DataBase.InsertScore(PlayerPrefs.GetString("Email"), puntaje);
        }
    }
    public void Weaken()
    {       
        vitalidad--;
         GetComponent<AudioSource>().PlayOneShot(weakenAudio);
        if (vitalidad == 0)
        {
            vidas--;
            textVidas.text = vidas.ToString();
            vitalidad = vitalidadPorVida;
        }
    }

    public void GetItem()
    {
        puntaje += puntosPorItem;
        textPuntaje.text = puntaje.ToString();
        GetComponent<AudioSource>().PlayOneShot(itemAudio);
    }

   
}

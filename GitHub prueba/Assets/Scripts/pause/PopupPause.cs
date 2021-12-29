using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class PopupPause : MonoBehaviour
{
    public bool gameIsPaused = false;

    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject gameOverMenu;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                resumir();
            }
            else
            {
                pausar();
            }
        }
        /*
        buttonOptions.onClick.AddListener(() =>
        {
            optionsMenu.SetActive(true);
        });
        buttonBack.onClick.AddListener(() =>
        {
            optionsMenu.SetActive(false);
        });
        */
    }

    public void resumir()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void pausar()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void options()
    {
        optionsMenu.SetActive(true);
    }

    public void back()
    {
        optionsMenu.SetActive(false);
    }

    public void gameOver()
    {
        gameOverMenu.SetActive(true);
    }


    /*
    [SerializeField] Button continuar;
    [SerializeField] Button options;
    [SerializeField] Button salir;
    [SerializeField] TextMesh textoContinuar;
    [SerializeField] TextMesh textoOptions;
    [SerializeField] TextMesh textoSalir;
    [SerializeField] Text textoPopup;


    public void Init(Transform canvas, string popupMsg, string btnContinuetxt, string btnOptionstxt, string btnSalirtxt, Action action)
    {
        textoPopup.text = popupMsg;
        textoContinuar.text = btnContinuetxt;
        textoOptions.text = btnOptionstxt;
        textoSalir.text = btnSalirtxt;

        transform.SetParent(canvas);
        transform.localScale = Vector3.one;
        GetComponent<RectTransform>().offsetMin = Vector2.zero;
        GetComponent<RectTransform>().offsetMax = Vector2.zero;

        //no.OnPointerEnter(Instantiate(sonidoUp));

        continuar.onClick.AddListener(() =>
        {
            GameObject.Destroy(this.gameObject);
        });

    }
    */
}

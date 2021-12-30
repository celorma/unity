using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class PopupPause : MonoBehaviour
{
    public bool gameIsPaused = false;

    [SerializeField] public GameObject pauseMenu;
    [SerializeField] public GameObject optionsMenu;
    [SerializeField] public GameObject gameOverMenu;
    [SerializeField] public GameObject inventary;
    [SerializeField] public GameObject key;

    public static bool hayLlave = false; 

    public bool activo = false;

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
                if(!activo)
                    pausar();
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (activo)
            {
                activo = false;
                inventario(activo);
            }
            else
            {
                if (!gameIsPaused)
                {
                    activo = true;
                    inventario(activo);
                }
            }
            
        }
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
        if (hayLlave)
        {
            key.SetActive(false);
        }
    }

    public void inventario(bool abrir)
    {
        inventary.SetActive(abrir);
        if (abrir)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void llave()
    {
        key.SetActive(hayLlave);
    }
}

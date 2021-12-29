using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void cargarScreen(string nombreLv)
    {
        SceneManager.LoadScene(nombreLv);
        Time.timeScale = 1f;
    }

    public void cargarNivel(string nombreLv)
    {
        LoadScene.loadLevel(nombreLv);
        Time.timeScale = 1f;
    }

    public void salirVG()
    {
        Application.Quit();
    }

}

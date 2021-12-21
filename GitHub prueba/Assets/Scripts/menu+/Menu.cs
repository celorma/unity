using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void cargarScreen(string nombreLv)
    {
        SceneManager.LoadScene(nombreLv);
    }

    public void salirVG()
    {
        Application.Quit();
    }

}

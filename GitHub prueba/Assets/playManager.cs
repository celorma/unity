using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playManager : MonoBehaviour
{
    public string nivelActual;
    public void recargarNivel()
    {
        nivelActual = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(nivelActual);
        Time.timeScale = 1f;
    }
}

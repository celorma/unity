using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class LoadScene
{
    public static string nextLevel;

    public static void loadLevel(string name)
    {
        nextLevel = name;
        SceneManager.LoadScene("loadingg");
    }
}

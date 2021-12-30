using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Transform mainCanvas;

    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
        {
            GameObject.Destroy(this.gameObject);
            return;
        }

        instance = this;
    }

    public Popup crearPopup()
    {
        GameObject popupGo = Instantiate(Resources.Load("UI/popup") as GameObject);
        return popupGo.GetComponent<Popup>();
    }
    public Popup crearPause()
    {
        GameObject popupGo = Instantiate(Resources.Load("UI/PauseCanvas") as GameObject);
        return popupGo.GetComponent<Popup>();
    }

    public Popup crearOptions()
    {
        GameObject popupGo = Instantiate(Resources.Load("UI/OptionsCanvas") as GameObject);
        return popupGo.GetComponent<Popup>();
    }

    public Popup crearGameOver()
    {
        GameObject popupGo = Instantiate(Resources.Load("UI/GameOverCanvas") as GameObject);
        return popupGo.GetComponent<Popup>();
    }
}

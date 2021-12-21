using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class cambiarColor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        Action action = () =>
        {
            Application.Quit();
        };

        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            Popup popup = UIController.instance.crearPopup();
            popup.Init(UIController.instance.mainCanvas, "¿Estás seguro?","No","Si", action);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

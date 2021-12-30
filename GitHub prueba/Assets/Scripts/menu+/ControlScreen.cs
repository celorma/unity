using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlScreen : MonoBehaviour
{
    [SerializeField] public Toggle toggle;

    [SerializeField] public TMP_Dropdown resolucion;
    Resolution[] resoluciones;

    // Start is called before the first frame update
    void Start()
    {
        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }
        checkResolution();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enableFullScreen(bool FullScreen)
    {
        Screen.fullScreen = FullScreen;
    }

    public void checkResolution()
    {
        resoluciones = Screen.resolutions;
        resolucion.ClearOptions();
        List<string> opciones = new List<string>();
        int resolucionActual = 0;

        for (int i=0; i<resoluciones.Length; i++)
        {
            string opcion = resoluciones[i].width + " x " + resoluciones[i].height;
            if (!opciones.Contains(opcion))
            {
                opciones.Add(opcion);
            }
            

            if (Screen.fullScreen && resoluciones[i].width == Screen.currentResolution.width
                && resoluciones[i].height == Screen.currentResolution.height)
            {
                resolucionActual = i;
            }
        }

        resolucion.AddOptions(opciones);
        resolucion.value = resolucionActual;
        resolucion.RefreshShownValue();
        resolucion.value = PlayerPrefs.GetInt("numResolution", 0);
    }

    public void changeResolution(int iResolution)
    {
        PlayerPrefs.SetInt("numResolution", resolucion.value);

        Resolution resolution = resoluciones[iResolution];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}

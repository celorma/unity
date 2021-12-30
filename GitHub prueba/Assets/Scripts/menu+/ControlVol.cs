using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlVol : MonoBehaviour
{
    [SerializeField] public Slider slider;
    public float valorSlider;
    [SerializeField] public Image imgMute;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volumenAudio", 1f);
        AudioListener.volume = slider.value;
        muteOrUnmuted();
    }

    // Update is called once per frame

    public void changeSlider(float valor)
    {
        valorSlider = valor;
        PlayerPrefs.SetFloat("volumenAudio", valorSlider);
        AudioListener.volume = slider.value;
        muteOrUnmuted();
    }
    public void muteOrUnmuted()
    {
        if (valorSlider == 0)
        {
            imgMute.enabled = true;
        }
        else
        {
            imgMute.enabled = false;
        }
    }

}

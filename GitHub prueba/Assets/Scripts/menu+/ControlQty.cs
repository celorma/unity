using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlQty : MonoBehaviour
{
    public TMP_Dropdown dropd;
    public int calidad;
    // Start is called before the first frame update
    void Start()
    {
        calidad = PlayerPrefs.GetInt("numCalidad", 2);
        dropd.value = calidad;
        ajustarCalidad();
    }

    public void ajustarCalidad()
    {
        QualitySettings.SetQualityLevel(dropd.value);
        PlayerPrefs.SetInt("numCalidad", dropd.value);
        calidad = dropd.value;
    }
}

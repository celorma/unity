using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtaqueEsp : MonoBehaviour
{
    public Image barraSpec;
    public float cargaActual = 0;
    public float cargaMaxima = 100;

    public GameObject bullet;

    void Update()
    {
        cargaActual = character.cargaSpec;
        barraSpec.fillAmount = cargaActual / cargaMaxima;
    }
}

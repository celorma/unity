using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class vida : MonoBehaviour
{
    [SerializeField] public Image barraVida;
    public float vidaActual;
    public float vidaMax;

    [SerializeField] public GameObject player;

    // Update is called once per frame
    void Update()
    {
        vidaActual = player.GetComponent<vida_damage>().getVida();
        barraVida.fillAmount = vidaActual / vidaMax;
    }
    public float getVidaMax()
    {
        return vidaMax;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hp_mount : MonoBehaviour
{
    public float porcentaje;
    public float vida;
    public float vidaMax;

    private Animator anim;

    public GameObject player;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        vida = player.GetComponent<vida_damage>().getVida();
        vidaMax = player.GetComponent<vida_damage>().vidaMax;
        porcentaje = (vida * 100 / vidaMax);
        anim.SetBool("LowHP", porcentaje<= 25);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lowHP : MonoBehaviour
{
    public double porc;
    public GameObject player;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        porc = player.GetComponent<vida_damage>().getVida() * 100 / player.GetComponent<vida_damage>().vidaMax;

        anim.SetBool("LowHP", porc <= 25);
    }
}

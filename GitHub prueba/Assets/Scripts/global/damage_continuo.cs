using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damage_continuo : MonoBehaviour
{
    public float damageCant = 30;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<vida_damage>().restarVida(damageCant);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<vida_damage>().restarVida(damageCant);
        }
    }
}

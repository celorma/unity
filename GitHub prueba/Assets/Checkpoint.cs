using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] public GameObject anuncio;
    [SerializeField] public GameObject flag;

    public bool activado = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!activado)
            {
                collision.GetComponent<PlayerRespawn>().checkpointAlcanzado(transform.position.x, transform.position.y);
                flag.SetActive(true);
                StartCoroutine(tiempoActivado());
                collision.GetComponent<vida_damage>().vida = collision.GetComponent<vida_damage>().vidaMax;
                GetComponent<Animator>().enabled = true;
                activado = true;
            }
        }
    }

    IEnumerator tiempoActivado()
    {
        anuncio.SetActive(true);
        yield return new WaitForSeconds(3f);
        anuncio.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    [SerializeField] public GameObject textoNo;
    [SerializeField] public GameObject textoSi;
    [SerializeField] public GameObject anuncio;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!PopupPause.hayLlave)
            {
                textoNo.SetActive(true);
                anuncio.SetActive(true);
                textoNo.SetActive(false);
            }
            else
            {
                textoNo.SetActive(false);
                anuncio.SetActive(true);
                textoSi.SetActive(true);
                PopupPause.hayLlave = false;
            }
        }
    }

    IEnumerable tiempo()
    {
        textoNo.SetActive(false);
        anuncio.SetActive(true);
        textoSi.SetActive(true);
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}

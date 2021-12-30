using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class llave : MonoBehaviour
{

    public GameObject canvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PopupPause.hayLlave = true;
            canvas.GetComponent<PopupPause>().llave();
            Destroy(gameObject);
        }
    }
}

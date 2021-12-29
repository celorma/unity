using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{

    public GameObject explosions;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject clon = Instantiate(explosions, transform.position, transform.rotation) as GameObject;
            Destroy(clon, 0.5f);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeLapse : MonoBehaviour
{
    public float segundos;
    public bool activador = false;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!activador)
        {
            anim.Play("wait");
            StartCoroutine(activar());
        }
    }

    IEnumerator activar()
    {
        activador = true;
        yield return new WaitForSeconds(segundos);
        activador = false;
    }

}

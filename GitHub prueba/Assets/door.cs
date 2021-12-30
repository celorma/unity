using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{

    public TextMesh mensaje;
    public GameObject ventana;

    public void mostrar()
    {
        ventana.SetActive(true);
    }

}

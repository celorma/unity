﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    [SerializeField] GameObject Seguimiento;

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        position.x = Seguimiento.transform.position.x;
        transform.position = position;
    }
}
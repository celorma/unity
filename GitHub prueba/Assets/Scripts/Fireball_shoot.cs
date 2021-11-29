using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_shoot : MonoBehaviour
{


    private Rigidbody2D Rigidbody2D;

    private Vector2 Direction;

    [SerializeField] float Speed = 1;


    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rigidbody2D.velocity = Direction * Speed;
    }


    public void DestroyBullet()
    {
        Destroy(gameObject);

    }


}


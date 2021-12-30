﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class character : MonoBehaviour
{

    private Rigidbody2D Rigidbody2D;
    internal Animator Animator;

    private float Horizontal;
    private bool Grounded;
    private float LastShoot;
    private Vector3 posicionEnY;

    [SerializeField] float JumpForce;
    [SerializeField] float Speed;
    [SerializeField] GameObject BulletPrefab;




    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            Horizontal = Input.GetAxisRaw("Horizontal") * Speed;

        }
        else
        {
            Horizontal = 0;
            transform.position = transform.position;
        }

        Animator.SetBool("Running", Horizontal != 0.0f);

        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);


        Debug.DrawRay(transform.position, Vector3.down * 0.26f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.3f))
        {
            Grounded = true;

            Animator.SetBool("Jump", false);
        }
        else
        {
            Grounded = false;
        }
        Animator.SetBool("Grounded", Grounded);
        if (Input.GetKeyDown(KeyCode.Z) && Grounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.X) && Time.time > LastShoot + 0.7f)
        {
            Shoot();

            LastShoot = Time.time;

        }
        else
        {
            EndShoot();
        }

        posicionEnY = transform.position;

        if (posicionEnY.y <= -2f)
        {
            muerte();
        }

    }



    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
        Animator.SetBool("Jump", true);
    }

    private void Shoot()
    {
        Vector3 direction;

        if (transform.localScale.x == 1) direction = Vector2.right;
        else direction = Vector2.left;
        Animator.SetBool("Shoot", true);
        GameObject bullet = Instantiate(BulletPrefab, new Vector3(transform.position.x, transform.position.y + 0.05f, 0) + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<bullet>().SetDirection(direction);

    }

    public void EndShoot()
    {
        Animator.SetBool("Shoot", false);
    }
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal, Rigidbody2D.velocity.y);
    }

    private void muerte()
    {
        GameObject.Destroy(gameObject);
    }

}

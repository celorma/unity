using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character2 : MonoBehaviour
{
    private SpriteRenderer spr;
    private Rigidbody2D Rigidbody2D;
    internal Animator Animator;

    private float Horizontal;
    private bool Grounded;
    private float LastShoot;
    

    [SerializeField] float JumpForce;
    [SerializeField] float Speed;
    [SerializeField] GameObject BulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            Horizontal = Input.GetAxisRaw("Horizontal") * Speed;
        else
        {
            Horizontal = 0;
            transform.position = transform.position;
        }

        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        Animator.SetBool("Running", Horizontal != 0.0f);

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

        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.N) && Time.time > LastShoot + 0.26f)
        {
            Shoot();
            LastShoot = Time.time;

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

}

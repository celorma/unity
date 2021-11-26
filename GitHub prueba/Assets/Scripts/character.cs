using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{

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
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal") * Speed;

        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        Animator.SetBool("Running", Horizontal != 0.0f);

        Debug.DrawRay(transform.position, Vector3.down * 0.26f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.26f))
        {
            Grounded = true;
            Animator.SetBool("Jump", false);
        }
        else Grounded = false;

        if (Input.GetKeyDown(KeyCode.Z) && Grounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.X) && Time.time > LastShoot + 0.25f)
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

        GameObject bullet = Instantiate(BulletPrefab, transform.position + new Vector3(0, transform.position.y - 0.365f,0) +direction * 0.2f, Quaternion.identity);
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

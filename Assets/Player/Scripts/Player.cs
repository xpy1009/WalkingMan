using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject avatar;
    public float walkSpeed;
    public float runSpeed;

    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D col;

    // Start is called before the first frame update
    void Start()
    {
        animator = avatar.GetComponent<Animator>();
        rb = avatar.GetComponent<Rigidbody2D>();
        col = avatar.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float speed = 0.0f;

        if (Input.GetKey(KeyCode.LeftShift)) {
            animator.SetBool("Run", true);
            animator.SetBool("Walk", false);
            speed = runSpeed;
        }
        else {
            animator.SetBool("Run", false);
            speed = walkSpeed;
        }

        float hRaw = Input.GetAxisRaw("Horizontal");
        if (hRaw != 0.0f) {
            avatar.transform.localScale = new Vector3(-hRaw, 1, -1);
            animator.SetBool("Walk", true);
            animator.SetBool("Idle", false);
        }
        else {
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", true);
        }

        rb.velocity = new Vector2(h * speed * Time.fixedDeltaTime, rb.velocity.y);
    }

}

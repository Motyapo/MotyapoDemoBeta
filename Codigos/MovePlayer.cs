using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]



public class Player : MonoBehaviour
{
    public Animator playerAnimator;
    float input_x = 0;
    float input_y = 0;
    public float speed = 3f;
    bool isWalking = false;
    Rigidbody2D rb2d;

    Vector2 movement = Vector2.zero;

  

    // Start is called before the first frame update
    void Start()
    {
        isWalking = false;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
        {
            input_x = Input.GetAxisRaw("Horizontal");
            input_y = Input.GetAxisRaw("Vertical");
            isWalking = (input_x != 0 || input_y != 0);
            movement = new Vector2(input_x, input_y);

        
            if (isWalking)
            {
                playerAnimator.SetFloat("input_x", input_x);
                playerAnimator.SetFloat("input_y", input_y);
            }

            playerAnimator.SetBool("isWalking", isWalking);
        }

    }

    private void FixedUpdate()
    {
        if (input_x != 0 && input_y != 0)
        {
            rb2d.MovePosition(rb2d.position + movement * (speed/2) * Time.fixedDeltaTime);
        }
        else
        {
            rb2d.MovePosition(rb2d.position + movement * speed * Time.fixedDeltaTime);
        }
        
    }
}


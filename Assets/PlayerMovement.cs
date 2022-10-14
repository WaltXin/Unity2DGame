using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //defined fileds
    private float moveSpeed = 7f;
    private float jumpForce = 7f;
    private float dirX = 0f;

    //Global variables
    private Rigidbody2D player;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private enum MovementState {idle, run, jump, fall}
    
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {   
        dirX = Input.GetAxisRaw("Horizontal");
        player.velocity = new Vector2(dirX * moveSpeed, player.velocity.y);

        if (Input.GetButtonDown("Jump")) {
            player.velocity = new Vector2(player.velocity.x, jumpForce);
        }

        updateMovementState();
    }

    private void updateMovementState() {
        MovementState state;

        if (dirX > 0f) {
            state = MovementState.run;
            spriteRenderer.flipX = false;
        } else if (dirX < 0f) {
            state = MovementState.run;
            spriteRenderer.flipX = true;
        } else {
            state = MovementState.idle;
        }

        if (player.velocity.y > .1f) {
            state = MovementState.jump;
        } else if (player.velocity.y < -.1f) {
            state = MovementState.fall;
        }

        animator.SetInteger("state", (int)state);
    }
}

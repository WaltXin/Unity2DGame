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
    private BoxCollider2D coll;
    [SerializeField] private LayerMask jumpableGround;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private enum MovementState {idle, run, jump, fall}

    float distToGround = 0f;
    
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();

        distToGround = coll.bounds.extents.y;
    }

    
    void Update()
    {   
        dirX = Input.GetAxisRaw("Horizontal");
        player.velocity = new Vector2(dirX * moveSpeed, player.velocity.y);

        if (Input.GetButtonDown("Jump") && isGround()) {
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

    private bool isGround() {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
        //return Physics2D.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
        //return Physics2D.Raycast(coll.bounds.center, Vector2.down, coll.bounds.extents.y);
    }
}

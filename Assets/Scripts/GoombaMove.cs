using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class GoombaMove : MonoBehaviour {
    private InputAction move, jump;
    [SerializeField] private InputActionAsset actions;
    [SerializeField] float speed = 0.5f;
    private InputActionMap actionMap;
    private SpriteRenderer renderer;
    private Animator animator;
    Rigidbody2D rb;
    bool grounded;


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        actionMap = actions.FindActionMap("Goomba");
        move = actionMap.FindAction("Move");
        jump = actionMap.FindAction("Jump");
        jump.performed += ctx => Jump(ctx);
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Jump(InputAction.CallbackContext ctx) {
        if (grounded) {
            rb.velocity += 5 * Vector2.up;
        }
    }


    private void OnEnable() {
        actionMap.Enable();
    }

    private void OnDisable() {
        actionMap.Disable();
    }

    // Update is called once per frame
    void Update() {
        grounded = Physics2D.Raycast(transform.position, Vector2.down, 0.02f) || Physics2D.Raycast((Vector2)transform.position + Vector2.left * 0.3f, Vector2.down, 0.02f) || Physics2D.Raycast((Vector2)transform.position + Vector2.right * 0.3f, Vector2.down, 0.02f);
        animator.SetBool("isJumping", !grounded);
        Vector2 moveVector = move.ReadValue<Vector2>();

        if (moveVector.x != 0) {
            animator.SetBool("isMoving", true);
            renderer.flipX = moveVector.x < 0;
            moveVector *= Time.deltaTime * speed;
            transform.Translate(moveVector);
        } else {
            animator.SetBool("isMoving", false);
        }

    }
}

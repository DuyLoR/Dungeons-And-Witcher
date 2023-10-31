using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector2 movementInputDirection;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    [SerializeField] private float movementSpeed;

    [SerializeField] private bool isFacingRight = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        CheckInput();
        CheckMovementDirection();
    }

    private void FixedUpdate()
    {
        ApllyMovement();
    }

    private void CheckInput()
    {
        movementInputDirection.x = Input.GetAxisRaw("Horizontal");
        movementInputDirection.y = Input.GetAxisRaw("Vertical");
    }

    private void ApllyMovement()
    {
        rb.velocity = movementInputDirection * movementSpeed * Time.deltaTime;
    }

    private void CheckMovementDirection()
    {
        if (isFacingRight && movementInputDirection.x < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementInputDirection.x > 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInput playerInput;
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMove(InputValue inputValue)
    {
        movement = inputValue.Get<Vector2>();

    }

    private void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;
    }    
}

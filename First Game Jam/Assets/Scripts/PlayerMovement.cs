using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInput playerInput;
    private Rigidbody2D rb;
    private BoolHolder boolHolder;
    private Vector2 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boolHolder = GetComponentInParent<BoolHolder>();
    }

    private void OnMove(InputValue inputValue)
    {
        movement = inputValue.Get<Vector2>();

    }

    private void FixedUpdate()
    {
        rb.velocity = movement * boolHolder.playerSpeed;
    }    
}

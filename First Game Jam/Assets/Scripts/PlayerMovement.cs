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
    private Animator anim;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("isMoving", false); // idle
        rb = GetComponent<Rigidbody2D>();
        boolHolder = GetComponentInParent<BoolHolder>();
    }

    private void OnMove(InputValue inputValue)
    {
        if (boolHolder.playerFrozen)
        {
            return;
        }
        movement = inputValue.Get<Vector2>();
        if (movement.magnitude == 0)
        {
            anim.SetBool("isMoving", false); // idle
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, new Vector2(-movement.y, movement.x));
            anim.SetBool("isMoving", true);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = movement * boolHolder.playerSpeed;
    }    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction moveAction;

    [SerializeField] Rigidbody rb;

    //private void Start()
    //{
    //    playerInput = GetComponent<PlayerInput>();
    //    moveAction = playerInput.actions.FindAction("Move");
    //}

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(1f, 1f);
        //MovePlayer();
    }

    void MovePlayer()
    {
        Debug.Log(moveAction.ReadValue<Vector2>());
    }
}

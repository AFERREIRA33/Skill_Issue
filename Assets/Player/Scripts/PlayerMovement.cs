using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    private Vector2 movementVector;
    private PlayerInput playerInput;
    public float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = new PlayerInput();
        playerInput.Player_Map.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
        movementVector = new Vector2();
    }


    // Update is called once per frame
    void Update()
    {
        movementVector = playerInput.Player_Map.Movement.ReadValue<Vector2>();
        rigidbody2d.velocity = movementVector * speed;
    }

}
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

    private float activeMoveSpeed;
    public float dashSpeed;

    public float dashLength = .5f, dashCooldown = 1f;

    private float dashCounter;
    private float dashCooldownCounter;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = new PlayerInput();
        playerInput.Player_Map.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
        movementVector = new Vector2();
        activeMoveSpeed = speed;
    }


    // Update is called once per frame
    void Update()
    {
        movementVector = playerInput.Player_Map.Movement.ReadValue<Vector2>();
        rigidbody2d.velocity = movementVector * activeMoveSpeed;
        if (playerInput.Player_Map.Dash.triggered || Input.GetMouseButtonDown(1) && Gamepad.all.Count < 1)
        {
            if (dashCounter <= 0 && dashCooldownCounter <=0) 
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
            }
        }

        if (dashCounter >0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                activeMoveSpeed = speed;
                dashCooldownCounter = dashCooldown;
            }
        }
        if (dashCooldownCounter > 0)
        {
            dashCooldownCounter -= Time.deltaTime;
        }
    }

}
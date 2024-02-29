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
    private float dashSpeed = 10f;

    private float doubleDashSpeed = 15f;
    public float dashLength = .5f, dashCooldown = 1f, doubleDashLength = .5f;

    private float dashCounter;
    private float dashCooldownCounter;

    private int numberDash = 0;

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
        if (!gameObject.GetComponent<Player>().isStun)
        {
            movementVector = playerInput.Player_Map.Movement.ReadValue<Vector2>();
            rigidbody2d.velocity = movementVector * activeMoveSpeed;
            if (gameObject.GetComponent<Player>().doubleDash)
            {
                
                if ((playerInput.Player_Map.Dash.triggered || Input.GetMouseButtonDown(1)) && Gamepad.all.Count < 1)
                {
                    if (dashCounter <= 0)
                    {
                        if (numberDash < 1) 
                        {
                            activeMoveSpeed = dashSpeed;
                            dashCounter = dashLength;
                            numberDash++; 
                        }
                        else if (numberDash == 1) 
                        {
                            
                            activeMoveSpeed = doubleDashSpeed;
                            dashCounter = doubleDashLength;
                            numberDash = 0; 
                        }
                    }
                }

                if (dashCounter > 0)
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
            else
            {
                if (playerInput.Player_Map.Dash.triggered || Input.GetMouseButtonDown(1) && Gamepad.all.Count < 1)
                {
                    if (dashCounter <= 0 && dashCooldownCounter <= 0)
                    {
                        activeMoveSpeed = dashSpeed;
                        dashCounter = dashLength;
                    }
                }

                if (dashCounter > 0)
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
        
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Aim : MonoBehaviour
{
    public Transform playerTransform;
    public Vector2 aimDirection;
    public float lookSensitivity = 3f;
    private Vector3 mousePos;
    public GameObject bullet;
    public Transform transformBullet;
    private PlayerInput playerInput;


    //private void Awake()
    //{
    //    // Initialize Input System
    //    InputSystem.onDeviceChange += (device, change) =>
    //    {
    //        if (change == InputDeviceChange.Added && device is Gamepad)
    //        {
    //            var gamepad = (Gamepad)device;
    //            gamepad.rightStick.performed += ctx => rightStickInput = ctx.ReadValue<Vector2>();
    //            gamepad.rightStick.canceled += ctx => rightStickInput = Vector2.zero;
    //        }
    //    };
    //}

    void Start()
    {
        playerInput = new PlayerInput();
        playerInput.Player_Map.Enable();

        playerTransform = GetComponent<Transform>();
    }


    void Update()
    {
        if (Input.GetJoystickNames().Length == 1)
        {
            if (Time.timeScale > 0)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 rotation = mousePos - transform.position;
                float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, rotZ);
                if (Input.GetMouseButtonDown(0) || playerInput.Player_Map.Fire.IsPressed())
                {
                    Instantiate(bullet, transformBullet.position, Quaternion.identity);
                }
            }
        }
        else
        {
            float xInput = Input.GetAxisRaw("RightJoystickX");
            float yInput = Input.GetAxisRaw("RightJoystickY");


            aimDirection = new Vector2(xInput, yInput);
            aimDirection = Vector2.ClampMagnitude(aimDirection, 1);
        }
    }
    //private void RotateCharacter()
    //{
    //    if (rightStickInput != Vector2.zero)
    //    {
    //        float angle = Mathf.Atan2(rightStickInput.y, rightStickInput.x) * Mathf.Rad2Deg;
    //        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

    //        // Apply rotation to the character
    //        if (rb2d != null)
    //        {
    //            // If using Rigidbody2D, use MoveRotation to apply rotation smoothly
    //            rb2d.MoveRotation(Quaternion.Slerp(rb2d.rotation, targetRotation, Time.deltaTime * rotationSpeed));
    //        }
    //        else
    //        {
    //            // If not using Rigidbody2D, use transform.rotation
    //            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    //        }
    //    }
    //}
}

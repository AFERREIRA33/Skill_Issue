using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Aim : MonoBehaviour
{
    public GameObject projectile;
    public Transform transformProjectile;
    public Transform playerTransform;
    public Vector2 aimDirection;
    private Vector3 mousePos;
    private PlayerInput playerInput;

    
    void Start()
    {
        playerInput = new PlayerInput();
        playerInput.Player_Map.Enable();
        playerTransform = GetComponent<Transform>();

    }

    void Update()
    {
        if (Gamepad.all.Count < 1)
        {
            if (Time.timeScale > 0)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 rotation = mousePos - transform.position;
                float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, rotZ);
                if (Input.GetMouseButtonDown(0) && !gameObject.GetComponent<Player>().isGuard)
                {
                    Instantiate(projectile, transformProjectile.position, Quaternion.identity);
                }
            }
        }
        else
        {
            if (playerInput.Player_Map.Rotation.triggered)
            {
                aimDirection = playerInput.Player_Map.Rotation.ReadValue<Vector2>();
                float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));

            }
            if (playerInput.Player_Map.Fire.triggered && !gameObject.GetComponent<Player>().isGuard)
            {
                Instantiate(projectile, transformProjectile.position, Quaternion.identity);
            }
        }
    }
}

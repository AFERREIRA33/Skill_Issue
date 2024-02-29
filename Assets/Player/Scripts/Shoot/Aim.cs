using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Aim : MonoBehaviour
{
    public GameObject projectile;
    public GameObject grenade;
    public Transform transformProjectile;
    public Transform playerTransform;
    public Vector2 aimDirection;
    public bool multiShoot;
    public bool isPoisonGrenade;
    [HideInInspector]public float coolDownShoot = .4f;
    private Vector3 mousePos;
    private PlayerInput playerInput;
    private float angle;
    private float multiShootPosY;
    private bool canFire = true;




    void Start()
    {
        multiShootPosY = transformProjectile.position.y;
        playerInput = new PlayerInput();
        playerInput.Player_Map.Enable();
        playerTransform = GetComponent<Transform>();
        
    }

    void Update()
    {
        if (gameObject.GetComponent<Player>().speedUpShoot)
        {
            coolDownShoot = 0.3f;
        }
        Fire();
    }

    public void Fire()
    {
        if (Gamepad.all.Count < 1 && !playerTransform.gameObject.GetComponent<Player>().isStun)
        {
            if (Time.timeScale > 0)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 rotation = mousePos - transform.position;
                float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, rotZ);
                if (Input.GetMouseButtonDown(0) && !gameObject.GetComponent<Player>().isGuard && canFire)
                {
                    gameObject.GetComponent<Player>().stopInvisible = true;
                    if (multiShoot)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (i == 2)
                            {
                                multiShootPosY += .5f;
                            }
                            else if (i == 1)
                            {
                                multiShootPosY -= .5f;
                            }
                            Instantiate(projectile, new Vector3(transformProjectile.position.x, multiShootPosY, transformProjectile.position.z), Quaternion.identity);
                            multiShootPosY = transformProjectile.position.y;
                        }
                    }
                    else
                    {
                        if (isPoisonGrenade)
                        {
                            Instantiate(grenade, transformProjectile.position, Quaternion.identity);
                            isPoisonGrenade = false;
                        }
                        Instantiate(projectile, transformProjectile.position, Quaternion.identity);
                    }
                    canFire = false;
                    StartCoroutine(CoolDown());

                }
            }
        }
        else if (!playerTransform.gameObject.GetComponent<Player>().isStun)
        {
            if (playerInput.Player_Map.Rotation.triggered)
            {
                aimDirection = playerInput.Player_Map.Rotation.ReadValue<Vector2>();
                angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            }
            if (playerInput.Player_Map.Fire.triggered && !gameObject.GetComponent<Player>().isGuard && canFire)
            {
                gameObject.GetComponent<Player>().stopInvisible = true;
                if (multiShoot)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (i == 2)
                        {
                            multiShootPosY += 1f;
                        }
                        else if (i == 1)
                        {
                            multiShootPosY -= 1f;
                        }
                        Instantiate(projectile, new Vector3(transformProjectile.position.x, multiShootPosY, transformProjectile.position.z), Quaternion.identity);
                        multiShootPosY = transformProjectile.position.y;
                    }
                }
                else
                {
                    if (isPoisonGrenade)
                    {
                        Instantiate(grenade, transformProjectile.position, Quaternion.identity);
                        isPoisonGrenade = false;
                    }
                    Instantiate(projectile, transformProjectile.position, Quaternion.identity);
                }
                canFire = false;
                StartCoroutine(CoolDown());

            }
        }

    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(coolDownShoot);
        canFire = true;
    }
}

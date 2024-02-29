using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float MaxHp = 100;
    public HPBar hpbar;
    public GameObject activable;
    public GameObject turret;
    public GameObject stunObject;
    public GameObject invicibleObject;
    public bool isStun;
    public bool stunProjectile;
    private float currentHP;
    public float timerGuard = 4f;
    public float reloadGuard = 2f;
    public bool isGuard;
    public bool isInvicible;
    private float invicibleTimer = 3f;
    private PlayerInput playerInput;
    private GameObject attackCollider;
    private float timeMultiShoot = 20f;
    private float stunTimer = 3f;
    private float turretTimer = 10f;
    private bool canSpawnTurret = true;

    
    void Start()
    {

        isGuard = false;
        playerInput = new PlayerInput();
        playerInput.Player_Map.Enable();
        currentHP = MaxHp;
        hpbar.UpdateHPSlider(currentHP, MaxHp);
        attackCollider = transform.GetChild(0).gameObject;
    }


    private void Update()
    {
        if (playerInput.Player_Map.Guard.IsPressed() && !isStun)
        {
            if (timerGuard > 0)
            {
                timerGuard -= Time.deltaTime;
                isGuard = true;
            }
        }
        else
        {
            if (timerGuard < 4 && reloadGuard <= 0)
            {
                timerGuard = 4f;
                reloadGuard = 2f;
            }
            else if (isGuard || reloadGuard < 2)
            {
                reloadGuard = -Time.deltaTime;

                isGuard = false;
            }

        }
        if (playerInput.Player_Map.CAC.IsPressed() && !isStun)
        {
            if (attackCollider != null)
            {
                attackCollider.SetActive(true);
            }
            
        }
        else
        {
            if (attackCollider != null)
            {
                attackCollider.SetActive(false);
            }
        }
        if (playerInput.Player_Map.Activable.triggered && !isStun)
        {
            if (activable != null)
            {
                
            }
        }
    }

    public void OnTakeDamage(float damage)
    {
        if (isGuard || isInvicible)
        {
            damage = 0;
        }
        else
        {
            currentHP -= damage;
            hpbar.UpdateHPSlider(currentHP, MaxHp);
            if (currentHP <= 0)
            {
                Debug.Log("Dead");
                hpbar.UpdateHPSlider(currentHP, MaxHp);
            }
        }
    }

    // Invicibility
    public void Invicibility()
    {
        isInvicible = true;
        invicibleObject.SetActive(true);
        StartCoroutine(Invicble());
    }

    IEnumerator Invicble()
    {
        while (invicibleTimer > 0)
        {

            invicibleTimer -= Time.deltaTime;
            yield return null;
        }
        isInvicible = false;
        invicibleObject.SetActive(false);
        //invicibleTimer = 3f;
    }

    // Stun
    public void Stuned()
    {
        isStun = true;
        stunObject.SetActive(true);
        StartCoroutine(Stun());
    }

    IEnumerator Stun()
    {
        while (stunTimer > 0)
        {
            stunTimer -= Time.deltaTime;
            yield return null;
        }
        isStun = false;
        stunObject.SetActive(false);
    }

    // multi Shoot

    public void MultiShoot()
    {
        gameObject.GetComponent<Aim>().multiShoot = true;
        StartCoroutine(MultiProjectile());
    }

    IEnumerator MultiProjectile()
    {
        
        while (timeMultiShoot > 0)
        {
            
            timeMultiShoot -= Time.deltaTime;
            yield return null;
        }
        gameObject.GetComponent<Aim>().multiShoot = false;
        //timeMultiShoot = 20f;
    }

    // Turret

    public void SpawnTurret()
    {
        if (canSpawnTurret)
        {
            Instantiate(turret, transform.position, transform.rotation);
            turret.tag = "PlayerTurret";
            canSpawnTurret = false;
            StartCoroutine(TimeTurret());
        }
    }

    IEnumerator TimeTurret()
    {
        while (turretTimer >0)
        {
            turretTimer -= Time.deltaTime;
            yield return null;
        }
        Destroy(GameObject.FindGameObjectWithTag("PlayerTurret"));
    }

    // Stun Projectile

}

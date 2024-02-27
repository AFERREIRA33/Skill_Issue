using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int MaxHp = 100;
    public HPBar hpbar;
    public GameObject activable;
    private int currentHP;
    public float timerGuard = 4f;
    public float reloadGuard = 2f;
    public bool isGuard;
    private PlayerInput playerInput;
    private GameObject attackCollider;
    
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
        if (playerInput.Player_Map.Guard.IsPressed())
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
        if (playerInput.Player_Map.CAC.IsPressed())
        {
            attackCollider.SetActive(true);
        }
        else
        {
            attackCollider.SetActive(false);
        }
        if (playerInput.Player_Map.Activable.triggered)
        {
            if (activable != null)
            {
                
            }
        }
    }
    public void OnTakeDamage(int damage)
    {
        if (isGuard)
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

}

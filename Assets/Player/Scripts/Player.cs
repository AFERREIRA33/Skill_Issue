using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int MaxHp = 100;
    public HPBar hpbar;
    public GameObject activable;
    private int currentHP;
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
            isGuard = true;
        }
        else
        {
            isGuard = false;
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
            Debug.Log("toto");
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

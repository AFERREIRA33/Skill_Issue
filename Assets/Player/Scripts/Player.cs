using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int MaxHp = 100;
    public HPBar hpbar;
    private int currentHP;
    // Start is called before the first frame update
    void Start()
    {
        currentHP = MaxHp;
        hpbar.UpdateHPSlider(currentHP, MaxHp);
    }


    public void OnTakeDamage(int damage)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    private bool isGuard = false;
    private float currentHealth = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTakeDamage(float damage)
    {

        if (!isGuard)
        {
            currentHealth -= damage;
            //hpbar.UpdateHPSlider(currentHP, MaxHp);
            if (currentHealth <= 0)
            {
                Destroy(this.gameObject);
                //hpbar.UpdateHPSlider(currentHP, MaxHp);
            }
            Debug.Log(currentHealth);
        }
    }
}

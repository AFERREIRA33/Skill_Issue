using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Vitality_Drain_Debuf : Cards
{
    void Start()
    {
        cardName = "Vitality Drain Debuff";
        description = "reduce enemy health bar to 65hp";
        weight = 0;
        reusable = true;
        activable = false;
    }
    public override void UseCard(bool isPlayer)
    {
        if (isPlayer)
        {
            GameObject ia = GameObject.FindGameObjectWithTag("Ia");
            ia.GetComponent<IaController>().maxHealth += 25;
            ia.GetComponent<IaController>().currentHealth += 25;
        }
        else
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            // pas  trouver fonction
            player.GetComponent<Player>().MaxHp = 65;
            player.GetComponent<Player>().currentHP = 65;

        }
    }
}

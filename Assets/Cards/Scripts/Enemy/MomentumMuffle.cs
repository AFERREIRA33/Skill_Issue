using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomentumMuffle : Cards
{
    void Start()
    {
        cardName = "Momentum Muffle";
        description = "reduce move speed by 10%";
        weight = 0;
        reusable = true;
        activable = false;
    }
    public override void UseCard(bool isPlayer)
    {
        if (isPlayer)
        {
            GameObject.FindGameObjectWithTag("Ia").GetComponent<IaController>().speed *= 0.90f;


        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().speed *= 0.90f;
        }
    }
}

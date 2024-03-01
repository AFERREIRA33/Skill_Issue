using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VelocityVeil : Cards
{
    void Start()
    {
        cardName = "Velocity Veil";
        description = "reduce firing speed by 5%";
        weight = 0;
        reusable = true;
        activable = false;
    }
    public override void UseCard(bool isPlayer)
    {
        if (isPlayer)
        {
            GameObject.FindGameObjectWithTag("Ia").GetComponent<IaController>().coolDownShoot *= 1.05f;
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Aim>().coolDownShoot *= 1.05f;

        }
    }
}

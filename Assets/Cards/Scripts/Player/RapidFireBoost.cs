using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RapidFireBoost : Cards
{
    void Start()
    {
        cardName = "Rapid fire boost";
        description = "Increase shooting speed by 25%";
        weight = 0;
        reusable = true;
        activable = false;
    }
    public override void UseCard(bool isPlayer)
    {
        if (isPlayer)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            // pas  trouver fonction
            player.GetComponent<Player>().speedUpShoot = true;
        }
        else
        {
            GameObject.FindGameObjectWithTag("Ia").GetComponent<IaController>().coolDownShoot *= 0.75f;
        }
    }
}

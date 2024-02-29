using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDashMastery : Cards
{
    void Start()
    {
        cardName = "Double Dash Mastery";
        description = "Can do Double Dash";
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
            player.GetComponent<Player>().doubleDash = true;
        }
        else
        {

        }
    }
}

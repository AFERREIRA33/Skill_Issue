using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonGrenade : Cards
{
    // Start is called before the first frame update
    void Start()
    {
        cardName = "Poison Grenade";
        description = "Create a poison zone in the aimed direction";
        weight = 0;
        reusable = true;
        activable = true;
    }
    public override void UseCard(bool isPlayer)
    {
        if (isPlayer)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            // pas trouver la fonction
        }
        else
        {

        }
    }

}

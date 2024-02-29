using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShieldedAugmentation : Cards
{
    void Start()
    {
        cardName = "Shielded Augmentation";
        description = "Add a shield of 25 HP";
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
            player.GetComponent<Player>().Shield();
        }
        else
        {

        }
    }
}

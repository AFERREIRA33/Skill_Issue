using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CloakedManeuver : Cards
{
    void Start()
    {
        cardName = "Cloaked Maneuver";
        description = "Invisibility (10 seconds) as long as no touch or blow is given (not reusable)";
        weight = 0;
        reusable = false;
        activable = false;
    }
    public override void UseCard(bool isPlayer)
    {
        if (isPlayer)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            // pas  trouver fonction
            player.GetComponent<Player>().Invisible();
        }
        else
        {

        }
    }
}

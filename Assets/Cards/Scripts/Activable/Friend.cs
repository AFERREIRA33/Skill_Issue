using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Friend : Cards
{
    // Start is called before the first frame update
    void Start()
    {
        cardName = "Friend";
        description = "Summon a turret for 10 seconds with 20 HP";
        weight = 0;
        reusable = true;
        activable = true;
    }
    public override void UseCard(bool isPlayer)
    {
        if(isPlayer)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Player>().SpawnTurret();
        } else
        {

        }
    }
}

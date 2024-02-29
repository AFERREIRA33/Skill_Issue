using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Invincible : Cards
{
    // Start is called before the first frame update
    void Start()
    {
        cardName = "Invincible";
        description = "Frames of invincibility for 3 seconds";
        weight = 0;
        reusable = true;
        activable = true;
    }
    public override void UseCard(bool isPlayer)
    {
        if (isPlayer)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Player>().Invicibility();
        }
        else
        {

        }
    }
}

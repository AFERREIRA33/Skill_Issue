using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StunBullet : Cards
{
    // Start is called before the first frame update
    void Start()
    {
        cardName = "Stun Bullet";
        description = "A bullet that stuns the enemy for 3 seconds";
        weight = 0;
        reusable = true;
        activable = true;
    }
    public override void UseCard(bool isPlayer)
    {
        if (isPlayer)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            // pas  trouver fonction
        }
        else
        {

        }
    }
}

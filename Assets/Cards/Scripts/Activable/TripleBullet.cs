using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TripleBullet : Cards
{
    // Start is called before the first frame update
    void Start()
    {
        cardName = "Triple Bullet";
        description = "Fire 3 bullets for 20 seconds on 'W'";
        weight = 0;
        reusable = true;
        activable = true;
    }
    public override void UseCard(bool isPlayer)
    {
        if (isPlayer)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Player>().MultiShoot();
        }
        else
        {

        }
    }
}

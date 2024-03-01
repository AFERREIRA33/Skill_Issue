using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DistractingMirage : Cards
{
    void Start()
    {
        cardName = "Distracting mirage";
        description = "Reduce time of guard by 10%";
        weight = 0;
        reusable = true;
        activable = false;
    }
    public override void UseCard(bool isPlayer)
    {
        if (isPlayer)
        {
            GameObject.FindGameObjectWithTag("Ia").GetComponent<IaController>().timerGuard *= 00.90f;

        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().timerGuard *= 0.90f;

        }
    }
}

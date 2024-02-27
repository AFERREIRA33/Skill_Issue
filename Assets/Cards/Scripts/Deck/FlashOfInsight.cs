using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlashOfInsight : Cards
{
    // Start is called before the first frame update
    void Start()
    {
        cardName = "Flash of Insight";
        description = "Reveal the next 2 cards of your opponent";
        weight = 0;
        reusable = true;
    }

    public override void UseCard()
    {

    }
}

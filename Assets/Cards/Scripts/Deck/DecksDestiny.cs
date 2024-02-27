using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecksDestiny : Cards
{
    // Start is called before the first frame update
    void Start()
    {
        cardName = "Deck's Destiny";
        description = "Reveal the next 3 cards of your deck";
        weight = 0;
        reusable = true;
    }

    public override void UseCard()
    {

    }
}

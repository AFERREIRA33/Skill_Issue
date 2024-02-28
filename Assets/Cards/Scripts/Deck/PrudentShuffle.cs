using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrudentShuffle : Cards
{
    // Start is called before the first frame update
    void Start()
    {
        cardName = "Prudent Shuffle";
        description = "Choose one of the remaining cards from your deck and put it on top of the deck";
        weight = 0;
        reusable = true;
    }
    public override void UseCard(bool isPlayer)
    {

    }
}

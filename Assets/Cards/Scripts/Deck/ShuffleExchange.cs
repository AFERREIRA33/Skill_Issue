using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShuffleExchange : Cards
{
    // Start is called before the first frame update
    void Start()
    {
        cardName = "Shuffle Exchange";
        description = "Exchange one of the cards in your deck with one already drawn";
        weight = 0;
        reusable = true;
    }

    public override void UseCard()
    {

    }
}

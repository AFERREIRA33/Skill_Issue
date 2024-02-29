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

    public override void UseCard(bool isPlayer)
    {
        Debug.Log(isPlayer);
        List<GameObject> deck;
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (isPlayer)
        {
            deck = gameManager.deckTemp;
        }
        else
        {
            deck = gameManager.deckEnemyTemp;
        }

        if (deck.Count <= 2 && deck.Count > 0)
        {
            for (int i = 0; i < deck.Count; i++)
            {
                Debug.Log(deck[i].GetComponent<Cards>().cardName);
            }
        }
        else if (deck.Count >= 3)
        {
            for (int i = 0; i < 2; i++)
            {
                Debug.Log(deck[i].GetComponent<Cards>().cardName);
            }
        }
        else
        {
            Debug.Log("no more card");
        }
    }
}

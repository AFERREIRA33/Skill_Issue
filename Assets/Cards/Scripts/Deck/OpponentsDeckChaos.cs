using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentsDeckChaos : Cards
{
    // Start is called before the first frame update
    void Start()
    {
        cardName = "Opponent's Deck Chaos";
        description = "Shuffle your opponent's deck";
        weight = 0;
        reusable = true;
    }

    public override void UseCard(bool isPlayer)
    {
        Debug.Log(isPlayer);
        List<GameObject> deck;
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (!isPlayer)
        {
            deck = gameManager.deckTemp;
        }
        else
        {
            deck = gameManager.deckEnemyTemp;
        }

        int loop = deck.Count;
        int index;
        List<GameObject> deckShuffle = new List<GameObject>();
        for (int i = 0; i < loop; i++)
        {
            index = Random.Range(0, deck.Count);
            deckShuffle.Add(deck[index]);
            deck.RemoveAt(index);
        }

        if (!isPlayer)
        {
            gameManager.deckTemp = deckShuffle;
        }
        else
        {
            gameManager.deckEnemyTemp = deckShuffle;
        }

    }
}

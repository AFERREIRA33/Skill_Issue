using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> allCards;
    public List<GameObject> deck;
    public void SaveDeck()
    {
        GameObject allCardsButtons = GameObject.FindGameObjectWithTag("AllCards");
        GameObject deckButtons = GameObject.FindGameObjectWithTag("Deck");
        allCards.Clear();
        foreach (ChangeDeck card in allCardsButtons.GetComponentsInChildren<ChangeDeck>())
        {
            allCards.Add(card.card);
        }
        deck.Clear();
        foreach (ChangeDeck card in deckButtons.GetComponentsInChildren<ChangeDeck>())
        {
            deck.Add(card.card);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    public List<GameObject> allCards;
    public List<GameObject> deck;
    public List<GameObject> deckEnemy;
    private void Start()
    {
        deckEnemy = allCards;
    }
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
    public void LoadPlayTest()
    {
        SceneManager.LoadScene("Test_Game_Part_Draw_Card");
    }
}

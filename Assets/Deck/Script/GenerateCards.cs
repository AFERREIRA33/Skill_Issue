using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class GenerateCards : MonoBehaviour
{
    private static GameManager gameManager;
    public GameObject buttonCard;
    public Transform scrollViewContentAllCards;
    public Transform scrollViewContentDeck;
    public Button add;
    void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
        foreach (GameObject card in gameManager.deck)
        {
            GameObject button = Instantiate(buttonCard, scrollViewContentDeck);
            button.GetComponentInChildren<TextMeshProUGUI>().text = card.GetComponent<Cards>().cardName;
            button.GetComponent<ChangeDeck>().card = card;
            button.GetComponent<ChangeDeck>().scrollViewContentAllCards = scrollViewContentAllCards;
            button.GetComponent<ChangeDeck>().scrollViewContentDeck = scrollViewContentDeck;
            button.GetComponent<ChangeDeck>().inDeck = true;
            button.GetComponent<ChangeDeck>().add = add;
            button.GetComponent<Button>().onClick.AddListener(button.GetComponent<ChangeDeck>().GetInfo);
        }
        foreach (GameObject card in gameManager.allCards)
        {
            GameObject button = Instantiate(buttonCard, scrollViewContentAllCards);
            button.GetComponentInChildren<TextMeshProUGUI>().text = card.GetComponent<Cards>().cardName;
            button.GetComponent<ChangeDeck>().card = card;
            button.GetComponent<ChangeDeck>().scrollViewContentAllCards = scrollViewContentAllCards;
            button.GetComponent<ChangeDeck>().scrollViewContentDeck = scrollViewContentDeck;
            button.GetComponent<ChangeDeck>().inDeck = false;
            button.GetComponent<ChangeDeck>().add = add;
            button.GetComponent<Button>().onClick.AddListener(button.GetComponent<ChangeDeck>().GetInfo);
        }
    }
}

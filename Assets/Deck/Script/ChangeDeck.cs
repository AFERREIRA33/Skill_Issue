using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;


public class ChangeDeck : MonoBehaviour
{
    public bool inDeck;
    public bool isExchange = false;
    public GameObject card;
    public Transform scrollViewContentAllCards;
    public Transform scrollViewContentDeck;
    public Button add;

    public void GetInfo()
    {
        GameObject.FindGameObjectWithTag("Title").GetComponent<TextMeshProUGUI>().text = card.GetComponent<Cards>().cardName;
        GameObject.FindGameObjectWithTag("Description").GetComponent<TextMeshProUGUI>().text = card.GetComponent<Cards>().description;
        add.onClick.RemoveAllListeners();
        add.onClick.AddListener(Add);
    }
    public void Add()
    {
        if (inDeck)
        {
            transform.SetParent(scrollViewContentAllCards, true);
            inDeck = !inDeck;
        }
        else
        {
            bool alreadyHaveActive = false;
            if (card.GetComponent<Cards>().activable) 
            { 
                foreach(Transform cardInDeck in scrollViewContentDeck.transform)
                {
                    if (cardInDeck.gameObject.GetComponent<ChangeDeck>().card.GetComponent<Cards>().activable)
                    {
                        alreadyHaveActive = true;
                        break;
                    }
                }
                if (!alreadyHaveActive)
                {
                    transform.SetParent(scrollViewContentDeck, true);
                    inDeck = !inDeck;
                } else
                {
                    Debug.Log("Already have activable");
                }
            }
            else
            {
                transform.SetParent(scrollViewContentDeck, true);
                inDeck = !inDeck;
            }
        }
    }

    public void SwitchPrudentShuffle()
    {
        if (inDeck)
        {
            if (scrollViewContentAllCards.GetComponentsInChildren<Button>().Length == 0)
            {
                transform.SetParent(scrollViewContentAllCards, true);
                inDeck = !inDeck;
                add.onClick.RemoveAllListeners();
                add.onClick.AddListener(GoUp);
            }
        }
        else
        {
            add.onClick.RemoveAllListeners();
            transform.SetParent(scrollViewContentDeck, true);
            inDeck = !inDeck;
        }
    }

    public void switchShuffleExchange()
    {
        bool alreadyExchange = false;
        if (inDeck)
        {
            if (isExchange)
            {
                transform.SetParent(scrollViewContentAllCards, true);
                isExchange = false;
                inDeck = !inDeck;

            }
            else
            {
                foreach (Transform cardDeck in scrollViewContentAllCards.transform)
                {
                    if (cardDeck.gameObject.GetComponent<ChangeDeck>().isExchange)
                    {
                        alreadyExchange = true;
                        break;
                    }
                }
                if (!alreadyExchange)
                {
                    isExchange = true;
                    transform.SetParent(scrollViewContentAllCards, true);
                    inDeck = !inDeck;
                }
                else
                {
                    Debug.Log("already exchange");
                }
            }
        }
        else
        {
            if (isExchange)
            {
                transform.SetParent(scrollViewContentDeck, true);
                isExchange = false;
                inDeck = !inDeck;
            }
            else
            {
                foreach (Transform cardDeck in scrollViewContentDeck.transform)
                {
                    if (cardDeck.gameObject.GetComponent<ChangeDeck>().isExchange)
                    {
                        alreadyExchange = true;
                        break;
                    }
                }
                if (!alreadyExchange)
                {
                    isExchange = true;
                    transform.SetParent(scrollViewContentDeck, true);
                    inDeck = !inDeck;

                }
                else
                {
                    Debug.Log("already exchange");
                }
            }
        }

    }

    public void GoUp()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (scrollViewContentAllCards.GetComponentsInChildren<Button>().Length > 0)
        {
            List<GameObject> newDeck = new List<GameObject>();
            newDeck.Add(card);
            foreach (GameObject cardDeck in gameManager.deckTemp)
            {
                if (card.name != cardDeck.name)
                {
                    newDeck.Add(cardDeck);
                }
            }
            gameManager.deckTemp = newDeck;
            foreach (Transform button in scrollViewContentAllCards.transform)
            {
                Destroy(button.gameObject);
            }
            foreach (Transform button in scrollViewContentDeck.transform)
            {
                Destroy(button.gameObject);
            }
            add.onClick.RemoveAllListeners();
            gameManager.hudModif.SetActive(false);
        }
    }

    public void ExchangeCard()
    {
        bool alreadyExchangeDeck = false;
        bool alreadyExchangeDraw = false;
        GameObject deckExchange = new GameObject();
        GameObject drawExchange = new GameObject();
        GameManager gameManager = FindObjectOfType<GameManager>();
        foreach (Transform cardDeck in scrollViewContentDeck.transform)
        {
            if (cardDeck.gameObject.GetComponent<ChangeDeck>().isExchange)
            {
                deckExchange = cardDeck.gameObject.GetComponent<ChangeDeck>().card;
                alreadyExchangeDeck = true;
                break;
            }
        }
        foreach (Transform cardDeck in scrollViewContentAllCards.transform)
        {
            if (cardDeck.gameObject.GetComponent<ChangeDeck>().isExchange)
            {
                drawExchange = cardDeck.gameObject.GetComponent<ChangeDeck>().card;
                alreadyExchangeDraw = true;
                break;
            }
        }
        if (alreadyExchangeDeck && alreadyExchangeDraw)
        {
            List<GameObject> deck = new List<GameObject>(gameManager.deckTemp);
            List<GameObject> draw = new List<GameObject>(gameManager.cardDraw);
            foreach (GameObject card in draw)
            {
                if (card.name == deckExchange.name)
                {
                    gameManager.cardDraw[draw.IndexOf(card)] = drawExchange;
                    break;
                }
            }
            foreach (GameObject card in deck)
            {
                if (card.name == drawExchange.name)
                {
                    gameManager.deckTemp[deck.IndexOf(card)] = deckExchange;
                    break;
                }
            }
            foreach (Transform button in scrollViewContentAllCards.transform)
            {
                Destroy(button.gameObject);
            }
            foreach (Transform button in scrollViewContentDeck.transform)
            {
                Destroy(button.gameObject);
            }
            add.onClick.RemoveAllListeners();
            gameManager.hudModif.SetActive(false);
        }
    }
}

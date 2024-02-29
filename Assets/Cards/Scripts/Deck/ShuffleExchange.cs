using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    public override void UseCard(bool isPlayer)
    {
        List<GameObject> deck;
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager.cardDraw.Count > 0 && gameManager.deckTemp.Count > 0)
        {
            if (isPlayer)
            {
                gameManager.hudModif.SetActive(true);
                deck = Shuffle(gameManager.deckTemp);
                List<GameObject> cardDraw = Shuffle(gameManager.cardDraw);
                Transform scrollViewContentDeck = GameObject.FindGameObjectWithTag("Deck").GetComponent<RectTransform>();
                Transform scrollViewContentAllCards = GameObject.FindGameObjectWithTag("AllCards").GetComponent<RectTransform>();
                Button add = GameObject.FindGameObjectWithTag("ButtonModif").GetComponent<Button>();

                foreach (GameObject card in deck)
                {
                    GameObject button = Instantiate(gameManager.buttonCard, scrollViewContentDeck);
                    button.GetComponentInChildren<TextMeshProUGUI>().text = card.GetComponent<Cards>().cardName;
                    button.GetComponent<ChangeDeck>().card = card;

                    button.GetComponent<ChangeDeck>().scrollViewContentAllCards = scrollViewContentAllCards;
                    button.GetComponent<ChangeDeck>().scrollViewContentDeck = scrollViewContentDeck;

                    button.GetComponent<ChangeDeck>().inDeck = true;

                    button.GetComponent<Button>().onClick.RemoveAllListeners();
                    button.GetComponent<Button>().onClick.AddListener(button.GetComponent<ChangeDeck>().switchShuffleExchange);


                    button.GetComponent<ChangeDeck>().add = add;

                }
                foreach (GameObject card in cardDraw)
                {
                    GameObject button = Instantiate(gameManager.buttonCard, scrollViewContentAllCards);
                    button.GetComponentInChildren<TextMeshProUGUI>().text = card.GetComponent<Cards>().cardName;
                    button.GetComponent<ChangeDeck>().card = card;

                    button.GetComponent<ChangeDeck>().scrollViewContentAllCards = scrollViewContentAllCards;
                    button.GetComponent<ChangeDeck>().scrollViewContentDeck = scrollViewContentDeck;

                    button.GetComponent<ChangeDeck>().inDeck = false;

                    button.GetComponent<Button>().onClick.RemoveAllListeners();
                    button.GetComponent<Button>().onClick.AddListener(button.GetComponent<ChangeDeck>().switchShuffleExchange);

                    add.onClick.RemoveAllListeners();
                    add.onClick.AddListener(button.GetComponent<ChangeDeck>().ExchangeCard);
                    button.GetComponent<ChangeDeck>().add = add;

                }
            }
            else
            {
                deck = Shuffle(gameManager.deckEnemyTemp);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PrudentShuffle : Cards
{
    public Button switchB;
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
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager.deckTemp.Count > 0) 
        {
            List<GameObject> deck;
            if (isPlayer)
            {
                gameManager.hudModif.SetActive(true);
                deck = Shuffle(gameManager.deckTemp);
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
                    button.GetComponent<Button>().onClick.AddListener(button.GetComponent<ChangeDeck>().SwitchPrudentShuffle);


                    button.GetComponent<ChangeDeck>().add = add;

                }
            }
            else
            {
                // Enemy prudent shuffle
                deck = Shuffle(gameManager.deckEnemyTemp);
            }
        }
    }
}

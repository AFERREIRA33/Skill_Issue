using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
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
            Time.timeScale = 0;
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
                    EventSystem.current.SetSelectedGameObject(button);


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
                    EventSystem.current.SetSelectedGameObject(button);


                }
            }
            else
            {
                int poidChoiceBase = -1;
                int poidChoiceMin = 200;
                GameObject ChooseCardDeckTemp = new GameObject();
                GameObject ChooseCardDraw = new GameObject();
                int randomValue;
                if (gameManager.deckEnemyTemp.Count - 1 > 0 && gameManager.enemyCardDraw.Count - 1 > 0)
                {
                    for (int i = 0; i < gameManager.deckEnemyTemp.Count; i++)
                    {
                        if (gameManager.deckEnemyTemp[i].GetComponent<Cards>().weight > poidChoiceBase)
                        {
                            ChooseCardDeckTemp = gameManager.deckEnemyTemp[i];
                            poidChoiceBase = ChooseCardDeckTemp.GetComponent<Cards>().weight;
                        }
                        else if (gameManager.deckEnemyTemp[i].GetComponent<Cards>().weight == poidChoiceBase)
                        {
                            randomValue = Random.Range(0, 2);
                            if (randomValue == 1)
                            {
                                ChooseCardDeckTemp = gameManager.deckEnemyTemp[i];
                                poidChoiceBase = ChooseCardDeckTemp.GetComponent<Cards>().weight;
                            }

                        }
                    }
                    for (int j = 0; j < gameManager.enemyCardDraw.Count; j++)
                    {
                        if (gameManager.enemyCardDraw[j].GetComponent<Cards>().weight < poidChoiceMin)
                        {
                            ChooseCardDraw = gameManager.enemyCardDraw[j];
                            poidChoiceMin = ChooseCardDraw.GetComponent<Cards>().weight;
                        }
                        else if (gameManager.enemyCardDraw[j].GetComponent<Cards>().weight == poidChoiceMin)
                        {
                            randomValue = Random.Range(0, 2);
                            if (randomValue == 1)
                            {
                                ChooseCardDraw = gameManager.enemyCardDraw[j];
                                poidChoiceMin = ChooseCardDraw.GetComponent<Cards>().weight;
                            }

                        }
                    }
                    deck = new List<GameObject>(gameManager.deckTemp);
                    List<GameObject> draw = new List<GameObject>(gameManager.cardDraw);
                    foreach (GameObject card in draw)
                    {
                        if (card.name == ChooseCardDeckTemp.GetComponent<Cards>().name)
                        {
                            gameManager.cardDraw[draw.IndexOf(card)] = ChooseCardDraw;
                            break;
                        }
                    }
                    foreach (GameObject card in deck)
                    {
                        if (card.name == ChooseCardDraw.GetComponent<Cards>().name)
                        {
                            gameManager.deckTemp[deck.IndexOf(card)] = ChooseCardDeckTemp;
                            break;
                        }
                    }
                }
            }
        }
    }
}

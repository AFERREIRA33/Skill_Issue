using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
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
            Time.timeScale = 0;
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
                    EventSystem.current.SetSelectedGameObject(button);
                }
            }
            else
            {
                // Enemy prudent shuffle
                deck = Shuffle(gameManager.deckEnemyTemp);
                int poidChoiceBase = -1;
                GameObject ChooseCard = new GameObject();
                int randomValue;
                if (gameManager.deckEnemyTemp.Count - 1 > 0)
                {
                    for (int i = 0; i < gameManager.deckEnemyTemp.Count; i++)
                    {
                        if (gameManager.deckEnemyTemp[i].GetComponent<Cards>().weight > poidChoiceBase)
                        {
                            ChooseCard = gameManager.deckEnemyTemp[i];
                            poidChoiceBase = ChooseCard.GetComponent<Cards>().weight;
                        }
                        else if (gameManager.deckEnemyTemp[i].GetComponent<Cards>().weight == poidChoiceBase)
                        {
                            randomValue = Random.Range(0, 2);
                            if (randomValue == 1)
                            {
                                ChooseCard = gameManager.deckEnemyTemp[i];
                                poidChoiceBase = ChooseCard.GetComponent<Cards>().weight;
                            }

                        }
                    }
                    Debug.Log(ChooseCard.GetComponent<Cards>().cardName);
                    List<GameObject> newDeck = new List<GameObject>();
                    newDeck.Add(ChooseCard);
                    foreach (GameObject cardDeck in gameManager.deckTemp)
                    {
                        if (ChooseCard.name != cardDeck.name)
                        {
                            newDeck.Add(cardDeck);
                        }
                    }
                }
                Time.timeScale = 1;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
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
            GameObject.FindGameObjectWithTag("Info").GetComponent<TextMeshProUGUI>().text = "";
            for (int i = 0; i < deck.Count; i++)
            {
                GameObject.FindGameObjectWithTag("Info").GetComponent<TextMeshProUGUI>().text += deck[i].GetComponent<Cards>().cardName+ "\n";
            }
        }
        else if (deck.Count >= 3)
        {
            GameObject.FindGameObjectWithTag("Info").GetComponent<TextMeshProUGUI>().text = "";
            for (int i = 0; i < 3; i++)
            {
                GameObject.FindGameObjectWithTag("Info").GetComponent<TextMeshProUGUI>().text += deck[i].GetComponent<Cards>().cardName + "\n";
            }
        }
        else
        {
            GameObject.FindGameObjectWithTag("Info").GetComponent<TextMeshProUGUI>().text = "no more card";
        }
    }
}

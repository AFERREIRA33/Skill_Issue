using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FlashOfInsight : Cards
{
    // Start is called before the first frame update
    void Start()
    {
        cardName = "Flash of Insight";
        description = "Reveal the next 2 cards of your opponent";
        weight = 0;
        reusable = true;
    }

    public override void UseCard(bool isPlayer)
    {
        List<GameObject> deck;
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (!isPlayer)
        {
            deck = gameManager.deckTemp;
        } else
        {
            deck = gameManager.deckEnemyTemp;
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (deck.Count == 1)
        {
            GameObject.FindGameObjectWithTag("Info").GetComponent<TextMeshProUGUI>().text = deck[0].GetComponent<Cards>().cardName;
        } else if(deck.Count >= 2) 
        {
            GameObject.FindGameObjectWithTag("Info").GetComponent<TextMeshProUGUI>().text = "";
            for (int i = 0;  i < 2; i++)
            {
                GameObject.FindGameObjectWithTag("Info").GetComponent<TextMeshProUGUI>().text += deck[i].GetComponent<Cards>().cardName+ "\n";
            }
        } else
        {
            GameObject.FindGameObjectWithTag("Info").GetComponent<TextMeshProUGUI>().text = "no more card";
        }
    }
}

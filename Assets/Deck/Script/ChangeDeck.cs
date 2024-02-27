using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;


public class ChangeDeck : MonoBehaviour
{
    public bool inDeck;
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
            transform.SetParent(scrollViewContentAllCards,true);
        } else
        {
            transform.SetParent(scrollViewContentDeck, true);
        }
        inDeck = !inDeck;
    }
}

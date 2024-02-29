using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        deckEnemy = FuseList(allCards, deck);
    }
    public List<GameObject> allCards;

    public List<GameObject> deck;
    public List<GameObject> deckTemp;
    public GameObject playerCard;
    public List<GameObject> cardDraw;

    public List<GameObject> deckEnemy;
    public List<GameObject> deckEnemyTemp;
    public GameObject enemyCard;
    public List<GameObject> enemyCardDraw;

    public GameObject hudModif;
    public GameObject buttonCard;
    public bool isFinish = false;
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

    private List<GameObject> FuseList(List<GameObject> list1, List<GameObject> list2)
    {
        /*
        int index;
        List<GameObject> list = list1.Union(list2).ToList();
        List<GameObject> result = new List<GameObject>();
        for (int i = 0; i < 10; i++)
        {
            index = Random.Range(0, list.Count);
            result.Add(list[index]);
            list.RemoveAt(index);
        }
        return result;
        */
        return list1.Union(list2).ToList();
    }
}

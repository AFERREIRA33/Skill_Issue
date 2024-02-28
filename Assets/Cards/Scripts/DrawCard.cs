using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class DrawCard : MonoBehaviour
{
    private static GameManager gameManager;
    public TextMeshProUGUI titlePlayer;   
    public TextMeshProUGUI descriptionPlayer;
    public TextMeshProUGUI titleEnemy;
    public TextMeshProUGUI descriptionEnemy;
    // Start is called before the first frame update
    void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
        if (gameManager.deckTemp.Count == 0)
        {
            gameManager.deckTemp = Shuffle(gameManager.deck);
            gameManager.deckEnemyTemp = Shuffle(gameManager.deckEnemy); 
        }
        Draw();
    }

    public void Draw()
    {
        if (gameManager.deckTemp.Count > 0 && gameManager.deckEnemyTemp.Count > 0)
        {
            GameObject myCard = gameManager.deckTemp[0];
            titlePlayer.text = myCard.GetComponent<Cards>().cardName;
            descriptionPlayer.text = myCard.GetComponent<Cards>().description;
            gameManager.playerCard = myCard;
            gameManager.deckTemp.RemoveAt(0);
            myCard.GetComponent<Cards>().UseCard(true);

            GameObject enemyCard = gameManager.deckEnemyTemp[0];
            titleEnemy.text = enemyCard.GetComponent<Cards>().cardName;
            descriptionEnemy.text = enemyCard.GetComponent<Cards>().description;
            gameManager.playerCard = enemyCard;
            gameManager.deckEnemyTemp.RemoveAt(0);
            enemyCard.GetComponent<Cards>().UseCard(false);

        } else
        {
            titleEnemy.text = "no more card";
            descriptionEnemy.text = "";
            titlePlayer.text = "no more card";
            descriptionPlayer.text = "";
        }
    }

    private List<GameObject> Shuffle(List<GameObject> deck)
    {
        int loop = deck.Count;
        int index;
        List<GameObject> deckShuffle = new List<GameObject>();
        for (int i = 0; i < loop; i++) 
        {
            index = Random.Range(0, deck.Count);
            deckShuffle.Add(deck[index]);
            deck.RemoveAt(index);
        }
        return deckShuffle;
    }
}

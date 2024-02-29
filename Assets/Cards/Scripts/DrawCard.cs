using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
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
        gameManager.hudModif = GameObject.FindGameObjectWithTag("HudModif");
        if (gameManager.hudModif != null)
        {
            gameManager.hudModif.SetActive(false);
        }

        if (gameManager.deckTemp.Count == 0 && !gameManager.isFinish)
        {
            gameManager.deckTemp = Shuffle(gameManager.deck);
            gameManager.deckEnemyTemp = Shuffle(gameManager.deckEnemy); 
        }
        Draw();
        EventSystem.current.SetSelectedGameObject(GameObject.FindGameObjectWithTag("Start"));
    }

    public void Draw()
    {
        if (gameManager.deckTemp.Count > 0)
        {
            GameObject a = new GameObject();
            GameObject myCard = gameManager.deckTemp[0];
            titlePlayer.text = myCard.GetComponent<Cards>().cardName;
            descriptionPlayer.text = myCard.GetComponent<Cards>().description;
            gameManager.playerCard = myCard;
            gameManager.deckTemp.RemoveAt(0);

            GameObject enemyCard = gameManager.deckEnemyTemp[0];
            titleEnemy.text = enemyCard.GetComponent<Cards>().cardName;
            descriptionEnemy.text = enemyCard.GetComponent<Cards>().description;
            gameManager.enemyCard = enemyCard;
            gameManager.deckEnemyTemp.RemoveAt(0);
            
            //myCard.GetComponent<Cards>().UseCard(true);
            //enemyCard.GetComponent<Cards>().UseCard(false);

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
        List<GameObject> deckToShuffle = new List<GameObject>(deck);
        int loop = deckToShuffle.Count;
        int index;
        List<GameObject> deckShuffle = new List<GameObject>();
        for (int i = 0; i < loop; i++) 
        {
            index = Random.Range(0, deckToShuffle.Count);
            deckShuffle.Add(deckToShuffle[index]);
            deckToShuffle.RemoveAt(index);
        }
        return deckShuffle;
    }

    // Temporaire : Lancera le niveau de duel et sera mis à jour la bas
    public void LoadPlayTest()
    {
        if (gameManager.deckTemp.Count > 0)
        {
            gameManager.cardDraw.Add(gameManager.playerCard);
            gameManager.enemyCardDraw.Add(gameManager.enemyCard);
        } else
        {
            gameManager.isFinish = true;
        }
        
        SceneManager.LoadScene("Map_5");
    }
}

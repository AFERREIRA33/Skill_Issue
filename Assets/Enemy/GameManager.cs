using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> allCards;

    public List<GameObject> deck;
    public List<GameObject> deckTemp;
    private GameObject playerCard;
    public List<GameObject> cardDraw;

    public List<GameObject> deckEnemy;
    public List<GameObject> deckEnemyTemp;
    private GameObject enemyCard;
    public List<GameObject> enemyCardDraw;
    // Start is called before the first frame update
    void Start()
    {
        enemyCardDraw = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

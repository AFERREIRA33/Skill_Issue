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

    void IaPioche()
    {
        int poidChoiceBase = -1;
        GameObject ChooseCard;
        int randomValue;
        if (deckEnemyTemp.Count - 1 > 0)
        {
            for(int i=0; i < deckEnemyTemp.Count; i++)
            {
                if (deckEnemyTemp[i].GetComponent<Cards>().weight > poidChoiceBase)
                {
                    ChooseCard= deckEnemyTemp[i];
                    poidChoiceBase = ChooseCard.GetComponent<Cards>().weight;
                }else if (deckEnemyTemp[i].GetComponent<Cards>().weight == poidChoiceBase)
                {
                    randomValue = Random.Range(0, 2);
                    if(randomValue == 1)
                    {
                        ChooseCard = deckEnemyTemp[i];
                        poidChoiceBase = ChooseCard.GetComponent<Cards>().weight;
                    }

                }
            }
        }
    }
}

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
    // Start is called before the first frame update
    void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
        Draw();
    }

    public void Draw()
    {
        GameObject myCard = gameManager.deck[Random.Range(0, gameManager.deck.Count)];
        titlePlayer.text = myCard.GetComponent<Cards>().cardName;
        descriptionPlayer.text = myCard.GetComponent<Cards>().description;

    }
}

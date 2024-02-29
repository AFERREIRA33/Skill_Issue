using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour
{
    public string cardName;
    public string description;
    public int weight;
    public bool reusable;
    public bool activable;
    public virtual void UseCard(bool isPlayer){}

    protected List<GameObject> Shuffle(List<GameObject> deck)
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
}

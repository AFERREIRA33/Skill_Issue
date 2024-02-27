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
    public virtual void UseCard(){}
}

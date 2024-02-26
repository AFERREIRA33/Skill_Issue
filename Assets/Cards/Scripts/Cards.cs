using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour
{
    public string CardName;
    public string Description;
    public int weight;
    public bool reusable;
    public virtual void UseCard(){}
}

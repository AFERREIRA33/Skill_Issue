using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{
    public float damage = 0.1f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == ("Player"))
        {
            //collision.GetComponent<Player>().OnTakeDamage(damage);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage = 20;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //collider.GetComponent<Enemy>().OnTakeDamage(damage);
    }
}

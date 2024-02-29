using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage = 20;

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider != null)
        {
            //collider.GetComponent<Enemy>().OnTakeDamage(damage);

            if (collider.tag == "PlayerTurret" /*|| collider.tag == "EnemyTurret"*/)
            {
                collider.gameObject.GetComponent<Turret>().OnTakeDamage(damage);
            }
        }

    }
}

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
            if (collider.gameObject.tag == "Ia")
            {
                collider.GetComponent<IaController>().OnTakeDamage(damage);
            }

            if (collider.tag == "PlayerTurret" /*|| collider.tag == "EnemyTurret"*/)
            {
                collider.gameObject.GetComponent<Turret>().OnTakeDamage(damage);
            }

            //if (collider.tag == "ProjectileEnemy")
            //{
                
            //}
        }

    }
}

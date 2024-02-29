using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (gameObject.transform.parent.tag != "PlayerTurret" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "ProjectilePlayer")
        {
            gameObject.transform.GetComponentInParent<Turret>().targetTransform = collision.transform;
            gameObject.transform.GetComponentInParent<Turret>().isInArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.transform.GetComponentInParent<Turret>().isInArea = false;
    }
}

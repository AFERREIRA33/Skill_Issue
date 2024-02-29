using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardDetection : MonoBehaviour
{
    private bool enemyDetected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemyDetected= true;
        }
    }

    public bool GetDetection()
    {
        return enemyDetected;
    }

    public void SetDetection(bool newDetection)
    {
        enemyDetected=newDetection;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameTest : MonoBehaviour
{
    public GameObject ia;
    public GameObject player;
    private float speed = 2f;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) {
            ia.GetComponent<IaController>().OnTakeDamage(51);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            player.transform.position = player.transform.position + Vector3.up;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            player.transform.position = player.transform.position + Vector3.down;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            player.transform.position = player.transform.position + Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            player.transform.position = player.transform.position + Vector3.right;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTest : MonoBehaviour
{
    public GameObject ia;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D)) {
            ia.GetComponent<IaController>().OnTakeDamage(50);
        }
    }
}

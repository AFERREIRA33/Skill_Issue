using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IaController : MonoBehaviour
{
    private GameObject player;
    private float distance = 1.3f;
    public Tilemap obstacle;
    private List<Vector2> nextMove = new List<Vector2>();
    private bool findPath = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        findPath= true;
        SearchPath();
        this.transform.position = nextMove[0];
    }

    void SearchPath()
    {
        while (findPath)
        {

        }
    }
}

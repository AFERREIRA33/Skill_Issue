using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IaController : MonoBehaviour
{
    private GameObject player;
    private float distance = 1.3f;
    public Tilemap obstacle;
    private List<Vector3> nextMove = new List<Vector3>();
    private bool findPath = true;
    private Vector3 savePathCross;
    private Dictionary<string, float> pathDico = new Dictionary<string, float>() { 
        { "up", 0f },
        { "down", 0f }, 
        { "left", 0f },
        { "right", 0f } };

    private Tile testedTilePath;
    private Vector3 posTested;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //findPath= true;
        SearchPath();
        //this.transform.position = nextMove[0];
    }

    void SearchPath()
    {
        while (findPath)
        {

            pathDico["up"] = Vector3.Distance(new Vector3(transform.position.x, transform.position.y + distance, 0), player.transform.position);
            pathDico["down"] = Vector3.Distance(new Vector3(transform.position.x, transform.position.y - distance, 0), player.transform.position);
            pathDico["left"] = Vector3.Distance(new Vector3(transform.position.x - distance, transform.position.y, 0), player.transform.position);
            pathDico["right"] = Vector3.Distance(new Vector3(transform.position.x + distance, transform.position.y, 0), player.transform.position);
            float min = pathDico.Values.Min();
            string pathDicoKey = pathDico.First(entry => entry.Value == min).Key;

            if (NoWall(pathDicoKey))
            {

            }

            findPath= false;
        }
    }

    bool NoWall(string posTry)
    {
        if(posTry == "up")
        {
            posTested = new Vector3(transform.position.x, transform.position.y + distance, 0);
        }else if(posTry == "down")
        {
            posTested = new Vector3(transform.position.x, transform.position.y - distance, 0);
        }
        else if (posTry == "right")
        {
            posTested = new Vector3(transform.position.x + distance, transform.position.y, 0);
        }
        else if (posTry == "left")
        {
            posTested = new Vector3(transform.position.x - distance, transform.position.y, 0);
        }

        testedTilePath = obstacle.GetTile<Tile>(new Vector3Int(Mathf.FloorToInt(posTested.x), Mathf.FloorToInt(posTested.y), Mathf.FloorToInt(posTested.z)));
        if (testedTilePath != null)
        {
            return false;
        }
        return true;
    }
}

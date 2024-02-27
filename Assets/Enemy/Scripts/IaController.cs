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
    private bool findPath = true;
    private List<Vector3> pathList = new List<Vector3>();
    private List<Vector3> upPath = new List<Vector3>();
    private List<Vector3> downPath = new List<Vector3>();
    private Dictionary<string, float> pathDico = new Dictionary<string, float>() { 
        { "up", 0f },
        { "down", 0f }, 
        { "left", 0f },
        { "right", 0f } };

    private Tile testedTilePath;
    private Vector3 posTested;
    private GameObject targetPath;
    private Vector3 saveLastPos = new Vector3(-20000,-20000,-20000);
    bool move = false;
    private float timer = 0;
    private float timeSearch = 0;
    private int count = 0;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        targetPath = player;
        pathList.Add(transform.position);
    }

    // Update is called once per frame
    void Update()
    {

        SearchPath();
        if (Vector3.Distance(transform.position, targetPath.transform.position) < 2f)
        {
            if(count == 0)
            {

                findPath = true;
            }
            count += 1;
            
        }

        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            
            timer = 0;
            move = true;

        }

        if (move)
        {
            if(pathList.Count -1> 0)
            {
                this.transform.position = pathList[0];
                pathList.RemoveAt(0);
                move = false;
            }
            
        }
    }

    void SearchPath()
    {
        int count = 0;
        posTested = transform.position;
        while (findPath)
        {
            saveLastPos = posTested;
            pathDico["up"] = Vector3.Distance(new Vector3(posTested.x, posTested.y + distance, -1), targetPath.transform.position);
            pathDico["down"] = Vector3.Distance(new Vector3(posTested.x, posTested.y - distance, -1), targetPath.transform.position);
            pathDico["left"] = Vector3.Distance(new Vector3(posTested.x - distance, posTested.y, -1), targetPath.transform.position);
            pathDico["right"] = Vector3.Distance(new Vector3(posTested.x + distance, posTested.y, -1), targetPath.transform.position);
            

            float min = pathDico.Values.Min();
            string pathDicoKey = pathDico.First(entry => entry.Value == min).Key;
            setPosTested(pathDicoKey, saveLastPos);
            if (pathList.Count - 1 > 0)
            {
                while (pathList.Contains(posTested))
                {
                    pathDico[pathDicoKey] = 2147483647;
                    min = pathDico.Values.Min();
                    pathDicoKey = pathDico.First(entry => entry.Value == min).Key;
                    setPosTested(pathDicoKey, saveLastPos);
                }
            }

            while (!NoWall())
            {
                pathDico[pathDicoKey] = 2147483647;
                min = pathDico.Values.Min();
                pathDicoKey = pathDico.First(entry => entry.Value == min).Key;
                setPosTested(pathDicoKey, saveLastPos);


                if (pathList.Count - 1 > 0)
                {
                    while (pathList.Contains(posTested))
                    {
                        pathDico[pathDicoKey] = 2147483647;
                        min = pathDico.Values.Min();
                        pathDicoKey = pathDico.First(entry => entry.Value == min).Key;
                        setPosTested(pathDicoKey, saveLastPos);


                    }
                }

            }

            
            pathList.Add(posTested);

            if (Vector3.Distance(posTested, targetPath.transform.position) < 1f)
            {
                findPath = false;

            }
            pathDico.Clear();
            count += 1;
        }

    
    }



    void setPosTested(string posTry, Vector3 newTestesPos)
    {
        if (posTry == "up")
        {
            posTested = new Vector3(newTestesPos.x, newTestesPos.y + distance, -1);
        }
        else if (posTry == "down")
        {
            posTested = new Vector3(newTestesPos.x, newTestesPos.y - distance, -1);
        }
        else if (posTry == "right")
        {
            posTested = new Vector3(newTestesPos.x + distance, newTestesPos.y, -1);
        }
        else if (posTry == "left")
        {
            posTested = new Vector3(newTestesPos.x - distance, newTestesPos.y, -1);
        }
    }

    bool NoWall()
    {


        testedTilePath = obstacle.GetTile<Tile>(new Vector3Int(Mathf.FloorToInt(posTested.x), Mathf.FloorToInt(posTested.y), Mathf.FloorToInt(0)));
        if (testedTilePath != null)
        {
            return false;
        }
        
        return true;
    }


}

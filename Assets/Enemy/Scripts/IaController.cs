using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.GraphicsBuffer;

public class IaController : MonoBehaviour
{
    private GameObject player;
    public Tilemap obstacle;
    private bool findPath = true;
    private List<Vector3> pathList = new List<Vector3>();
    private Dictionary<string, float> pathDico = new Dictionary<string, float>() { 
        { "up", 0f },
        { "down", 0f }, 
        { "left", 0f },
        { "right", 0f } };

    private Tile testedTilePath;
    private Vector3 posTested;
    private GameObject targetPath;
    private Vector3 saveLastPos = new Vector3(-20000,-20000,-20000);
    private int count = 0;
    private float speed = 2.0f;
    private bool findPlayer = false;
    private List<GameObject> myTargetCard= new List<GameObject>();
    private static GameManager gameManager;
    private GameObject childDetectionPlayer;
    private bool resetPathLIst = false;
    private Vector3 lastPosReach;
    private bool search = true;

    private bool invincibility = false;
    private bool grenade = false;
    private bool stunBall = false;
    private bool threeBallShoot = false;
    private bool turret = false;
    private bool hasActivable = false;

    private int currentHealth = 100;
    private int maxHealth = 100;
    private int damageSword = 20;
    private int damageShoot = 10;
    private int layer_mask;
    private bool shootReload = false;
    private GameObject swordRangeDetectionChild;
    private GameObject swordDamagerChild;
    private GameObject swordSpriteChild;
    private bool swordAttackReload = false;
    private GameObject shootRangeDetectionChild;

    private bool isGuard = false;
    public float timerGuard = 4f;
    public float reloadGuard = 2f;
    private GameObject guardDetectionChild;

    private float distanceTargetMin = 1f;
    private bool assault = true;
    private bool saveYourLife = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        targetPath = player;
        pathList.Add(transform.position);

        guardDetectionChild = transform.GetChild(0).gameObject;
        childDetectionPlayer = transform.GetChild(1).gameObject;
        swordRangeDetectionChild = transform.GetChild(2).gameObject;
        shootRangeDetectionChild = transform.GetChild(3).gameObject;
        swordDamagerChild= transform.GetChild(4).gameObject;
        swordSpriteChild= transform.GetChild(5).gameObject;

        layer_mask = LayerMask.GetMask("Wall");

        if (gameManager == null) {
            gameManager = FindObjectOfType<GameManager>();
        }
        myTargetCard = gameManager.GetComponent<GameManager>().enemyCardDraw;
        if (myTargetCard.Count -1 > 0)
        {
            targetPath = myTargetCard[0];
        }
        //SearchPath();
    }

    // Update is called once per frame
    void Update()
    {

        if (!childDetectionPlayer.GetComponent<PlayerDetection>().GetDetection() && !isGuard) {
            if (search)
            {
                SearchPath();
                search = false;
                resetPathLIst = true;
            }
            if (Mathf.FloorToInt(Vector3.Distance(transform.position, lastPosReach)) <= 2)
            {
                if(myTargetCard.Count -1 > 0)
                {
                    myTargetCard.RemoveAt(0);
                    if(myTargetCard.Count -1 >0)
                    {
                        targetPath= myTargetCard[0];
                    }
                    else
                    {
                        targetPath = player;
                    }
                }
                



                if((Mathf.FloorToInt(Vector3.Distance(transform.position, targetPath.transform.position)) > 2))
                {
                    findPath = true;
                    SearchPath();
                }

            }
            if (pathList.Count - 1 > 0)
            {
                transform.Translate(Vector3.Normalize(pathList[0] - transform.position) * Time.deltaTime * speed);
                
                if (Vector3.Distance(transform.position, pathList[0]) < 0.5f)
                {
                    pathList.RemoveAt(0);
                }
            }
        }
        else
        {
            if (resetPathLIst)
            {
                Debug.Log("test");
                search = true;
                pathList.Clear();
                resetPathLIst= false;
                findPath = true;
                ReachPlayer();
            }
            if (Mathf.FloorToInt(Vector3.Distance(transform.position, posTested)) <= 2)
            {
                ReachPlayer();
            }
            transform.Translate(Vector3.Normalize(posTested - transform.position) * Time.deltaTime * speed);

        }



        //gestion attack player
        if (guardDetectionChild.GetComponent<GuardDetection>().GetDetection() && currentHealth < 50)
        {
            if (timerGuard > 0)
            {
                timerGuard -= Time.deltaTime;
                isGuard = true;
                guardDetectionChild.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                guardDetectionChild.GetComponent<GuardDetection>().SetDetection(false);
                guardDetectionChild.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        else
        {
            if (timerGuard < 4 && reloadGuard <= 0)
            {
                timerGuard = 4f;
                reloadGuard = 2f;
            }
            else if (isGuard || reloadGuard < 2)
            {
                reloadGuard = -Time.deltaTime;

                isGuard = false;
            }
        }

        if (swordRangeDetectionChild.GetComponent<SwordDetection>().GetDetection() && !isGuard)
        {
            if (!swordAttackReload)
            {
                swordDamagerChild.SetActive(true);
                swordSpriteChild.SetActive(true);
                StartCoroutine(WaitUntilNextAttack());
                StartCoroutine(HideSwordSprite());
                swordAttackReload = true;
            }
        }else if (hasActivable)
        {
            if(invincibility && currentHealth < 25 && guardDetectionChild.GetComponent<GuardDetection>().GetDetection() && !isGuard)
            {
                Debug.Log("use Invincibility");
            }
            else if(invincibility && currentHealth < 50 && shootRangeDetectionChild.GetComponent<ShootDetection>().GetDetection() && !isGuard)
            {
                Debug.Log("Call turret");
            }
            else if((grenade || threeBallShoot || stunBall) && shootRangeDetectionChild.GetComponent<ShootDetection>().GetDetection() && !isGuard)
            {
                if (grenade)
                {
                    Debug.Log("grenade");
                }
                else if (threeBallShoot)
                {
                    Debug.Log("three ball shoot");
                }
                else if(stunBall)
                {
                    Debug.Log("stun ball");
                }
            }
        }
        else if (shootRangeDetectionChild.GetComponent<ShootDetection>().GetDetection() && !isGuard)
        {

            if (!shootReload)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, -(transform.position - player.transform.position), Vector3.Distance(transform.position, player.transform.position), layer_mask);
                if (!hit)
                {

                    Debug.Log("Shoot");
                }
                shootReload= true;
                StartCoroutine(ReloadShoot());
            }

        }

    }

    public void OnTakeDamage(int damage)
    {
        
        if (!isGuard)
        {
            currentHealth -= damage;
            //hpbar.UpdateHPSlider(currentHP, MaxHp);
            if (currentHealth <= 0)
            {
                Destroy(this.gameObject);
                //hpbar.UpdateHPSlider(currentHP, MaxHp);
            }else if(currentHealth <= 50)
            {
                SwitchMode(true);
            }
            
        }
    }

    void SwitchMode(bool safe) 
    {
        if (safe)
        {
            assault = false;
            saveYourLife= true;
            distanceTargetMin = 5f;
        }
        else
        {
            assault = true;
            saveYourLife = false;
            distanceTargetMin = 1f;
        }
    }


    void ReachPlayer()
    {
        pathDico["up"] = Vector3.Distance(transform.position + Vector3.up, player.transform.position);
        pathDico["down"] = Vector3.Distance(transform.position + Vector3.down, player.transform.position);
        pathDico["left"] = Vector3.Distance(transform.position + Vector3.left, player.transform.position);
        pathDico["right"] = Vector3.Distance(transform.position + Vector3.right, player.transform.position);

        float min = pathDico.Values.Min();
        string pathDicoKey = pathDico.First(entry => entry.Value == min).Key;
        setPosTested(pathDicoKey, transform.position);


        while (!NoWall(posTested))
        {
            pathDico[pathDicoKey] = 2147483647;
            min = pathDico.Values.Min();
            pathDicoKey = pathDico.First(entry => entry.Value == min).Key;
            setPosTested(pathDicoKey, transform.position);

        }


    }


    // Pathfinding Function
    void SearchPath()
    {
        int count = 0;
        posTested = transform.position;
        pathList.Clear();
        lastPosReach = targetPath.transform.position;
        while (findPath)
        {
            saveLastPos = posTested;
            pathDico["up"] = Vector3.Distance(posTested + Vector3.up, targetPath.transform.position);
            pathDico["down"] = Vector3.Distance(posTested + Vector3.down, targetPath.transform.position);
            pathDico["left"] = Vector3.Distance(posTested + Vector3.left, targetPath.transform.position);
            pathDico["right"] = Vector3.Distance(posTested + Vector3.right, targetPath.transform.position);
            

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

            while (!NoWall(posTested))
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

            if (Vector3.Distance(posTested, targetPath.transform.position) < distanceTargetMin)
            {
                findPath = false;

            }
            count += 1;
        }

    
    }

    //path finding function
    void setPosTested(string posTry, Vector3 newTestesPos)
    {
        if (posTry == "up")
        {
            posTested = newTestesPos + Vector3.up;
        }
        else if (posTry == "down")
        {
            posTested = newTestesPos + Vector3.down;
        }
        else if (posTry == "right")
        {
            posTested = newTestesPos + Vector3.right;
        }
        else if (posTry == "left")
        {
            posTested = newTestesPos + Vector3.left;
        }
    }

    //path finding function
    bool NoWall(Vector3 posToTest)
    {


        testedTilePath = obstacle.GetTile<Tile>(new Vector3Int(Mathf.FloorToInt(posToTest.x), Mathf.FloorToInt(posToTest.y), Mathf.FloorToInt(0)));
        if (testedTilePath != null)
        {
            return false;
        }
        
        return true;
    }

    IEnumerator SearchTimer()
    {
        yield return new WaitForSeconds(1.5f);
        count = 0;
    }

    public int GetDamageSword()
    {
        return damageSword;
    }

    IEnumerator WaitUntilNextAttack()
    {
        yield return new WaitForSeconds(2f);
        swordAttackReload = false;
    }
    IEnumerator HideSwordSprite()
    {
        yield return new WaitForSeconds(1f);
        swordDamagerChild.SetActive(false);
        swordSpriteChild.SetActive(false);
    }

    IEnumerator ReloadShoot()
    {
        yield return new WaitForSeconds(0.5f);
        shootReload = false;
    }
}

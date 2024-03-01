using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public float speed = 2.0f;
    private bool findPlayer = false;
    private List<GameObject> myTargetCard= new List<GameObject>();
    private static GameManager gameManager;
    private GameObject childDetectionPlayer;
    private bool resetPathLIst = false;
    private Vector3 lastPosReach;
    private bool search = true;
    private float lastDistancePlayer;

    private bool invincibility = false;
    private bool grenade = false;
    private bool stunBall = false;
    private bool threeBallShoot = false;
    private bool turret = false;
    private bool hasActivable = false;

    public float currentHealth = 100;
    public float maxHealth = 100;
    private float damageSword = 20;
    private float damageShoot = 10;
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

    public Transform targetTransform;
    public GameObject projectile;
    public  bool canFire = false;

    private float invisibleTimer = 10f;

    [HideInInspector] public bool stopInvisible = true;
    public bool invisible = false;
    [HideInInspector] public float coolDownShoot = 2f;
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
        if (gameManager.enemyCard.GetComponent<Cards>().activable)
        {
            //GameObject.FindGameObjectWithTag("Activable").GetComponent<TextMeshProUGUI>().text = gameManager.playerCard.GetComponent<Cards>().cardName;
            //hasActivable = gameManager.enemyCard;
        }
        else
        {
            gameManager.enemyCard.GetComponent<Cards>().UseCard(false);
        }
        myTargetCard = gameManager.enemyCardDraw;
        //if (myTargetCard.Count -1 > 0)
        //{
        //    targetPath = myTargetCard[0];
        //}
        //SearchPath();
    }

    // Update is called once per frame
    void Update()
    {
        if (invisible)
        {
            Invisible();
        }
        if (!childDetectionPlayer.GetComponent<PlayerDetection>().GetDetection() && !isGuard) {
            if (search)
            {
                findPath = true;
                SearchPath();
                search = false;
                resetPathLIst = true;
            }
            if (Mathf.FloorToInt(Vector3.Distance(transform.position, lastPosReach)) <= distanceTargetMin)
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

            }
            if (pathList.Count - 1 > 0)
            {
                transform.Translate(Vector3.Normalize(pathList[0] - transform.position) * Time.deltaTime * speed);
                
                if (Vector3.Distance(transform.position, pathList[0]) < 0.5f)
                {
                    pathList.RemoveAt(0);
                }
            }
            else
            {
                if ((Mathf.FloorToInt(Vector3.Distance(transform.position, targetPath.transform.position)) > distanceTargetMin))
                {

                    findPath = true;
                    SearchPath();
                }
            }
        }
        else if(childDetectionPlayer.GetComponent<PlayerDetection>().GetDetection() && !isGuard)
        {
            if (resetPathLIst)
            {
                search = true;
                pathList.Clear();
                resetPathLIst= false;
                ReachPlayer();
            }
            if (Mathf.FloorToInt(Vector3.Distance(transform.position, player.transform.position)) >= distanceTargetMin)
            {   
                ReachPlayer();
                lastDistancePlayer = Vector3.Distance(transform.position, player.transform.position);
            }
            else
            {
                if (lastDistancePlayer > Vector3.Distance(transform.position, player.transform.position) && saveYourLife)
                {
                    Debug.Log("test");
                    MoveBack();
                    lastDistancePlayer = Vector3.Distance(transform.position, player.transform.position);
                }

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
                search = true;
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
                Vector3 rotation = player.transform.position - transform.position;
                float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                //swordRangeDetectionChild.transform.RotateAround(transform.position, Vector3.forward, rotZ);
                swordSpriteChild.transform.position = 1.0f * Vector3.Normalize(player.transform.position - transform.position) + transform.position;
                swordSpriteChild.transform.rotation = Quaternion.Euler(0, 0, rotZ);
                swordRangeDetectionChild.transform.position = 1.0f * Vector3.Normalize(player.transform.position - transform.position) + transform.position;
                swordRangeDetectionChild.transform.rotation = Quaternion.Euler(0, 0, rotZ);


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
                    canFire = false;
                    Fire();

                }
                shootReload = true;
                StartCoroutine(ReloadShoot());
            }

        }

    }
    private void Fire()
    {
        projectile.GetComponent<TurretProjectile>().target = targetTransform;
        Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);
        StartCoroutine(CoolDown());
    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(coolDownShoot);
        canFire = true;
    }

    public void OnTakeDamage(float damage)
    {
        
        if (!isGuard)
        {
            currentHealth -= damage;
            Debug.Log(currentHealth + "/" + maxHealth); 
            //hpbar.UpdateHPSlider(currentHP, MaxHp);
            if (currentHealth <= 0)
            {
                gameManager.WhoWin(false);
                SceneManager.LoadScene("Test_Game_Part_Draw_Card");
                //hpbar.UpdateHPSlider(currentHP, MaxHp);
            }
            else if(currentHealth <= 50)
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
            distanceTargetMin = 4f;
        }
        else
        {
            assault = true;
            saveYourLife = false;
            distanceTargetMin = 1f;
        }
    }

    void MoveBack()
    {
        pathDico["up"] = Vector3.Distance(transform.position + Vector3.up, player.transform.position);
        pathDico["down"] = Vector3.Distance(transform.position + Vector3.down, player.transform.position);
        pathDico["left"] = Vector3.Distance(transform.position + Vector3.left, player.transform.position);
        pathDico["right"] = Vector3.Distance(transform.position + Vector3.right, player.transform.position);
        float max = pathDico.Values.Max();
        string pathDicoKey = pathDico.First(entry => entry.Value == max).Key;
        setPosTested(pathDicoKey, transform.position);

        while (!NoWall(posTested))
        {
            pathDico[pathDicoKey] = 0;
            max = pathDico.Values.Max();
            pathDicoKey = pathDico.First(entry => entry.Value == max).Key;
            setPosTested(pathDicoKey, transform.position);

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

    public float GetDamageSword()
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
        yield return new WaitForSeconds(coolDownShoot);
        shootReload = false;
    }

    public void Invisible()
    {
        Debug.Log("Toto");
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(TimeInvisible());
        if (stopInvisible)
        {
            invisibleTimer = 0;

        }
        if (invisibleTimer <= 0)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    IEnumerator TimeInvisible()
    {
        yield return new WaitForSeconds(invisibleTimer);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}

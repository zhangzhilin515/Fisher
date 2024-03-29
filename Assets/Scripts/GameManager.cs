using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject Sand;
    public GameObject initShark;//场景中原来存在的鲨鱼
    public GameObject initSeaMonster;
    private Vector3 SeaMonsterPosition;
    public Vector3[] sandPositions;
    public Vector3[] sharkPositions;
    public Player player;
    public Shark shark;//后来生成的鲨鱼
    public SeaMonster seamonster;
    public Text fishNumber;
    public Text fishspearNumber;
    public Text finalscore;
    public GameObject gameover;
    private bool sandGenerateRun=true;
    private bool EnemyGenerateRun=true;
   //public bool sandGenerateComplete=false;
    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else
        {
            if(instance!=this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        SetSandPositions();
        //SetEnemyPos();
        sharkPositions[0] = new Vector3(0, 1.5f, 1);
        Instantiate(initShark, sharkPositions[0], Quaternion.identity);
        initShark.SetActive(false);
        for (int i = 0; i <4; i++)
        {
            Instantiate(Sand, sandPositions[i], Quaternion.identity);
        }
        sandGenerateRun = true;
        //sandGenerateComplete = true;
    }
    void Update()
    {
        fishNumber.text = $"{player.getFishNumber}";
        fishspearNumber.text = $"{player.getFishSpearNumber}";
        finalscore.text = fishNumber.text;
        if (player.getFishNumber == 18 && sandGenerateRun == true)
        {
            for (int i = 4; i <15; i++)
            {
                Instantiate(Sand, sandPositions[i], Quaternion.identity);
            }
            sandGenerateRun = false;
            //sandGenerateComplete = true;
        }
        if(player.getFishNumber==18&&EnemyGenerateRun==true)
        {
            SetEnemyPos();
        }
        if (player.sharkUnableStepNumber == 0&&player.getFishSpearNumber==0)
        {
            if(player.transform.position.x == shark.transform.position.x && player.transform.position.y == shark.transform.position.y)
            {
                gameover.SetActive(true);
            }
        }
        if(player.getFishNumber>18)
        {
            if(player.transform.position.x == seamonster.transform.position.x && player.transform.position.y == seamonster.transform.position.y)
            {
                gameover.SetActive(true);
            }
        }
    }
    void SetSandPositions()
    {
        sandPositions[0] = new Vector3(0.5f, 0.6f, 0);
        sandPositions[1] = new Vector3(-0.5f, 0.6f, 0);
        sandPositions[2] = new Vector3(0.5f,-0.3f, -1);
        sandPositions[3] = new Vector3(-0.5f, -0.3f, -1);
        sandPositions[4] = new Vector3(-2f, 1.2f, 1);
        sandPositions[5] = new Vector3(-2, 0.2f, -1);
        sandPositions[6] = new Vector3(-2, 2.2f, 3);
        sandPositions[7] = new Vector3(1.5f, -1.3f, -4);
        sandPositions[8] = new Vector3(1, -1.8f, -5);
        sandPositions[9] = new Vector3(-0.5f, -1.3f, -4);
        sandPositions[10] = new Vector3(2, -0.8f, -3);
        sandPositions[11] = new Vector3(2.5f, 0.7f, 0);
        sandPositions[12] = new Vector3(0, 2.2f, 3);
        sandPositions[13] = new Vector3(1, 2.2f, 3);
        sandPositions[14] = new Vector3(2, 2.2f, 3);
    }
    void SetEnemyPos()
    {
        /*
        if(player.getFishNumber==0)
        {
            sharkPositions[0] = new Vector3(0, 1.5f, 1);
            Instantiate(initShark, sharkPositions[0], Quaternion.identity);
            initShark.SetActive(false);
        }
        */
        if(player.getFishNumber==18)
        {
            initSeaMonster.SetActive(true);
            SeaMonsterPosition = new Vector3(0.5f, -1f, -4);
            Instantiate(initSeaMonster, SeaMonsterPosition, Quaternion.identity);
            initSeaMonster.SetActive(false);
            EnemyGenerateRun = false;
        }
    }
}

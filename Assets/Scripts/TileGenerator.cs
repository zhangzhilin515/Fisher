using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public float Level;
    public List<Vector3> generatePositions = new List<Vector3>();
    private GameObject seaPrefab;
    public GameObject fish;
    public GameObject fishspear;
    private Vector3 generatePos;
    public Player player;
    public LayerMask Sand;
    public bool HasFish;
    public bool HasFishSpear;
    private bool seaGenerateRun;
    private bool fishGenerateRun;
    void Start()
    {
        fish = transform.GetChild(0).gameObject;
        fishspear = transform.GetChild(1).gameObject;
        seaPrefab = gameObject;
        HasFish = false;
        StartCoroutine(ChangeLevel());
    }
    void Update()
    {
    }
    
    IEnumerator ChangeLevel()
    {
        Level = 2;
        seaGenerateRun = true;
        fishGenerateRun = true;
        SeaPosSet();
        SeaGenerate();
        FishGenerate();
        FishSpearGenerate();
        yield return new WaitUntil(player.playerLevel1);
        Level = 4;
        seaGenerateRun = true;
        fishGenerateRun = true;
        SeaPosSet();
        SeaGenerate();
        FishGenerate();
        FishSpearGenerate();
        yield return new WaitUntil(player.playerLevel2);
    }
    
    void SeaPosSet()
    {
        generatePos = gameObject.transform.localPosition;
        generatePos += new Vector3(0.5f, 0.5f, 1);
        generatePositions.Add(generatePos);
        generatePos = gameObject.transform.localPosition;
        generatePos += new Vector3(-0.5f, 0.5f, 1);
        generatePositions.Add(generatePos);
        generatePos = gameObject.transform.localPosition;
        generatePos += new Vector3(0.5f, -0.5f, -1);
        generatePositions.Add(generatePos);
        generatePos = gameObject.transform.localPosition;
        generatePos += new Vector3(-0.5f, -0.5f, -1);
        generatePositions.Add(generatePos);
        generatePos = gameObject.transform.localPosition;
        generatePos += new Vector3(1, 0, 0);
        generatePositions.Add(generatePos);
        generatePos = gameObject.transform.localPosition;
        generatePos += new Vector3(-1,0,0);
        generatePositions.Add(generatePos);
    }
    void SeaGenerate()
    {
        if(seaGenerateRun==true)
        {
            for (int i = 0; i < Level * 6; i++)
            {
                for (int j = 0; j < generatePositions.Count; j++)
                {
                        if ((Mathf.Abs(generatePositions[j].x) + Mathf.Abs(generatePositions[j].y)) <= Level &&
                   !Physics2D.OverlapCircle(generatePositions[j], 0.1f) && Mathf.Abs(generatePositions[j].y) != Level)
                            Instantiate(seaPrefab, generatePositions[j], Quaternion.identity);
                    
                }
            }
            generatePositions.Clear();
            seaGenerateRun = false;
            //GameManager.instance.sandGenerateComplete = false;
        }
    }
    void FishGenerate()
    {
        if (fishGenerateRun == true)
        {
            if ((Mathf.Abs(transform.position.x) + Mathf.Abs(transform.position.y)) >= Level - 1 && HasFish == false)
            {
                fish.SetActive(true);
                HasFish = true;
            }
            fishGenerateRun = false;
        }
    }
    void FishSpearGenerate()
    {
        if(Level==2)
        {
            if (transform.position.x == 1 && transform.position.y == 0)
            {
                fishspear.SetActive(true);
                HasFishSpear = true;
            }
        }
        if(Level==4)
        {
            if (transform.position.x == -1.5 && transform.position.y == 0.5)
            {
                fishspear.SetActive(true);
                HasFishSpear = true;
            }
            if (transform.position.x == 2 && transform.position.y == 0)
            {
                fishspear.SetActive(true);
                HasFishSpear = true;
            }
            if (transform.position.x == -1 && transform.position.y == 2)
            {
                fishspear.SetActive(true);
                HasFishSpear = true;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.localPosition, 0.1f);
    }
}

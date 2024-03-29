using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]private Vector3 playerPos;
    public bool playerCanMove;
    private TileGenerator tg;
    void Start()
    {
        tg = GetComponent<TileGenerator>();
    }
    void Update()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        playerMove();
    }
    /*
    private void OnMouseEnter()
    {
        if(playerCanMove)
        {
            transform.localScale += Vector3.one * 0.5f;
        }
    }
    private void OnMouseExit()
    {
        if (playerCanMove)
        {
            transform.localScale -= Vector3.one * 0.5f;
        }
    }
    */
    private void OnMouseDown()
    {
        if(playerCanMove)
        {
            GameManager.instance.player.Move(transform.position);
        }
        if(tg.HasFish)
        {
            tg.fish.SetActive(false);
            tg.HasFish = false;
            GameManager.instance.player.getFishNumber++;
            GameManager.instance.player.startAudio = true;
        }
        if (tg.HasFishSpear)
        {
            tg.fishspear.SetActive(false);
            tg.HasFishSpear = false;
            GameManager.instance.player.getFishSpearNumber++;
        }
    }
    public void playerMove()
    {
        if (Mathf.Abs(playerPos.x - this.transform.position.x) + Mathf.Abs(playerPos.y - 0.5f - this.transform.position.y) <= 1
            && Mathf.Abs(playerPos.y - 0.5f - this.transform.position.y) < 1&&playerPos!=this.transform.position)
        {
            playerCanMove = true;
        }
        else
        {
            playerCanMove = false;
        }
    }
}

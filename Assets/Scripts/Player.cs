using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public int getFishNumber=0;
    public int getFishSpearNumber = 0;
    public int sharkUnableStepNumber;
    public int seamonsterUnableStepNumber;
    public bool startAudio;
    public AudioClip getfish;
    public AudioSource playerAudio;
    void Start()
    {
        GameManager.instance.player = this;
    }

    void Update()
    {
        
    }
    public void Move(Vector3 movePos)
    {
        StartCoroutine(MoveCo(movePos));
        if(getFishSpearNumber>0&&transform.position.x==GameManager.instance.shark.transform.position.x
            &&transform.position.y==GameManager.instance.shark.transform.position.y)
        {
            sharkUnableStepNumber = 10;
            getFishSpearNumber--;
        }
        else
        {
            if (sharkUnableStepNumber == 0)
            {
                if(movePos.x!=GameManager.instance.shark.transform.position.x||movePos.y+0.5f!=GameManager.instance.shark.transform.position.y)
                {
                    GameManager.instance.shark.FindPlayer();//如果鲨鱼没有被禁锢且玩家要行动的目标位置不是鲨鱼，则调动寻找
                }
            }
        }
        if (sharkUnableStepNumber > 0)
        {
            sharkUnableStepNumber--;
        }
        if (getFishNumber>18)//如果到达二阶段，再进行此判断
        {
            if (getFishSpearNumber > 0 && transform.position.x == GameManager.instance.seamonster.transform.position.x
            && transform.position.y == GameManager.instance.seamonster.transform.position.y)
            {
                seamonsterUnableStepNumber = 10;
                getFishSpearNumber--;
            }
            else
            {
                if (seamonsterUnableStepNumber == 0)
                {
                    if (movePos.x != GameManager.instance.seamonster.transform.position.x || movePos.y + 0.5f != GameManager.instance.seamonster.transform.position.y)
                    {
                        GameManager.instance.seamonster.FindPlayer();
                    }
                }
            }
            if (seamonsterUnableStepNumber > 0)
            {
                seamonsterUnableStepNumber--;
            }
        }
        if (startAudio == true)
        {
            playerAudio.clip = getfish;
            playerAudio.Play();
            startAudio = false;
        }
    }
    IEnumerator MoveCo(Vector3 movePos)
    {
        if(movePos.x<transform.position.x)
        {
            transform.localScale= new Vector3(-1,1,1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(movePos.x, movePos.y+0.5f), moveSpeed * Time.deltaTime);
        yield return null;
    }
    public bool playerLevel1()
    {
        if (getFishNumber == 18)
            return true;
        else
            return false;
    }
    public bool playerLevel2()
    {
        if (getFishNumber == 30)
            return true;
        else
            return false;
    }
}

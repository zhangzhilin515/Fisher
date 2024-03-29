using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaMonster : MonoBehaviour
{
    private List<Vector3> findPositions = new List<Vector3>();
    public List<Vector3> findWays = new List<Vector3>();
    private Vector3 findPos;
    public Vector3 targetPos;
    public LayerMask Sea;
    public GameObject player;
    public float moveSpeed;
    void Start()
    {
        GameManager.instance.seamonster = this;
    }

    void Update()
    {

    }
    public void FindPlayer()
    {
        findPos = gameObject.transform.localPosition;
        findPos += new Vector3(0.5f, 0, 1);
        findPositions.Add(findPos);
        findPos = gameObject.transform.localPosition;
        findPos += new Vector3(-0.5f, 0, 1);
        findPositions.Add(findPos);
        findPos = gameObject.transform.localPosition;
        findPos += new Vector3(0.5f, -1f, -1);
        findPositions.Add(findPos);
        findPos = gameObject.transform.localPosition;
        findPos += new Vector3(-0.5f, -1f, -1);
        findPositions.Add(findPos);
        findPos = gameObject.transform.localPosition;
        findPos += new Vector3(1, -0.5f, 0);
        findPositions.Add(findPos);
        findPos = gameObject.transform.localPosition;
        findPos += new Vector3(-1, -0.5f, 0);
        findPositions.Add(findPos);
        for (int i = 0; i < 6; i++)
        {
            if (Physics2D.OverlapCircle(findPositions[i], 0.1f, Sea))
            {
                findWays.Add(findPositions[i]);
            }
        }
        if(Distance(transform.position,player.transform.position)<=1f)
        {
            for (int j = 0; j < findWays.Count; j++)
            {
                if (Distance(findWays[0], -1*player.transform.position) >= Distance(findWays[j], -1*player.transform.position))
                {
                    findWays[0] = findWays[j];
                }
            }
        }
        else
        {
            for (int j = 0; j < findWays.Count; j++)
            {
                if (Distance(findWays[0], player.transform.position) >= Distance(findWays[j], player.transform.position))
                {
                    findWays[0] = findWays[j];
                }
            }
        }
        targetPos = new Vector3(findWays[0].x, findWays[0].y + 0.5f, findWays[0].z);
        StartCoroutine(MoveCo(targetPos));
        findPositions.Clear();
        findWays.Clear();
    }
    IEnumerator MoveCo(Vector3 movePos)
    {
        if (movePos.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(movePos.x, movePos.y), moveSpeed * Time.deltaTime);
        yield return null;
    }
    float Distance(Vector3 a, Vector3 b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }
}

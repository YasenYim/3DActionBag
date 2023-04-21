using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public float moveSpeed;  //移动速度，相当于PlayerController里的Input.GetAxis("Horizontal")
    Character character;

    Vector3 move;

    // 隔多久改变一次移动方向
    public float changeDirTime = 1;
    float lastChangeDirTime;

    void Start()
    {
        character = GetComponent<Character>();
    }

    void Update()
    {
        if (Time.time > lastChangeDirTime + changeDirTime)
        {
            Vector2 temp = Random.insideUnitCircle.normalized;
            move = new Vector3(temp.x, 0, temp.y);
            lastChangeDirTime = Time.time;
        }

        bool jump = false;
        if (Random.Range(0f, 1f) < 0.01f)   //%1 概率起跳
        {
            jump = true;
        }

        character.Move(move.x, move.z, jump);
    }

}

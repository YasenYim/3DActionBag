using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    public Transform catchObj;
    public Transform catchPos;
    public const int N = 20;  // 总道具数量

    List<ItemData> items = new List<ItemData>();

    // 查找第一个为空的格子
    public int FindFirstEmpty()
    {
        // 有空格，找空格
        for(int i =0; i<items.Count; i++)
        {
            if(items[i] == null)
            { 
                return i;
            }
        }

        // 背包彻底满了
        if(items.Count == N)
        { 
            return -1;
        }

        // 列表长度不足，拓容
        items.Add(null);
        return items.Count - 1;
    }

    public void CatchBox()
    {
        if(catchObj != null)
        {
            //如果有抓取物,释放
            catchObj.SetParent(null);
            catchObj.GetComponent<Rigidbody>().isKinematic = false;
            catchObj = null;
            animator.SetBool("Catch", false);
        }
        else
        {
            //如果没有抓取物，发射射线尝试抓取
            var dist = cc.radius;

            //接收射线返回的信息
            RaycastHit hit;
            //打一条线段起点、终点
            Debug.DrawLine(transform.position, transform.position + transform.forward * (dist + 1), Color.red, 10f);
            //打一条射线起点、方向、长度
            if(Physics.Raycast(transform.position,transform.forward,out hit , dist + 1))
            {
                if(hit.collider.CompareTag("GrabBox"))
                {
                    catchObj = hit.transform;
                    catchObj.SetParent(catchPos);
                    catchObj.localPosition = Vector3.zero;
                    catchObj.localRotation = Quaternion.identity;
                    catchObj.GetComponent<Rigidbody>().isKinematic = true;
                    animator.SetBool("Catch", true);
                }
            }
        }
    }

    // 添加随机道具
    public void AddRandomItem()
    {
        // 测试添加道具
        ItemData item = WorldItemManager.Instance.CreateItem(Random.Range(1001, 1005));

        // item加到背包列表中
        int index = FindFirstEmpty();

        if(index == -1)
        {
            Debug.Log("背包已满");
            return;
        }

        items[index] = item;

        // 刷新界面
        UIManager.Instance.SetItem(index, item);
    }

    // 自己丢弃道具的逻辑
    public void RemoveItem(ItemData item)
    {
        int index = items.IndexOf(item);
        if(index == -1)
        {
            Debug.Log("道具不存在！" + item.autoId + " " + item.Json.name);
            return;
        }

        /// !!!不能用RemoveAt，因为列表下标错乱
        items[index] = null;
        WorldItemManager.Instance.RemoveItem(item.autoId);

        // 刷新界面
        UIManager.Instance.SetItem(index, null);
    }

    // 自己交换逻辑
    public void SwapItem(ItemData item,int to)
    {
        int from = items.IndexOf(item);
        if(from == -1)
        {
            Debug.Log("道具不存在！" + item.autoId + " " + item.Json.name);
            return;
        }

        // to有可能超出items长度，拓容
        for(int i = items.Count; i <=to; i++)
        {
            items.Add(null);
        }
    
        // 交换逻辑
        items[from] = items[to];
        items[to] = item;

        // 刷新界面
        UIManager.Instance.SetItem(from, items[from]);
        UIManager.Instance.SetItem(to, items[to]);
    }

    // 还原道具
    public void RestoreItem(ItemData item)
    {
        int index = items.IndexOf(item);
        if (index == -1)
        {
            Debug.Log("道具不存在！" + item.autoId + " " + item.Json.name);
            return;
        }
        UIManager.Instance.SetItem(index, item);
    }

    // 捡起道具
    public void PickUp()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1);
        foreach(var collider in colliders)
        {
            if(collider.CompareTag("Model"))
            {
                ModelInfo info = collider.GetComponent<ModelInfo>();
                ItemData item = info.itemData;

                Destroy(collider.gameObject);

                // item加到背包列表中
                int index = FindFirstEmpty();

                if (index == -1)
                {
                    Debug.Log("背包已满");
                    return;
                }

                items[index] = item;

                // 刷新界面
                UIManager.Instance.SetItem(index, item);
            }
        }
    }


}

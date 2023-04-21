using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItemManager : MonoBehaviour
{
    int counter = 1;
    // Unity 单例

    public static WorldItemManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    Dictionary<int, ItemData> allItems = new Dictionary<int, ItemData>();

    void Start()
    {
       
    }

    public ItemData CreateItem(int jsonID)
    {
        ItemData item = new ItemData();

        item.autoId = counter;

        counter++;

        item.jsonId = jsonID;

        item.quality = Random.Range(1, 5);

        item.addAtk = Random.Range(10, 100);

        // 添加到全部道具字典中
        allItems.Add(item.autoId, item);

        return item;
    }

    public void RemoveItem(int autoID)
    {
        allItems.Remove(autoID);
    }
  
}

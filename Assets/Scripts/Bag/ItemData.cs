using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData 
{
    public int jsonId;   // Json ID
    public int autoId;   // Auto ID
    public int quality;  // Ʒ��
    public int addAtk;   // ���ӹ���ֵ

    public ItemJsonData Json
    {
        get
        {
            return JsonDataManager.Instance.GetItemJson(jsonId);
        }
    }
}

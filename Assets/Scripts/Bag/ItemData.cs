using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData 
{
    public int jsonId;   // Json ID
    public int autoId;   // Auto ID
    public int quality;  // 品质
    public int addAtk;   // 附加攻击值

    public ItemJsonData Json
    {
        get
        {
            return JsonDataManager.Instance.GetItemJson(jsonId);
        }
    }
}

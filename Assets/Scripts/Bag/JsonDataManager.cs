using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 保证与Json字段一致
public class ItemJsonData
{
    public int id { get; }
    public int type { get; }
    public string name { get; }
    public int atk { get; }
    public int def { get; }
    public string modelPath { get; }
    public string imagePath { get; }
    public float crit { get; }

    public ItemJsonData(int id,int type,string name,int atk,int def,string modelPath,string imagePath,float crit)
    {
        this.id = id;
        this.type = type;
        this.name = name;
        this.atk = atk;
        this.def = def;
        this.modelPath = modelPath;
        this.imagePath = imagePath;
        this.crit = crit;
    }
}



public class JsonDataManager 
{
    // C# 单例
    private static JsonDataManager instance;
    public static JsonDataManager Instance
    {
        get 
        {
            if(instance == null)
            {
                instance = new JsonDataManager();
                // instance初始化
                instance.InitItemJsonData();
            }
            return instance;
        }
    }


    // Dictionary，保存所有道具数据，<道具ID,ItemJsonData>
    Dictionary<int, ItemJsonData> items;

    // 把Json文件的内容，填到字典里
    private void InitItemJsonData()
    {
        items = new Dictionary<int, ItemJsonData>();

        // 其中，文件路径，省略Resources，省略后缀
        string str = Resources.Load<TextAsset>("Json/ItemData").text;

        // 把string转化成JsonData数据
        JsonData jd = JsonMapper.ToObject(str);

        for (int i = 0; i < jd.Count; i++)
        {
            int id = int.Parse(jd[i]["ID"].ToString());

            items.Add(id, 
                new ItemJsonData(id,
                    int.Parse(jd[i]["Type"].ToString()),
                    jd[i]["Name"].ToString(),
                    int.Parse(jd[i]["Attack"].ToString()),
                    int.Parse(jd[i]["Defense"].ToString()),
                    jd[i]["ModelPath"].ToString(),
                    jd[i]["ImagePath"].ToString(),
                    float.Parse(jd[i]["Crit"].ToString())
                ));
        }
    }


    public ItemJsonData GetItemJson(int id)
    {
        if(items.ContainsKey(id))
        {
            return items[id];
        }
        Debug.LogError($"道具ID{id} 不存在！");
        return null;
    }
}

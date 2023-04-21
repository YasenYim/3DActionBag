using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��֤��Json�ֶ�һ��
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
    // C# ����
    private static JsonDataManager instance;
    public static JsonDataManager Instance
    {
        get 
        {
            if(instance == null)
            {
                instance = new JsonDataManager();
                // instance��ʼ��
                instance.InitItemJsonData();
            }
            return instance;
        }
    }


    // Dictionary���������е������ݣ�<����ID,ItemJsonData>
    Dictionary<int, ItemJsonData> items;

    // ��Json�ļ������ݣ���ֵ���
    private void InitItemJsonData()
    {
        items = new Dictionary<int, ItemJsonData>();

        // ���У��ļ�·����ʡ��Resources��ʡ�Ժ�׺
        string str = Resources.Load<TextAsset>("Json/ItemData").text;

        // ��stringת����JsonData����
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
        Debug.LogError($"����ID{id} �����ڣ�");
        return null;
    }
}

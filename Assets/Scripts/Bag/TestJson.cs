using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJson : MonoBehaviour
{
    void Start()
    {
        // ���У��ļ�·����ʡ��Resources��ʡ�Ժ�׺
        string str = Resources.Load<TextAsset>("Json/ItemData").text;
        //Debug.Log(str);

        var item1001 = JsonDataManager.Instance.GetItemJson(1001);
        Debug.Log(item1001.imagePath);
        Debug.Log(item1001.modelPath);

        var item1004 = JsonDataManager.Instance.GetItemJson(1004);
        Debug.Log(item1004.imagePath);
        Debug.Log(item1004.modelPath);
    }
}

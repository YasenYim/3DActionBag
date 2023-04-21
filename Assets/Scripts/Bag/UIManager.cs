using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Tooltip("�������ӵĸ�����")]
    public Transform grid;

    [Tooltip("������Ϣ��")]
    public Transform infoPanel;

    [Tooltip("���ߴ��ڣ�����ȷ����Χ")]
    public RectTransform bagPanel;

    public Image prefabItemIcon;

    PlayerCharacter player;

    // Unity ����
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
    }

    // �ѵ���������ʾ��������
    public void SetItem(int index,ItemData itemdata)
    {
        Transform slot = grid.GetChild(index);

        // ɾ��ԭ���ĵ���ͼ��
        if(slot.childCount >0)
        {
            // ɾ����һ���ӽڵ�
            Destroy(slot.GetChild(0).gameObject);
        }

        if(itemdata != null)
        {
            // ���ݵ������ݣ�������Ӧͼ��
            Image image = Instantiate(prefabItemIcon,slot);

            // ��UIItem���棬������������
            UIItem uiitem = image.GetComponent<UIItem>();
            uiitem.data = itemdata;

            // ͼƬ
            image.sprite = Resources.Load<Sprite>(itemdata.Json.imagePath);
        }
    }

    public void ShowInfoPanel(ItemData item)
    {
        infoPanel.gameObject.SetActive(true);
        infoPanel.transform.GetChild(0).GetComponent<Text>().text = "����:" + item.Json.name;
        infoPanel.transform.GetChild(1).GetComponent<Text>().text = "����:" + item.Json.type.ToString();
        infoPanel.transform.GetChild(2).GetComponent<Text>().text = "Ʒ��:" + item.quality; 
    }

    public void HideInfoPanel()
    {
        infoPanel.gameObject.SetActive(false);
    }
   
    // uiitem������ק���߼�Ӧ��д��uimanager����
    public void OnItemEndDrag(ItemData item,PointerEventData evt)
    {
        // (������ڵĵ�)������ھ��η�Χ��
        if(!RectTransformUtility.RectangleContainsScreenPoint(bagPanel, evt.position))
        {
            // ��������
            player.RemoveItem(item);
            return;
        }

        bool swap = false;
        for(int i = 0;i < grid.childCount;i++)
        {
            RectTransform slot = grid.GetChild(i).GetComponent<RectTransform>();
            if(RectTransformUtility.RectangleContainsScreenPoint(slot,evt.position))
            {
                player.SwapItem(item, i);
                swap = true;
                break;
            }
        }
        // ���û�н������ͻ�ԭ
        if(!swap)
        {
            // ��ԭ����
            player.RestoreItem(item);
        }
    }
}

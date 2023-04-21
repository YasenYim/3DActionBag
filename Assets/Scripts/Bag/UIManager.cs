using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Tooltip("背包格子的父物体")]
    public Transform grid;

    [Tooltip("道具信息框")]
    public Transform infoPanel;

    [Tooltip("道具窗口，用于确定范围")]
    public RectTransform bagPanel;

    public Image prefabItemIcon;

    PlayerCharacter player;

    // Unity 单例
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
    }

    // 把道具数据显示到界面上
    public void SetItem(int index,ItemData itemdata)
    {
        Transform slot = grid.GetChild(index);

        // 删除原来的道具图标
        if(slot.childCount >0)
        {
            // 删除第一个子节点
            Destroy(slot.GetChild(0).gameObject);
        }

        if(itemdata != null)
        {
            // 根据道具数据，创建对应图标
            Image image = Instantiate(prefabItemIcon,slot);

            // 在UIItem上面，关联道具数据
            UIItem uiitem = image.GetComponent<UIItem>();
            uiitem.data = itemdata;

            // 图片
            image.sprite = Resources.Load<Sprite>(itemdata.Json.imagePath);
        }
    }

    public void ShowInfoPanel(ItemData item)
    {
        infoPanel.gameObject.SetActive(true);
        infoPanel.transform.GetChild(0).GetComponent<Text>().text = "名字:" + item.Json.name;
        infoPanel.transform.GetChild(1).GetComponent<Text>().text = "类型:" + item.Json.type.ToString();
        infoPanel.transform.GetChild(2).GetComponent<Text>().text = "品质:" + item.quality; 
    }

    public void HideInfoPanel()
    {
        infoPanel.gameObject.SetActive(false);
    }
   
    // uiitem结束拖拽的逻辑应该写在uimanager里面
    public void OnItemEndDrag(ItemData item,PointerEventData evt)
    {
        // (鼠标所在的点)如果不在矩形范围内
        if(!RectTransformUtility.RectangleContainsScreenPoint(bagPanel, evt.position))
        {
            // 丢弃道具
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
        // 如果没有交换，就还原
        if(!swap)
        {
            // 还原道具
            player.RestoreItem(item);
        }
    }
}

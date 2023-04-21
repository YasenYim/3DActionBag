using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;  // 引入命名空间

public class UIItem : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,
                                    IBeginDragHandler,IDragHandler,IEndDragHandler
   
{
    public ItemData data;

    Transform canvas;

    private void Start()
    {
        canvas = GameObject.Find("Canvas").transform;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(canvas);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        UIManager.Instance.OnItemEndDrag(data, eventData);
        Destroy(gameObject);
    }

    // 进入
    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.ShowInfoPanel(data);
    }

    // 退出
    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.HideInfoPanel();
    }
}

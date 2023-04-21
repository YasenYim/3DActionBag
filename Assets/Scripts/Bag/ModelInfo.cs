using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelInfo : MonoBehaviour
{
    public ItemData itemData;

    private void OnMouseEnter()
    {
        UIManager.Instance.ShowInfoPanel(itemData);
    }
    private void OnMouseExit()
    {
        UIManager.Instance.HideInfoPanel();
    }
}

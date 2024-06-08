using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemData : MonoBehaviour  {
    public int itemID, requiredItemID, speed;
    public Transform goToPoint, inventoryPosition;
    public Transform item;
    public Image itemImage, inventoryImage;

    public void changeSortingLayer(string sortingLayerName)  {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)  {
            renderer.sortingLayerName = sortingLayerName;
        }
    }
}
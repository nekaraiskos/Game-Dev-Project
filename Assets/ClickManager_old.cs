using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager_old : MonoBehaviour
{
    public Transform player;
    public void goToItem(ItemData item) {
        player.position = item.goToPoint.position;
    }
}

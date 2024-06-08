using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static float moveSpeed = 6f, itemMoveSpeed = 60f, moveAccuracy = 0.15f;
    //public static List<int> CollectedItems = new List<int>();
    public static List<itemData> CollectedItems = new List<itemData>();

    public IEnumerator MoveToPoint(Transform myObject, Vector2 point, float currMoveSpeed)  {

        Vector2 positionDifference = point - (Vector2)myObject.position;
        while (positionDifference.magnitude > moveAccuracy)  {
            myObject.Translate(currMoveSpeed * positionDifference.normalized * Time.deltaTime);
            positionDifference = point - (Vector2)myObject.position;
            yield return null;
        }
        myObject.position = point;
        yield return null;
    }
}

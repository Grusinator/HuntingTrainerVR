using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToTarget : MonoBehaviour
{
    public GameObject objectToMove;
    public GameObject objectToMoveTo;
    
    public void Teleport()
    {
        objectToMove.transform.SetPositionAndRotation(objectToMoveTo.transform.position, objectToMoveTo.transform.rotation);
    }
}

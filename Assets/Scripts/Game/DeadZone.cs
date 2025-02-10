using System;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private int FallenObjects;
    public Action<int> ObjectFallen;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ObjectOnTray objectOnTray = collision.GetComponent<ObjectOnTray>();
        if (objectOnTray != null)
        {
            FallenObjects++;
            ObjectFallen.Invoke(FallenObjects);
            Destroy(objectOnTray);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterScript : MonoBehaviour
{
    public void Activate()
    {
        TeleportPlayer.TeleportTo(gameObject);
    }
}

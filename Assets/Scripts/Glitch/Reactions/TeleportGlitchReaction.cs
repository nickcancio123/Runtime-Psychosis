using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGlitchReaction : GlitchReaction
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform teleportDestination;
    
    public override void React()
    {
        player.transform.position = teleportDestination.position;
    }
}

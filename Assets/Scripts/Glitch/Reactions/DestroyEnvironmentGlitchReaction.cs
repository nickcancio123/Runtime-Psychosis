using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnvironmentGlitchReaction : GlitchReaction
{
    [SerializeField] private List<GameObject> blocks;

    public override void React()
    {
        foreach (GameObject block in blocks)
            block.SetActive(false);
    }
}

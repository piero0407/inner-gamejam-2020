using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetReferenceColor : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;

    [SerializeField] new MeshRenderer renderer;

    Material prevMaterial;

    private void Update()
    {
        if(prevMaterial != renderer.sharedMaterial)
        {
            if(sprite != null || renderer != null)
            {
                if(renderer.sharedMaterial != null)
                {
                    sprite.color = renderer.sharedMaterial.GetColor("_EmissionColor");
                    prevMaterial = renderer.sharedMaterial;
                }
            }
        }
    }
}

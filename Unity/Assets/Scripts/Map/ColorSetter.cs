using GameLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSetter : MonoBehaviour
{

    [SerializeField]
    private List<Color> locationColors;

    [SerializeField]
    private MeshRenderer meshRenderer;

    public void Set(LocationType locationType)
    {
        meshRenderer.material.color = locationColors[(int)locationType];
    }
}

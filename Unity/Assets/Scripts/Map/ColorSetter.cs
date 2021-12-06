using GameLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSetter : MonoBehaviour
{

    [SerializeField]
    private Color forestColor;
    [SerializeField]
    private Color plainsColor;
    [SerializeField]
    private Color dunesColor;
    [SerializeField]
    private Color seaColor;
    [SerializeField]
    private Color oceanColor;
    [SerializeField]
    private Color riverColor;
    [SerializeField]
    private Color lakeColor;
    [SerializeField]
    private Color holyColor;
    [SerializeField]
    private Color darkColor;
    [SerializeField]
    private Color parkColor;
    [SerializeField]
    private Color miscColor;

    [SerializeField]
    private MeshRenderer meshRenderer;

    public void Set(LocationType locationType)
    {
        switch (locationType){
            case LocationType.FOREST:
                meshRenderer.material.color = forestColor;
                break;
            case LocationType.PLAINS:
                meshRenderer.material.color = plainsColor;
                break;
            case LocationType.DUNES:
                meshRenderer.material.color = dunesColor;
                break;
            case LocationType.SEA:
                meshRenderer.material.color = seaColor;
                break;
            case LocationType.OCEAN:
                meshRenderer.material.color = oceanColor;
                break;
            case LocationType.RIVER:
                meshRenderer.material.color = riverColor;
                break;
            case LocationType.LAKE:
                meshRenderer.material.color = lakeColor;
                break;
            case LocationType.HOLY:
                meshRenderer.material.color = holyColor;
                break;
            case LocationType.DARK:
                meshRenderer.material.color = darkColor;
                break;
            case LocationType.PARK:
                meshRenderer.material.color = parkColor;
                break;
            case LocationType.MISC:
                meshRenderer.material.color = miscColor;
                break;
        }
    }
}

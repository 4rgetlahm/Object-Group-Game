using GameLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSetter : MonoBehaviour
{

    [SerializeField]
    private Material forestMaterial;
    [SerializeField]
    private Material plainsMaterial;
    [SerializeField]
    private Material dunesMaterial;
    [SerializeField]
    private Material seaMaterial;
    [SerializeField]
    private Material oceanMaterial;
    [SerializeField]
    private Material riverMaterial;
    [SerializeField]
    private Material lakeMaterial;
    [SerializeField]
    private Material holyMaterial;
    [SerializeField]
    private Material darkMaterial;
    [SerializeField]
    private Material parkMaterial;
    [SerializeField]
    private Material miscMaterial;

    [SerializeField]
    private MeshRenderer materializedObject;

    public void Set(LocationType locationType)
    {
        switch (locationType){
            case LocationType.FOREST:
                materializedObject.material = forestMaterial;
                break;
            case LocationType.PLAINS:
                materializedObject.material = plainsMaterial;
                break;
            case LocationType.DUNES:
                materializedObject.material = dunesMaterial;
                break;
            case LocationType.SEA:
                materializedObject.material = seaMaterial;
                break;
            case LocationType.OCEAN:
                materializedObject.material = oceanMaterial;
                break;
            case LocationType.RIVER:
                materializedObject.material = riverMaterial;
                break;
            case LocationType.LAKE:
                materializedObject.material = lakeMaterial;
                break;
            case LocationType.HOLY:
                materializedObject.material = holyMaterial;
                break;
            case LocationType.DARK:
                materializedObject.material = darkMaterial;
                break;
            case LocationType.PARK:
                materializedObject.material = parkMaterial;
                break;
            case LocationType.MISC:
                materializedObject.material = miscMaterial;
                break;
        }
    }
}

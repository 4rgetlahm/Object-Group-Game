using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusRenderer : MonoBehaviour
{
    private int radius;
    [SerializeField]
    private Transform radiusObjectTransform;
    [SerializeField]
    private float scaleModifier;


    public void SetRadius(int radius)
    {
        this.radius = radius;
        UpdateRadius();
    }

    public void UpdateRadius()
    {
        radiusObjectTransform.localScale = new Vector3(radius * scaleModifier * radius, 0.0000000001f, radius * scaleModifier * radius);
    }
}

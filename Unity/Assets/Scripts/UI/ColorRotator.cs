using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorRotator : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Image image;
    void Start()
    {
        image = this.GetComponent<Image>();
    }

    void Update()
    {
        var t = (Mathf.Sin(Time.time * speed + 1) / 2);
        image.color = Color.Lerp(Color.white, Color.yellow, t);
    }
}

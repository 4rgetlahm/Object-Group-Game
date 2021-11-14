using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelSetter : MonoBehaviour
{
    [SerializeField]
    private TextMesh text;

    public void Set(string newText)
    {
        text.text = newText;
    }
}

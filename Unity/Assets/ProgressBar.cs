using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    public double maximum;
    public double current;
    public Image mask;

    void Start()
    {

    }

   void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        current = LocalPlayer.Instance.Health;
        float fillAmount = (float)current / (float)maximum;
        mask.fillAmount = fillAmount;
    }

}
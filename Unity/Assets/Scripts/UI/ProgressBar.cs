using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    public double maximum;
    public double current;
    public Image mask;

    [SerializeField] TextMeshProUGUI m_Object;

    void Start()
    {

    }

   void Update()
    {
        GetCurrentFill();

        m_Object.text = current + " / " + maximum;
    }

    void GetCurrentFill()
    {
        current = LocalPlayer.Instance.Health;
        float fillAmount = (float)current / (float)maximum;
        mask.fillAmount = fillAmount;
    }

}

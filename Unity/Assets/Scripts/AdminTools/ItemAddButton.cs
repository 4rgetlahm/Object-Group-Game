using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameLibrary;
using System;

public class ItemAddButton : MonoBehaviour
{
    TMP_InputField nameText;
    TMP_InputField strengthText;
    TMP_InputField dexterityText;
    TMP_InputField intelligenceText;

    // Start is called before the first frame update
    void Start()
    {
        nameText = GameObject.Find("ItemNameInput").GetComponent<TMP_InputField>();
        strengthText = GameObject.Find("StrengthInput").GetComponent<TMP_InputField>();
        dexterityText = GameObject.Find("DexterityInput").GetComponent<TMP_InputField>();
        intelligenceText = GameObject.Find("IntelligenceInput").GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        string name = nameText.text;
        int strength = int.Parse(strengthText.text);
        int dexterity = int.Parse(dexterityText.text);
        int intelligence = int.Parse(intelligenceText.text);

        //new Item(name, strength, dexterity, intelligence);
        // insert into database
    }
}

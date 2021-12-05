using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField]
    private List<CharacterSelection> relatedSelections;
    [SerializeField]
    private Image selectionImage;
    [SerializeField]
    public int value;

    public bool selected = false;

    public void SetSelected(bool selected)
    {
        if(selected == false)
        {
            selectionImage.gameObject.SetActive(false);
            this.selected = false;
            return;
        }
        foreach(CharacterSelection relatedSelection in relatedSelections)
        {
            if (relatedSelection != this)
            {
                relatedSelection.selected = false;
                relatedSelection.SetSelected(false);
            }
        }
        this.selected = true;
        selectionImage.gameObject.SetActive(true);
        Debug.Log(this + " " + this.selected);
    }

    public List<CharacterSelection> GetRelatedSelections()
    {
        return this.relatedSelections;
    }

    public void OnClick()
    {
        Debug.Log("Click " + this.gameObject);
        this.SetSelected(true);
    }
}

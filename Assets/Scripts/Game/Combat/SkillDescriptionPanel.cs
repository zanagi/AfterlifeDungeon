using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDescriptionPanel : MonoBehaviour
{
    public Text descriptionText;

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void SetDescription(string description)
    {
        descriptionText.text = description;
    }
}

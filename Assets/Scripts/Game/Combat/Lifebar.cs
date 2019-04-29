using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lifebar : MonoBehaviour
{
    public Text healthText;
    public Image bar;
    private Stats stats;
    
    public void SetStats(Stats stats)
    {
        this.stats = stats;
    }

    // Update is called once per frame
    void Update()
    {
        if(stats == null)
        {
            return;
        }
        bar.fillAmount = stats.HealthRatio;
        healthText.text = stats.hp.ToString();
    }
}

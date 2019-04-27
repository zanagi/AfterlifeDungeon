using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text;
using System.Collections.Generic;

public static class Static
{
    public static void SetAlpha(this MaskableGraphic graphics, float alpha)
    {
        Color color = graphics.color;
        color.a = alpha;
        graphics.color = color;
    }

    public static void SetAlpha(this Shadow shadow, float alpha)
    {
        Color color = shadow.effectColor;
        color.a = alpha;
        shadow.effectColor = color;
    }
}

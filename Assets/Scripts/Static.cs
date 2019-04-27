using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text;
using System.Collections.Generic;

public static class Static
{
    public static readonly string horizontalAxis = "Horizontal", verticalAxis = "Vertical";

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

    public static void Shuffle<T>(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int idx = Random.Range(i, array.Length);

            //swap elements
            T tmp = array[i];
            array[i] = array[idx];
            array[idx] = tmp;
        }
    }

    public static T GetRandom<T>(this T[] array)
    {
        if (array.Length == 0)
        {
            Debug.Log("Warning! Getting random value from empty array!");
            return default(T);
        }
        return array[Random.Range(0, array.Length)];
    }

    public static bool IsFloor(this Tile tile)
    {
        return tile == Tile.Floor || tile == Tile.Corridor;
    }
}

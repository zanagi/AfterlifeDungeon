using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Room
{
    public int x, y, width, height;

    public Room(int x, int y, int width, int height)
    {
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
    }

    public bool Contains(int x, int y)
    {
        return x >= this.x && x <= this.x + width
            && y >= this.y && y <= this.y + height;
    }

    public bool Overlaps(Room other)
    {
        return Contains(other.x, other.y) || Contains(other.x + width, other.y)
            || Contains(other.x, other.y + height) || Contains(other.x + width, other.y + height);
    }

    public int RandomX
    {
        get { return x + Random.Range(0, width + 1); }
    }

    public int RandomY
    {
        get { return y + Random.Range(0, height + 1); }
    }
}
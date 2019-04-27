﻿using System;

[Serializable]
public class IntRange
{
    public int m_Min;
    public int m_Max;

    // Constructor to set the values.
    public IntRange(int min, int max)
    {
        m_Min = min;
        m_Max = max;
    }

    // Get a random value from the range.
    public int Random
    {
        get { return UnityEngine.Random.Range(m_Min, m_Max); }
    }
}
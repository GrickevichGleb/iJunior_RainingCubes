using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilsRandom : MonoBehaviour
{
    private static System.Random s_random = new System.Random();

    public static bool TryChance(float chance)
    {
        if (s_random.NextDouble() <= chance)
            return true;
        
        return false;
    }

    public static int GetRandomNumber(int min, int max)
    {
        return s_random.Next(min, max + 1);
    }

    public static Color GetRandomColor()
    {
        float r = Convert.ToSingle(s_random.NextDouble());
        float g = Convert.ToSingle(s_random.NextDouble());
        float b = Convert.ToSingle(s_random.NextDouble());
        
        return new Color(r, g, b);
    }
}

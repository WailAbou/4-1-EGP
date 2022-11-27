using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static T PickRandom<T>(this List<T> values, bool unique = false)
    {
        int i = Random.Range(0, values.Count);
        T picked = values[i];
        if (unique) values.Remove(picked);
        return picked;
    }

    public static void FillRandom(this List<Color> colors, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var color = new Color(Random.Range(0.25f, 1.0f), Random.Range(0.25f, 1.0f), Random.Range(0.25f, 1.0f));
            colors.Add(color);
        }
    }
}

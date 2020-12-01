using System.Collections.Generic;

public static class Extensions
{
    /// <summary>
    /// Returns a random item from an array.
    /// This will crash if the array is empty or null.
    /// </summary>
    public static T GetRandom<T>(this T[] items)
    {
        return items[UnityEngine.Random.Range(0, items.Length)]; // Note: Unity Random.Range is exclusive on the second parameter.
    }

    /// <summary>
    /// Returns a random item from a list.
    /// This will crash if the list is empty or null.
    public static T GetRandom<T>(this List<T> items)
    {
        return items[UnityEngine.Random.Range(0, items.Count)]; // Note: Unity Random.Range is exclusive on the second parameter.
    }
}

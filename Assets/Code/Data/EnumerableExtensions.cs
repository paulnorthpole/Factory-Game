using System;
using System.Collections.Generic;

public static class EnumerableExtensions {
    public static T GetFirst<T>(this IEnumerable<T> list)  {
        foreach(var item in list) {
            return item;
        }
        return default(T);
    }
    public static T GetAt<T>(this IEnumerable<T> list, int index, T defaultValue = default(T))  {
        foreach(var item in list) {
            if (0 == index--)
                return item;
        }
        return defaultValue;
    }
    public static int GetCount<T>(this IEnumerable<T> list)  {
        var count = 0;
        foreach(var item in list) {
            count++;
        }
        return count;
    }
    public static bool IsEmpty<T>(this IEnumerable<T> list)  {
        foreach(var item in list) {
            return false;
        }
        return true;
    }
}


using System;

public static class DateExtensions {
    public static bool HasBeen(this DateTime date, int milliseconds) {
        return DateTime.Now - date > TimeSpan.FromMilliseconds(milliseconds);
    }
}
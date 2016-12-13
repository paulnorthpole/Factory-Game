using System;

public static class Assert {
    [System.Diagnostics.Conditional("DEBUG")]
    public static void IsTrue(bool result) {
        if(!result) {
            UnityEngine.Debug.Assert(false);
        }
    }
}


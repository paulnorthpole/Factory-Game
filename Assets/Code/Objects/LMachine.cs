using UnityEngine;

public class LMachine : Machine
{
    protected override Vector3[] CreateBlocks ()
    {
        return new Vector3[] {
            new Vector3(0f, 0f, 1f),
            new Vector3(1f, 0f, 1f),
            new Vector3(1f, 0f, 0f)
        };
    }
    protected override EnumDirection[] inputDirections {
        get {
            return new[] { EnumDirection.West };
        }
    }
    protected override EnumDirection[] outputDirections {
        get {
            return new[] { EnumDirection.South };
        }
    }
}
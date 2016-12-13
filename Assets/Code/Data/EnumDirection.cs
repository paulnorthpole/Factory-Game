using UnityEngine;

public enum EnumDirection {
    North, East, South, West
}

public static class EnumDirectionExtensions {
    public static EnumDirection GetLeftOf(this EnumDirection from) {
        switch(from) {
        case EnumDirection.North:
        default:
            return EnumDirection.West;
        case EnumDirection.East:
            return EnumDirection.North;
        case EnumDirection.South:
            return EnumDirection.East;
        case EnumDirection.West:
            return EnumDirection.South;
        }
    }
    public static EnumDirection GetRightOf(this EnumDirection from) {
        switch(from) {
        case EnumDirection.North:
        default:
            return EnumDirection.East;
        case EnumDirection.East:
            return EnumDirection.South;
        case EnumDirection.South:
            return EnumDirection.West;
        case EnumDirection.West:
            return EnumDirection.North;
        }
    }
    public static EnumDirection GetOppositeOf(this EnumDirection from)
    {
        switch (from) {
            case EnumDirection.North:
            default:
                return EnumDirection.South;
            case EnumDirection.East:
                return EnumDirection.West;
            case EnumDirection.South:
                return EnumDirection.North;
            case EnumDirection.West:
                return EnumDirection.East;
        }
    }
    public static Vector3 GetVector(this EnumDirection direction)
    {
        switch (direction) {
            case EnumDirection.North:
                return Vector3.forward;
            case EnumDirection.East:
                return Vector3.right;
            case EnumDirection.South:
                return Vector3.back;
            case EnumDirection.West:
                return Vector3.left;
            default:
                return Vector3.zero;
        }
    }
    public static float GetAngle (this EnumDirection direction)
    {
        switch (direction) {
            default:
            case EnumDirection.North:
                return 0f;
            case EnumDirection.East:
                return 90f;
            case EnumDirection.South:
                return 180f;
            case EnumDirection.West:
                return 270f;
        }
    }
}


using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public struct Location {
    public int x, y;
    public Location(int x, int y) {
        this.x = x;
        this.y = y;
    }
    public Location(Vector3 position, bool isRelative = false) {
        this.x = Mathf.RoundToInt(position.x);
        this.y = Mathf.RoundToInt(position.z);
        if(!isRelative) {
            this.x += (int)Grid.gridWidth / 2;
            this.y += (int)Grid.gridLength / 2;
        }
    }
    public static bool operator ==(Location a, Location b) {
        return a.x == b.x && a.y == b.y;
    }
    public static bool operator !=(Location a, Location b) {
        return !(a == b);
    }
    public Vector3 position {
        get {
            return new Vector3(x - (int)Grid.gridWidth / 2, 0, y - (int)Grid.gridLength / 2);
        }
    }
    public static Location operator +(Location a, Location b) {
        return new Location(a.x + b.x, a.y + b.y);
    }
}

public static class LocationExtensions {

    public static bool Contains(this IEnumerable<Location> list, Location item) {
        foreach(var itemInList in list) {
            if(itemInList == item) {
                return true;
            }
        }
        return false;
    }

    public static EnumDirection Direction(this Location from, Location to) {
        if(from.x == to.x) {
            if(from.y == to.y + 1) {
                return EnumDirection.North;
            } else {
                return EnumDirection.South;
            }
        } else {
            if(from.x == to.x + 1) {
                return EnumDirection.East;
            } else {
                return EnumDirection.West;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;


public class Grid : MonoBehaviour {
    #region Data
    public static GameObject onEffect;
    public GameObject machineOnEffect;
    public const uint gridWidth = 52, gridLength = 52;
    public static AbstractPlacedItem[,] items = new AbstractPlacedItem[gridWidth, gridLength];
    protected void Start() {
        onEffect = machineOnEffect;
    }
    public static AbstractPlacedItem GetItem(Vector3 position) {
        int x, z;
        if(GetCoords(position, out x, out z)) {
            return GetItem(x, z);
        }
        return null;
    }
    public static AbstractPlacedItem GetItem(Location location) {
        return GetItem(location.x, location.y);
    }

    public static AbstractPlacedItem GetItem(int x, int z) {
        return items[x, z];
    }

    public static bool GetCoords(Vector3 position, out int x, out int z) {
        x = Mathf.RoundToInt(position.x) + (int)gridWidth / 2;
        z = Mathf.RoundToInt(position.z) + (int)gridLength / 2;
        if(x < 0 || x > gridWidth || z < 0 || z > gridLength) {
            MonoBehaviour.print("Out of bounds");
            return false; // out of bounds
        }
        return true;
    }
    #endregion

    #region Public API
    // GetSurrounding is no longer used, because we know about our input and output points now (using the ports).
    public static IEnumerable<Type> GetSurrounding<Type>(Vector3 position, bool includeCenterSquare = false)
        where Type : AbstractPlacedItem
    {
        int x, z;
        var surroundings = new HashSet<Type>();

        if (GetCoords(position, out x, out z)) {
            var item = GetItem(x, z);
            if (item != null) {
                surroundings.UnionWith(GetSurrounding<Type>(item, includeCenterSquare));
            }
        }

        return surroundings;
    }

    public static IEnumerable<Type> GetSurrounding<Type>(AbstractPlacedItem item, bool includeCenterSquare = false)
        where Type : AbstractPlacedItem
    {
        var surroundings = new HashSet<Type>();

        int x, z;
        if (GetCoords(item.transform.position, out x, out z)) {
            if (includeCenterSquare && GetItem(x, z) == item && item is Type) {
                surroundings.Add((Type) item);
            }

            var blocks = item.rotatedBlocks;
            for (int iBlock = 0; iBlock < blocks.Length; ++iBlock) {
                int blockX = x + Mathf.RoundToInt(blocks[iBlock].x);
                int blockZ = z + Mathf.RoundToInt(blocks[iBlock].z);

                for (int checkX = blockX - 1; checkX <= blockX + 1; checkX++) {
                    for (int checkZ = blockZ - 1; checkZ <= blockZ + 1; checkZ++) {
                        if (checkX != blockX ^ checkZ != blockZ) {
                            var checkItem = GetItem(checkX, checkZ);
                            if (checkItem != null && checkItem != item && checkItem is Type) {
                                surroundings.Add((Type) checkItem);
                            }
                        }
                    }
                }
            }
        }

        return surroundings;
    }

    public static IEnumerable<Type> GetAbstractPlacedItems<Type>() where Type : AbstractPlacedItem {
        foreach (var item in items) {
            var castItem = item as Type;
            if (castItem != null) {
                yield return castItem;
            }
        }
    }   

    public static bool PlaceItem (AbstractPlacedItem itemToPlace)
    {
        int x, z;
        if (GetCoords(itemToPlace.transform.position, out x, out z)) {
            var blocks = itemToPlace.rotatedBlocks;
            // first, check if all slots are empty
            for (int i = 0; i < blocks.Length; ++i) {
                int blockX = x + Mathf.RoundToInt(blocks[i].x);
                int blockZ = z + Mathf.RoundToInt(blocks[i].z);
                if (items[blockX, blockZ] != null) {
                    Assert.IsTrue(false);
                    return false;
                }
            }
            // then set them to this item, if we are still here.
            for (int i = 0; i < blocks.Length; ++i) {
                int blockX = x + Mathf.RoundToInt(blocks[i].x);
                int blockZ = z + Mathf.RoundToInt(blocks[i].z);
                items[blockX, blockZ] = itemToPlace;
            }

            // MonoBehaviour.print("Grid added " + itemToPlace + " @ " + x + ", " + z + " + " + String.Join("; ", System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Select(blocks, b => b.ToString()))));

            //Network.AddToWorld(itemToPlace); // establish port connections

            foreach(var item in Grid.GetSurrounding<AbstractPlacedItem>(itemToPlace)) {
                item.OnConnectionChange();
            }
            return true;
        }
        return false;
    }


    public static bool RemoveItem(AbstractPlacedItem itemToRemove) {
        int x, z;
        if (GetCoords(itemToRemove.transform.position, out x, out z)) {
            var blocks = itemToRemove.rotatedBlocks;
            // first, check if all slots are actually belonging to that item,
            for (int i = 0; i < blocks.Length; ++i) {
                int blockX = x + Mathf.RoundToInt(blocks[i].x);
                int blockZ = z + Mathf.RoundToInt(blocks[i].z);
                if (items[blockX, blockZ] != itemToRemove) {
                    Assert.IsTrue(false);
                    return false;
                }
            }

            // then clear them, if we are still here.
            for (int i = 0; i < blocks.Length; ++i) {
                int blockX = x + Mathf.RoundToInt(blocks[i].x);
                int blockZ = z + Mathf.RoundToInt(blocks[i].z);
                items[blockX, blockZ] = null;
            }
            MoneyManager.Refund(itemToRemove);
            for(int i = 0; i < itemToRemove.products.Count; i++) {
                GameObject.Destroy(itemToRemove.products[i].gameObject);
            }
            itemToRemove.products.Clear();
            //Network.RemoveFromWorld(itemToRemove); // reset port connections.
            // MonoBehaviour.print("Grid added " + itemToPlace + " @ " + x + ", " + z);
            return true;
        }
        return false;
    }

    // this function is no longer used, because we know about shapes now and do not rely on directions that much.
    public static EnumDirection GetDirectionTo(AbstractPlacedItem origin, AbstractPlacedItem destination) {
        if(Mathf.RoundToInt(origin.transform.position.x) ==
            Mathf.RoundToInt(destination.transform.position.x)) {
            if(origin.transform.position.z < destination.transform.position.z) {
                return EnumDirection.North;
            } else {
                return EnumDirection.South;
            }
        } else {
            if(origin.transform.position.x < destination.transform.position.x) {
                return EnumDirection.East;
            } else {
                return EnumDirection.West;
            }
        }
    }
    #endregion
}


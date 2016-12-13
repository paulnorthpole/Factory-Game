using System;
using UnityEngine;
using System.Collections.Generic;

public abstract class AbstractPlacedItem : MonoBehaviour {
    public int connectedInputCount, connectedOutputCount;
    public AbstractPlacedItem firstConnectedInput, firstConnectedOutput;
    


    #region Data / Init
    [NonSerialized]
    protected List<Product> _products = new List<Product>();
    public List<Product> products {
        get {
            return _products;
        }
    }
    public float cost;
    protected abstract EnumDirection[] inputDirections {
        get;
    }
    protected abstract EnumDirection[] outputDirections {
        get;
    }
    public bool IsPlaced {
        get {
            return Grid.GetItem(location) == this;
        }
    }
    public EnumDirection forward {
        get {
            var rotation = (transform.rotation * outputDirections[0].GetVector());

            if(Mathf.RoundToInt(rotation.z) < 0) {
                return EnumDirection.South;
            }
            if(Mathf.RoundToInt(rotation.z) > 0) {
                return EnumDirection.North;
            }
            if(Mathf.RoundToInt(rotation.x) < 0) {
                return EnumDirection.West;
            }
            if(Mathf.RoundToInt(rotation.x) > 0) {
                return EnumDirection.East;
            }
            throw new Exception();
        }
    }

    public IEnumerable<Location> inputLocations {
        get {
            for(int i = 0; i < inputDirections.Length; i++) {
                yield return new Location(transform.rotation * (inputDirections[i].GetVector()), true) + location;
            }
        }
    }
    public IEnumerable<Location> outputLocations {
        get {
            for(int i = 0; i < outputDirections.Length; i++) {
                yield return new Location(transform.rotation * (outputDirections[i].GetVector()), true) + location;
            }
        }
    }
    public Location location {
        get {
            return new Location(transform.position);
        }
    }
    #endregion

    #region API
    public virtual int AddProduct(Product p)
    {
        _products.Add(p);
        return 0;
    }
    public virtual void RemoveProduct(Product p)
    {
        _products.Remove(p);
    }
    #endregion

    #region Multiblock
    private Vector3[] _blocks = null;
    // cache for rotated blocks. blocks are needed often, so it is a good idea to not calculate them every time.
    private Quaternion rotatedBlocksRotation = Quaternion.identity;
    private Vector3[] _rotatedBlocks = null;

    public IEnumerable<Vector3> blocks {
        get {
            if(_blocks == null) {
                _blocks = CreateBlocks();
            }
            return _blocks;
        }
    }

    // rotated blocks changes very infrequently, so let's cache them
    public Vector3[] rotatedBlocks {
        get {
            if(_rotatedBlocks == null || transform.rotation != rotatedBlocksRotation) {
                if(_blocks == null) {
                    _blocks = CreateBlocks();
                }

                _rotatedBlocks = new Vector3[_blocks.Length];
                for(int i = 0; i < _blocks.Length; ++i) {
                    _rotatedBlocks[i] = transform.rotation * _blocks[i];
                }

                rotatedBlocksRotation = transform.rotation;
            }
            return _rotatedBlocks;
        }
    }
    #endregion

    #region Internal API

    // these functions are mandatory to override by new AbstractPlacedItems.
    // you should not call these functions yourself, but instead use the properties, that cache the result of them.

    // return the Shape of the Item.
    // This function uses vectors as a simple return type, the code actually expects an "integer vector".
    // (0|0) is the position the mouse pointer hit last.
    protected abstract Vector3[] CreateBlocks();

    // this is called at least once after connectedPorts of any of the inputs or outputs got changed.
    // there is no guarentee on the frequency of these calls.
    public virtual void OnConnectionChange() {
        connectedInputCount = connectedInputs.GetCount();
        firstConnectedInput = connectedInputs.GetFirst();
        firstConnectedOutput = connectedOutputs.GetFirst();
        connectedOutputCount = connectedOutputs.GetCount();
    }
   
    #endregion

    public int gridX {
        get {
            int x, y;
            Grid.GetCoords(transform.position, out x, out y);
            return x;
        }
    }

    public int gridZ {
        get {
            int x, y;
            Grid.GetCoords(transform.position, out x, out y);
            return y;
        }
    }

    public IEnumerable<AbstractPlacedItem> connectedInputs {
        get {
            foreach(var surroundingPlacedItem in Grid.GetSurrounding<AbstractPlacedItem>(this)) {
                foreach(var surroundingPlaceItemOutput in surroundingPlacedItem.outputLocations) {
                    if(surroundingPlaceItemOutput == location
                        && inputLocations.Contains(surroundingPlacedItem.location)) {
                        yield return surroundingPlacedItem;
                    }
                }
            }
        }
    }
    public IEnumerable<AbstractPlacedItem> connectedOutputs {
        get {
            foreach(var surroundingPlacedItem in Grid.GetSurrounding<AbstractPlacedItem>(this)) {
                foreach(var surroundingPlaceItemInput in surroundingPlacedItem.inputLocations) {
                    if(surroundingPlaceItemInput == location
                        && outputLocations.Contains(surroundingPlacedItem.location)) {
                        yield return surroundingPlacedItem;
                    }
                }
            }
        }
    }

    public bool hasInputConnected {
        get {
            foreach(var input in connectedInputs) {
                return true;
            }
            return false;
        }
    }


    #region Event
    public virtual void OnClick() {
        Grid.RemoveItem(this);
        FloorPlacementController.thingToPlace = this;
    }
    #endregion
}


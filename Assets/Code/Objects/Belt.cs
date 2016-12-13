using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : AbstractPlacedItem {
    #region Data / Init

    public GameObject graphicsContainer;

    // just a one-block thing.
    protected override Vector3[] CreateBlocks() {
        return new Vector3[] { Vector3.zero };
    }

    protected override EnumDirection[] inputDirections {
        get {
            return new[] { EnumDirection.North, EnumDirection.West, EnumDirection.East };
        }
    }
    protected override EnumDirection[] outputDirections {
        get {
            return new[] { EnumDirection.South };
        }
    }
    
    // if there are more than 1 connections, its a merge.
    // if there is a full (input+output) connection, check for curves,
    // it's straight otherwise.
    string art {
        get {
            if(connectedInputCount > 1) {
                return "Merge";
            }

            if(connectedInputCount > 0) {
                var dir = firstConnectedInput.location.Direction(location);
                if(dir.GetOppositeOf() != forward) {
                    return dir.GetRightOf() == forward ? "Left" : "Right";
                }
            }

            return "Straight";
        }
    }

    protected void OnEnable() {
        graphicsContainer = transform.FindChild("Graphics").gameObject;
    }

    //protected void Update() {
    //    OnConnectionChange();
    //}
    #endregion

    public override void OnConnectionChange() {
        base.OnConnectionChange();
        var shouldShowShort = false;
        var shouldShowShortLeft = false;
        var shouldShowShortRight = false;

        var inputDirection = inputDirections[0];
        var outputDirection = inputDirection.GetOppositeOf();
        if(connectedInputCount > 0) {
            inputDirection = firstConnectedInput.location.Direction(location);
        }
 if(!connectedOutputs.IsEmpty()) {
            //outputDirection = connectedOutputs.GetFirst().location.Direction(location);
            //var outputBelt = connectedOutputs.GetFirst() as Belt;
            //if(outputBelt != null && outputBelt.art == "Merge") {
            //    if(outputDirection != connectedOutputs.GetFirst().forward) {
            //        shouldShowShort = true;
            //    }
            //}
            //if(art == "Curve") {
            //    // lot's of trial and error got me there.
            //    if(inputDirection.GetRightOf() == forward) {
            //        //graphicsContainer.transform.localRotation 
            //        //    = Quaternion.Euler(-90f, inputDirection.GetAngle() + 90f, 90f);
            //    } else {
            //        //graphicsContainer.transform.localRotation 
            //        //    = Quaternion.Euler(-90f, inputDirection.GetAngle() - 180, -90);
            //    }
            //} 
            //else {
            //    // flipping in straight line situations
            //    if(output != null) {
            //        graphicsContainer.transform.localRotation = Quaternion.Euler(0f, output.direction.GetAngle(), 0f);
            //    } else if(input != null) {
            //        graphicsContainer.transform.localRotation = Quaternion.Euler(0f, inputDirection.GetAngle() + 180f, 0f);
            //    } else {
            //        // reset. this is important if we remove the item from the world.
            //        graphicsContainer.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
            //    }
            //}
        }
        if (art == "Merge") {
            foreach (var input in connectedInputs) {
                var dir = location.Direction(input.location);
                if (dir.GetLeftOf() == forward) {
                    shouldShowShortLeft = true;
                }
                else if (dir.GetRightOf() == forward) {
                    shouldShowShortRight = true;
                }
            }
        }

        for (int i = 0; i < graphicsContainer.transform.childCount; i++) {
            var child = graphicsContainer.transform.GetChild(i);
            switch(child.name) {
                case "Sphere": break;
                case "Canvas": break;
                case "Short": child.gameObject.SetActive(shouldShowShort); break;
                case "ShortLeft": child.gameObject.SetActive(shouldShowShortLeft); break;
                case "ShortRight": child.gameObject.SetActive(shouldShowShortRight); break;
                default: child.gameObject.SetActive(child.name == art); break;
            }
        }

    }
}


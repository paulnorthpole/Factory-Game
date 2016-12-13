//using System;
//using UnityEngine;
//using System.Collections.Generic;

//public static class Network {

//    /*
//     * Instead of CanConnectTo, we now have explicit knowledge of all possible connectivity points,
//     * so instead of looking at all possible neighbours, we can just iterate over the inputs and outputs properties
//     * to figure out what to do.
//     * These functions also set both sides of the connection directly, and do not use some property magic.
//     * 
//     */

//    public static void AddToWorld(AbstractPlacedItem item) {
//        Grid.PlaceItem()



//        //int ownX, ownZ;
//        //Assert.IsTrue(Grid.GetCoords(item.transform.position, out ownX, out ownZ));

//        //foreach(var input in item.inputs) {
//        //    // inputX/inputY is the neighbouring cell (that we would have gotten eventually using GetSurrounding).
//        //    var rotatedInput = item.transform.rotation * (input.portCenter + input.direction.GetVector());
//        //    int inputX = ownX + Mathf.RoundToInt(rotatedInput.x);
//        //    int inputZ = ownZ + Mathf.RoundToInt(rotatedInput.z);

//        //    AbstractPlacedItem checkItem = Grid.GetItem(inputX, inputZ);
//        //    if (checkItem != null && checkItem != item) {
//        //        int itemX, itemZ;
//        //        Assert.IsTrue(Grid.GetCoords(checkItem.transform.position, out itemX, out itemZ));

//        //        for (int iOut = 0; iOut < checkItem.outputs.Length; ++iOut) {
//        //            if (checkItem.outputs[iOut].output == null) {
//        //                // try to find a corresponding output that points in our direction.
//        //                var rotatedOutput = checkItem.transform.rotation * (checkItem.outputs[iOut].portCenter + checkItem.outputs[iOut].direction.GetVector());
//        //                int outputX = itemX + Mathf.RoundToInt(rotatedOutput.x);
//        //                int outputZ = itemZ + Mathf.RoundToInt(rotatedOutput.z);

//        //                // we can connect this input / output pair if the the outputting block is next to the search block and in the right direction
//        //                // (+ some overridable conditions)
//        //                if (Grid.GetItem(outputX, outputZ) == item /* && input.direction != checkItem.outputs[iOut].direction */
//        //                    && item.CanConnectInput(input, checkItem.outputs[iOut]) && checkItem.CanConnectOutput(checkItem.outputs[iOut], input)) {
//        //                    input.output = checkItem.outputs[iOut];
//        //                    checkItem.outputs[iOut].output = input;
//        //                    checkItem.OnConnectionChange();
//        //                }
//        //            }
//        //        }
//        //    }
//        //}

//        // do the same thing just with input/output reversed.
//        // inputs get checked first, because you usually are expected to build a string of things from input to output.
//        //for (int iOut = 0; iOut < item.connectedOutputs.Length; ++iOut) {
//        //    // check the block where the output searches for things.
//        //    var rotatedOutput = item.transform.rotation * (item.connectedOutputs[iOut].portCenter + item.connectedOutputs[iOut].direction.GetVector());
//        //    int outputX = ownX + Mathf.RoundToInt(rotatedOutput.x);
//        //    int outputZ = ownZ + Mathf.RoundToInt(rotatedOutput.z);

//        //    AbstractPlacedItem checkItem = Grid.GetItem(outputX, outputZ);
//        //    if (checkItem != null && checkItem != item) {
//        //        // check if a output is connected to this item. this prevents connecting to the same item as input as well as output
//        //        bool alreadyConnected = false;
//        //        //for (int iIn = 0; iIn < item.inputs.Length; ++iIn) {
//        //        //    if (input.output != null && input.output.me == checkItem) {
//        //        //        alreadyConnected = true;
//        //        //        break;
//        //        //    }
//        //        //}

//        //        if (!alreadyConnected) {
//        //            int itemX, itemZ;
//        //            Assert.IsTrue(Grid.GetCoords(checkItem.transform.position, out itemX, out itemZ));

//        //            for (int iIn = 0; iIn < checkItem.connectedInputs.Length; ++iIn) {
//        //                if (checkinput.output == null) {
//        //                    var rotatedInput = checkItem.transform.rotation * (checkinput.portCenter + checkinput.direction.GetVector());
//        //                    int inputX = itemX + Mathf.RoundToInt(rotatedInput.x);
//        //                    int inputZ = itemZ + Mathf.RoundToInt(rotatedInput.z);

//        //                    // we can connect this input and this output if the the outputting block is next to the search block, in the right direction.
//        //                    if (Grid.GetItem(inputX, inputZ) == item /* && item.outputs[iOut].direction != checkinput.direction */
//        //                        && item.CanConnectOutput(item.connectedOutputs[iOut], checkinput) && checkItem.CanConnectInput(checkinput, item.connectedOutputs[iOut])) {
//        //                        item.connectedOutputs[iOut].output = checkinput;
//        //                        checkinput.output = item.connectedOutputs[iOut];
//        //                        checkItem.OnConnectionChange();
//        //                    }
//        //                }
//        //            }
//        //        }
//        //    }
//        //}

//        item.OnConnectionChange();
//    }

//    public static void RemoveFromWorld(AbstractPlacedItem item) {
//        MoneyManager.Refund(item);
//        // TODO

//        //for (int i = 0; i < item.products.Count; i++) {
//        //    GameObject.Destroy(item.products[i].gameObject);
//        //}
//        //item.products.Clear();

//        //// loop through all ports and clear them.
//        //for (int i = 0; i < item.connectedInputs.Length; ++i) {
//        //    if (item.connectedInputs[i].output != null) {
//        //        var connectedItem = item.connectedInputs[i].output.me;
//        //        item.connectedInputs[i].output.output = null;
//        //        item.connectedInputs[i].output = null;
//        //        connectedItem.OnConnectionChange();
//        //    }
//        //}
//        //for (int i = 0; i < item.connectedOutputs.Length; ++i) {
//        //    if (item.connectedOutputs[i].output != null) {
//        //        var connectedItem = item.connectedOutputs[i].output.me;
//        //        item.connectedOutputs[i].output.output = null;
//        //        item.connectedOutputs[i].output = null;
//        //        connectedItem.OnConnectionChange();
//        //    }
//        //}

//        //item.OnConnectionChange();
//    }
//}

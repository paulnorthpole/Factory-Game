using UnityEngine;
using System.Collections.Generic;

public class Splitter : AbstractPlacedItem {
    #region Data / Init

    protected int[] productsCountPerOutput = new int[] { 0, 0 };
    protected Product[] lastProductPerOutput = new Product[] { null, null };
    bool isAOrB;

    public GameObject graphicsContainer;

    // just a one-block thing.
    protected override Vector3[] CreateBlocks() {
        return new Vector3[] { Vector3.zero };
    }

    // input only from the south, output alternating west and east.
    protected override EnumDirection[] inputDirections {
        get {
            return new[] { EnumDirection.South };
        }
    }
    protected override EnumDirection[] outputDirections {
        get {
            return new[] { EnumDirection.West, EnumDirection.East };
        }
    }

    //protected override bool supportsMultipleOutputs
    //{
    //    get
    //    {
    //        return true;
    //    }
    //}

    protected void Start() {
        graphicsContainer = transform.FindChild("Graphics").gameObject;
    }

    #endregion

    #region API
    public override int AddProduct(Product p) {
        base.AddProduct(p);
        // Choose the output with the least products in queue
        int nbOutputs = this.connectedOutputs.GetCount();
        var bestOutput = -1;
        if(nbOutputs == 1) {
            bestOutput = 0;
        } else {
            if(lastProductPerOutput[0] != null && lastProductPerOutput[0].wasBlockedLastFrame) {
                bestOutput = 1;
            } else if(lastProductPerOutput[1] != null && lastProductPerOutput[1].wasBlockedLastFrame) {
                bestOutput = 0;
            } else {
                bestOutput = isAOrB ? 0 : 1;
                isAOrB = !isAOrB;
            }
        }
        //int bestOutput = -1;
        //if (nbOutputs > 0)
        //{
        //    for (int i = 0; i < nbOutputs; ++i)
        //    {
        //        if ((lastProductPerOutput[i] == null || !lastProductPerOutput[i].wasBlockedLastFrame) &&
        //            (bestOutput == -1 || productsCountPerOutput[i] < productsCountPerOutput[bestOutput]))
        //        {
        //            bestOutput = i;
        //        }
        //    }
        //    if (bestOutput >= 0)
        //    {
        //    }
        //}
        var aOpen = lastProductPerOutput[0] == null || lastProductPerOutput[0].currentHolder != this;
        var bOpen = lastProductPerOutput[1] == null || lastProductPerOutput[1].currentHolder != this;
        if(bestOutput >= 0) {
            if(aOpen && bOpen) {
                productsCountPerOutput[bestOutput] += 1;
                lastProductPerOutput[bestOutput] = p;
                return bestOutput;
            } else if(aOpen) {
                productsCountPerOutput[0] += 1;
                lastProductPerOutput[0] = p;
                return 0;
            } else if(bOpen) {
                productsCountPerOutput[1] += 1;
                lastProductPerOutput[1] = p;
                return 1;
            }
        }
        return -1;
    }
    public override void RemoveProduct(Product p) {
        if(p.currentHolderOutputIndex >= 0) {
            productsCountPerOutput[p.currentHolderOutputIndex] -= 1;
            if(lastProductPerOutput[p.currentHolderOutputIndex] == p) {
                lastProductPerOutput[p.currentHolderOutputIndex] = null;
            }
        }
        base.RemoveProduct(p);
    }
    #endregion
    //int currentMainOutput = 0;
    //bool needToAdvance = false;
    //void FixedUpdate()
    //{
    //    if (needToAdvance)
    //    {
    //        needToAdvance = false;
    //        print("advance splitter");

    //        List<Port> connectedOutputs = new List<Port>(connectedOutputs.Length);
    //        for (int i = 0; i < base.connectedOutputs.Length; ++i)
    //        {
    //            if (base.connectedOutputs[i].output != null)
    //            {
    //                connectedOutputs.Add(base.connectedOutputs[i]);
    //            }
    //        }

    //        if (connectedOutputs.Count > 0)
    //        {
    //            currentMainOutput = (currentMainOutput + 1) % connectedOutputs.Count;
    //        }
    //        else
    //        {
    //            currentMainOutput = -1;
    //        }
    //    }
    //}

    //public override Port firstConnectedOutput
    //{
    //    get
    //    {
    //        needToAdvance = true;
    //        return currentMainOutput >= 0 ? connectedOutputs[currentMainOutput] : null;
    //    }
    //}
}
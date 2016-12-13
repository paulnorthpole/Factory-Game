using System;
using UnityEngine;

public class Machine : AbstractPlacedItem {
    #region Data / Init
    GameObject effect;
    public InputRequirements[] inputRequirements;
    public int maxTotalStorage;
    public int ticksPerProduct = 60;
    public GameObject product;
    int ticksSinceLastProduct, productsPendingDistribution;
    Product lastProduct;
    AudioSource[] audio;
    public float percentFull {
        get {
            if(inputRequirements.Length == 0) {
                return (float)productsPendingDistribution / maxTotalStorage;
            }
            return (float)inputRequirements.TotalReceived() / maxTotalStorage;
        }
    }
    protected override Vector3[] CreateBlocks() {
        return new Vector3[] { Vector3.zero };
    }
    protected override EnumDirection[] inputDirections {
        get {
            return new[] { EnumDirection.North, EnumDirection.East, EnumDirection.West };
        }
    }
    protected override EnumDirection[] outputDirections {
        get {
            return new[] { EnumDirection.South };
        }
    }
    
    protected void Start() {
        if(inputRequirements.Length == 0) {
            productsPendingDistribution = maxTotalStorage;
        }
        audio = GetComponentsInChildren<AudioSource>();
    }
    #endregion
    public override int AddProduct(Product p) {
        base.AddProduct(p);
        return 
            maxTotalStorage < inputRequirements.TotalReceived() || !inputRequirements.HasSpaceFor(p)
            ? -1 : 0;
    }
    public void FixedUpdate() {
        var inProgress = DoFixedUpdated();
        bool? turnOn = null;
        if(inProgress && effect == null) {
            effect = Instantiate(Grid.onEffect, transform, false);
            turnOn = true;
        } else if(!inProgress && effect != null) {
            Destroy(effect.gameObject);
            turnOn = false;
        }

        if(turnOn.HasValue) {
            for(int i = 0; i < audio.Length; i++) {
                if(turnOn.Value) {
                    audio[i].Play();
                } else {
                    audio[i].Stop();
                }
            }
        }
    }

    protected virtual void PrepareOutput(Product p) {
    }

    private bool DoFixedUpdated() {
        if(!IsPlaced) {
            return false;
        }
        if(inputRequirements.Length == 0 && productsPendingDistribution == 0) {
            if(products.Count == 0) {
                Destroy(gameObject);
            }
            return false;
        }
        bool builtSomething = false;
        if(inputRequirements.Satisfied()) {
            if(connectedOutputCount > 0) {
                if(ticksSinceLastProduct == ticksPerProduct) {
                    // check collision before spawning
                    //var productPosition = transform.position + forward.GetVector();
                    if(lastProduct == null || !lastProduct.wasBlockedLastFrame) { //(lastProduct.transform.position - productPosition).magnitude > Product.productCollisionRadius) {
                        var position = transform.position;
                        position.y = product.transform.position.y;
                        lastProduct = Instantiate(this.product, position, product.transform.localRotation).GetComponent<Product>();
                        lastProduct.currentHolder = this;
                        Assert.IsTrue(lastProduct != null);
                        PrepareOutput(lastProduct);
                        products.Add(lastProduct); // add to the queue so we can also detect products backing up during Product.IsBlocked
                        inputRequirements.DeductOneProduct();
                        if(productsPendingDistribution > 0) {
                            productsPendingDistribution--;
                        }
                    }
                }
            }
            ticksSinceLastProduct++;
            if(!(this is CollectionForSale)) {
                builtSomething = true;
            }
            if(ticksSinceLastProduct > ticksPerProduct) {
                if(connectedOutputCount > 0) {
                    ticksSinceLastProduct = 0;
                } else {
                    ticksSinceLastProduct--;
                    builtSomething = false;
                }
            }
        } else {
            ticksSinceLastProduct = 0;
        }
        return builtSomething;
    }

    internal bool Consume(Product product) {
        return inputRequirements.ConsumeProduct(product);
    }
}


using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public class Product : MonoBehaviour {
    #region Data / Init
    public string typeName;
    //public const float productCollisionRadius = .5f;
    [NonSerialized]
    public AbstractPlacedItem currentHolder;
    const float speed = .01f;

    /// Index of the connectedOutput to target,
    /// if -1 the currentHolder does not have one available yet.
    public int currentHolderOutputIndex = 0;
    public bool wasBlockedLastFrame;
    //bool isBlockedThisFrame;
    List<GameObject> collisions = new List<GameObject>();

    int lastIndex = -1;
    AbstractPlacedItem lastHolder;
    Location lastTarget;

    public Location target {
        get {
            if (currentHolderOutputIndex < 0) { // we don't have an output available
                currentHolderOutputIndex++;
                return currentHolder.location;
            } else {
                if(lastIndex != currentHolderOutputIndex || lastHolder != currentHolder) {
                    lastIndex = currentHolderOutputIndex;
                    lastHolder = currentHolder;
                    lastTarget = currentHolder.connectedOutputs.GetAt(currentHolderOutputIndex, currentHolder).location;
                }
                return lastTarget;
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if(currentHolder.connectedOutputCount > 0) {
            if((transform.position - target.position).sqrMagnitude
                > (other.gameObject.transform.position - target.position).sqrMagnitude) {
                collisions.Add(other.gameObject);
            }
        }
    }
    void OnTriggerExit(Collider other) {
        collisions.Remove(other.gameObject);
    }

    private void OnDisable() {
        currentHolder.OnConnectionChange();
    }
    #endregion

    protected void FixedUpdate() {
        wasBlockedLastFrame = !collisions.IsEmpty();
        if(wasBlockedLastFrame || currentHolder.connectedOutputCount == 0) {
            return;
        }
        DoFixedUpdatedAfterCollisionStay();
    }

    void DoFixedUpdatedAfterCollisionStay() {  
        var originalLocation = new Location(transform.position);
        var targetPosition = target.position;
        targetPosition.y = transform.position.y;
        if((transform.position - targetPosition).magnitude > 1) {
            targetPosition = currentHolder.transform.position;
            targetPosition.y = transform.position.y;
        }
        var destinationPosition = Vector3.MoveTowards(transform.position, targetPosition, speed);

        if(originalLocation != new Location(destinationPosition)) {
            var targetItem = Grid.GetItem(target);
            if(targetItem != null) {
                currentHolderOutputIndex = targetItem.AddProduct(this);
                if(currentHolderOutputIndex < 0) {
                    targetItem.RemoveProduct(this);
                    return;
                } 
                currentHolder.RemoveProduct(this);
                var machine = targetItem as Machine;
                if(machine != null) {
                    if(machine.Consume(this)) {
                        Destroy(gameObject);
                    }
                    return;
                }
                currentHolder = targetItem;
            }
        }

        transform.position = destinationPosition;
    }

    #region Helpers
    //bool IsBlocked(Vector3 targetPosition) {
    //    // this got forced to be blocked by another moving product.
    //    // this prevents 2 products that would be too close from moving together,
    //    // by (ab)using the difference in position between the their FixedUpdate() calls.
    //    if (forceBlocked) {
    //        return true;
    //    }

    //    // machines can also have products, this prevents them moving after spawn.
    //    foreach(var belt in Grid.GetSurrounding<AbstractPlacedItem>(targetPosition, true)) {
    //        for(int i = 0; i < belt.products.Count; i++) {
    //            var product = belt.products[i];
    //            if(product != this) {
    //                var isFartherFromTarget = ((transform.position - targetPosition).sqrMagnitude > (product.transform.position - targetPosition).sqrMagnitude);
    //                // first, check if those two Products "really" collide, which would mean one HAS to stop if we want to prevent overlapping Products.
    //                if (CheckCollision(product, productCollisionRadius)) {
    //                    if (isFartherFromTarget) {
    //                        return true;
    //                    } else {
    //                        product.forceBlocked = true;
    //                    }
    //                }
    //                // second (wider) collision creates a priority queue, and forces products on joined belts to stop BEFORE they actually collide.
    //                if (CheckCollision(product, productCollisionRadius * 2f)) {
    //                    if (isFartherFromTarget) {
    //                        if (blockedSince <= 0 || (product.blockedSince > 0f && blockedSince >= product.blockedSince)) {
    //                            return true;
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    return false;
    //}

    //bool CheckCollision(Product other, float size) {
    //    float x0 = transform.position.x, y0 = transform.position.z;
    //    float x1 = other.transform.position.x, y1 = other.transform.position.z;
    //    return (x0 < x1 + size && x0 + size > x1 && y0 < y1 + size && y0 + size > y1);
    //}
    #endregion
}
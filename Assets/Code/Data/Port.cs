using UnityEngine;

// This class is used instead of simply storing the reference to AbstractPlacedItem in inputs.
public class Port {
    // this is not the connected item, but the item this port belongs to, usually "this".
    // to get the other item, use connectedPort.item
    public AbstractPlacedItem me;

    // target block of the connection.
    // this is not the block where the other item should be, but the block where the Products should move to.
    // usually this is part of the shape.
    public Vector3 portCenter;

    // Direction from which connections get accepted.
    // this defines the side of the block the connection should be on.
    public EnumDirection direction;

    // the connected port of the other item, or null.
    public AbstractPlacedItem output;

    public Port(AbstractPlacedItem item, int x, int z, EnumDirection direction) {
        Assert.IsTrue(item != null);
        this.me = item;
        portCenter = new Vector3(x, 0f, z);
        this.direction = direction;
        output = null;
    }
}
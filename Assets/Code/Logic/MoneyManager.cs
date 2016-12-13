using System;
using UnityEngine;

public class MoneyManager : MonoBehaviour {
    public static float cash = 20000; 

    public static bool CanAfford(AbstractPlacedItem item) {
        return item.cost <= cash;
    }

    public static bool Buy(AbstractPlacedItem item) {
        if(CanAfford(item)) {
            cash -= item.cost;
            return true;
        }
        return false;
    }

    public static float ValueOf(string productType) {
        switch(productType) {
        case "Arm":
            return 220340;
        case "Bar":
            return 30;
        case "Board":
            return 10;
        case "Body":
            return 1900000;
        case "Boots":
            return 1043100;
        case "Chest":
            return 549030;
        case "Head":
            return 578988;
        case "LED":
            return 2;
        case "Motor":
            return 902;
        case "PCB":
            return 517;
        case "Plastic":
            return 2;
        case "Processor":
            return 1767;
        case "Robot":
            return 2611798;
        case "Box":
            return 5;
        case "Silicon":
            return 54;
        default:
            throw new Exception();
        }
    }

    internal static void Sell(InputRequirements[] productsRecieved) {
        for(int i = 0; i < productsRecieved.Length; i++) {
            cash += productsRecieved[i].partsReceived * ValueOf(productsRecieved[i].inputType);
            productsRecieved[i].partsReceived = 0;
        }
    }

    public static void Refund(AbstractPlacedItem item) {
        cash += item.cost;
    }
}


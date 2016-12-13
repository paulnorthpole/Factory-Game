using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CollectionForSale : Machine {
    protected void Start() {
        Grid.PlaceItem(this);
        inputRequirements = new[] {
            new InputRequirements { inputType = "Arm", maxStorage = 100 },
            new InputRequirements { inputType = "Bar", maxStorage = 100 },
            new InputRequirements { inputType = "Board", maxStorage = 100 },
            new InputRequirements { inputType = "Body", maxStorage = 100 },
            new InputRequirements { inputType = "Boots", maxStorage = 100 },
            new InputRequirements { inputType = "Box", maxStorage = 100 },
            new InputRequirements { inputType = "Chest", maxStorage = 100 },
            new InputRequirements { inputType = "Head", maxStorage = 100 },
            new InputRequirements { inputType = "LED", maxStorage = 100 },
            new InputRequirements { inputType = "Motor", maxStorage = 100 },
            new InputRequirements { inputType = "PCB", maxStorage = 100 },
            new InputRequirements { inputType = "Plastic", maxStorage = 100 },
            new InputRequirements { inputType = "Processor", maxStorage = 100 },
            new InputRequirements { inputType = "Robot", maxStorage = 100 },
            new InputRequirements { inputType = "Silicon", maxStorage = 100 },
        };
        maxTotalStorage = 100;
    }

    public override void OnClick() {
        MoneyManager.Sell(inputRequirements);
    }
}


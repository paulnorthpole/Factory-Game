using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartButton : MonoBehaviour {
    public GameObject prefab;

    protected void Start() {
        if(prefab == null) {
            return;
        }
        
        var placedItem = prefab.GetComponent<AbstractPlacedItem>();
        var textObjects = GetComponentsInChildren<Text>();
        textObjects[0].text = placedItem.name;
        textObjects[1].text = "$" + placedItem.cost.ToString("N0");
        textObjects[2].text = "";
        if(placedItem is Machine) {
            var machine = (Machine)placedItem;
            for(int i = 0; i < machine.inputRequirements.Length; i++) {
                if(i == 0) {
                    textObjects[2].text += "[";
                } else {
                    textObjects[2].text += ", ";
                }
                textObjects[2].text += machine.inputRequirements[i].inputCountPerProduct + "x " + machine.inputRequirements[i].inputType;
            }
            if(textObjects[2].text != "") {
                textObjects[2].text += "]";
            }
        }
    }

    public void OnClick() {
        if(FloorPlacementController.thingToPlace != null) {
            Destroy(FloorPlacementController.thingToPlace.gameObject);
        }
        if(MoneyManager.CanAfford(prefab.GetComponent<AbstractPlacedItem>())) {
            FloorPlacementController.thingToPlace = Instantiate(prefab).GetComponent<AbstractPlacedItem>();
            Assert.IsTrue(FloorPlacementController.thingToPlace != null);
        }
    }
}

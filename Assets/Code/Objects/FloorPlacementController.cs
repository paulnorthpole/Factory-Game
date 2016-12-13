using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class FloorPlacementController : MonoBehaviour {
    public static AbstractPlacedItem thingToPlace;
    AudioSource onPlaceSound;

    #region Events
    protected void Start() {
        onPlaceSound = GetComponent<AudioSource>();
    }
    protected void OnMouseOver() {
        if(thingToPlace != null) {
            var hit = GetMouseHit();
            if(hit != null) {
                thingToPlace.transform.position = new Vector3(
                   Mathf.Round(hit.Value.point.x),
                   thingToPlace.transform.position.y,
                   Mathf.Round(hit.Value.point.z));
            }
        }
    }
    protected void Update() {
        if(thingToPlace != null) {
            if(Input.GetButtonDown("Rotate")) {
                var from = thingToPlace.transform.rotation.eulerAngles;
                thingToPlace.transform.rotation =
                    Quaternion.Euler(from.x, from.y + 90, from.z);
            }
            if(Input.GetMouseButtonDown(1)) {
                Destroy(thingToPlace.gameObject);
                thingToPlace = null;
            }
        }
        if(Input.GetMouseButtonDown(0)) {
            if(!EventSystem.current.IsPointerOverGameObject()) {
                var hit = GetMouseHit();
                if(hit != null) {
                    var itemAtLocation = Grid.GetItem(hit.Value.point);
                    if(thingToPlace != null && itemAtLocation == null) {
                        if(MoneyManager.Buy(thingToPlace)) {
                            if(MoneyManager.CanAfford(thingToPlace)) {
                                var thingToReallyPlace = Instantiate(FloorPlacementController.thingToPlace);
                                Grid.PlaceItem(thingToReallyPlace);
                                thingToReallyPlace.OnConnectionChange();
                                onPlaceSound.Play();
                            } else {
                                Destroy(thingToPlace);
                                thingToPlace = null;
                            }
                        } else {
                            Destroy(thingToPlace);
                            thingToPlace = null;
                        }
                    } else if(thingToPlace == null && itemAtLocation != null) {
                        itemAtLocation.OnClick();
                    }
                    if(itemAtLocation != null) {
                        foreach(var item in Grid.items) {
                            if(item != null) {
                                item.OnConnectionChange();
                            } 
                        }
                    }
                }
            }
        }

        if(thingToPlace != null) {
            thingToPlace.OnConnectionChange();
        }
    }
    #endregion

    #region Helpers
    RaycastHit? GetMouseHit() {
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var mask = LayerMask.GetMask(new[] { "Floor" });
        if(Physics.Raycast(ray, out hit, 100, mask)) {
            return hit;
        }
        return null;
    }
    #endregion
}
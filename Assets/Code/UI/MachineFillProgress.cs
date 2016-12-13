using System;
using UnityEngine;
using UnityEngine.UI;

public class MachineFillProgress : MonoBehaviour {
    Machine item;
    Slider slider;

    protected void Start() {
        item = GetComponentInParent<Machine>();
        slider = GetComponent<Slider>();
    }

    protected void Update() {
        slider.value = item.percentFull;
    } 
}


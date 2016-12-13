using System;
using UnityEngine;
using UnityEngine.UI;

public class TextMoney : MonoBehaviour {
    Text text;

    private void Start() {
        text = GetComponent<Text>();
    }

    private void Update() {
        text.text = "$" + MoneyManager.cash.ToString("N0");
    }
}


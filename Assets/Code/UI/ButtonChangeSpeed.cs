using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChangeSpeed : MonoBehaviour {

    public void OnClick() {
        var text = GetComponentInChildren<Text>().text;
        Time.timeScale = int.Parse(text.Substring(0, text.Length - 1));
    }
}

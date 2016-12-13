using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryButton : MonoBehaviour {

    public void OnClick() {
        CategoryButtons.selectedCategory = GetComponentInChildren<Text>().text;
    }
}

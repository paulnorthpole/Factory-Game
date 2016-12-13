using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryButtons : MonoBehaviour {
    static string _selectedCategory;
    public static string selectedCategory {
        get {
            return _selectedCategory;
        }
        set {
            _selectedCategory = value;
            if(onChange != null) {
                onChange();
            }
        }
    }
    public string[] categories;
    internal static event Action onChange;

    protected void Start() {
        var model = transform.FindChild("Button");
        for(int i = 0; i < categories.Length; i++) {
            var button = Instantiate(model, transform);
            button.GetComponentInChildren<Text>().text = categories[i];
        }
        selectedCategory = categories[0];
        Destroy(model.gameObject);
    }

}

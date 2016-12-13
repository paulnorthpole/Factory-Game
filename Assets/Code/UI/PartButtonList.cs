using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PartButtonCategororizedList {
    public string category;
    public GameObject[] prefab;

}

public class PartButtonList : MonoBehaviour {
    public PartButtonCategororizedList[] categories;
    Transform model;


    protected void Start() {
        model = transform.FindChild("Button");
        model.gameObject.SetActive(false);
        CategoryButtons.onChange += OnChange;
        OnChange();
    }

    protected void OnChange() { 
        for(int iKid = 1; iKid < transform.childCount; iKid++) {
            Destroy(transform.GetChild(iKid).gameObject);
        }
        for(int iCat = 0; iCat < categories.Length; iCat++) {
            if(categories[iCat].category == CategoryButtons.selectedCategory) {
                var prefab = categories[iCat].prefab;
                for(int i = 0; i < prefab.Length; i++) {
                    var button = Instantiate(model, transform);
                    button.gameObject.SetActive(true);
                    button.GetComponent<PartButton>().prefab = prefab[i];
                }
            }
        }

    }
}

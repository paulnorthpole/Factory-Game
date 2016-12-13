using UnityEngine;
using System.Collections.Generic;

public class TextureScroll : MonoBehaviour {
    public Vector2 speed;
    public string textureName = "_MainTex";
    private List<Material> materials;

    void Start() {
        materials = new List<Material>();
        foreach(var renderer in GetComponents<Renderer>()) {
            foreach(var material in renderer.materials) {
                materials.Add(material);
            }
        }
    }

    public void Update() {
        foreach(var material in materials) {
            var currentOffs = material.GetTextureOffset(textureName);
            currentOffs += speed * Time.deltaTime;
            material.SetTextureOffset(textureName, currentOffs);
        }
    }
}
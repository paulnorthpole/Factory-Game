//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class DebugTextDisplayPartInfo : MonoBehaviour {
//    Text text;

//    protected void Start() {
//        text = GetComponent<Text>();
//    }

//    void Update() {
//        var gridCellItem = Grid.GetItem(transform.position);
//        if(gridCellItem != null) {
//            int x, y;
//            if(Grid.GetCoords(gridCellItem.transform.position, out x, out y)) {
//                text.text = x + ", " + y;
//                if(gridCellItem.firstConnectedOutput != null) {
//                    if(Grid.GetCoords(gridCellItem.firstConnectedOutput.output.me.transform.position, out x, out y)) {
//                        text.text += " " + gridCellItem.firstConnectedOutput.output.me.GetType().Name + " @ " + x + ", " + y;
//                    }
//                }
//                //for(int i = 0; i < gridCellItem.inputs.Count; i++) {
//                //    text.text += " [" + gridCellItem.inputs[i].gridX + ", " + gridCellItem.inputs[i].gridZ + "]";
//                //}
//            }
//        }
//    }
//}

//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using System.Linq;


////--------------------------------
////Let the magic happen(hopefully)
////--------------------------------

//[CustomEditor(typeof(PartButtonList))] 
//public class ListPartButtonListeditor : Editor {
//	int selected = -1;
//	public override void OnInspectorGUI() {
//		PartButtonList partbuttons = (PartButtonList)target;
//		List<GameObject> prefabs = new List<GameObject> ();
//		for (int j = 0; j < partbuttons.prefab.Length; j++) {
//			EditorGUILayout.BeginHorizontal ();
//			partbuttons.prefab [j] = (GameObject)EditorGUILayout.ObjectField (partbuttons.prefab [j], typeof(GameObject), true);
//			if (j != 0) {
//				if (GUILayout.Button ("^", GUILayout.MaxWidth (19))) { 
//					prefabs = partbuttons.prefab.ToList ();
//					prefabs.RemoveAt (j);
//					prefabs.Insert (j - 1, partbuttons.prefab [j]);
//					partbuttons.prefab = prefabs.ToArray ();
//				}
//			} else {
//				GUILayout.Button (" ", GUILayout.MaxWidth (19));
//			}
//			if (j != partbuttons.prefab.Length-1) {
//				if (GUILayout.Button ("v",GUILayout.MaxWidth(19))){ 
//					prefabs = partbuttons.prefab.ToList ();
//					prefabs.RemoveAt (j);
//					prefabs.Insert (j+1,partbuttons.prefab[j]);
//					partbuttons.prefab = prefabs.ToArray ();
//				}
//			} else {
//				GUILayout.Button (" ", GUILayout.MaxWidth (19));
//			}

			
//			if (GUILayout.Button ("X", GUILayout.MaxWidth (19))) { 
//				prefabs = partbuttons.prefab.ToList ();
//				prefabs.RemoveAt (j);
//				partbuttons.prefab = prefabs.ToArray ();
//			}

//			EditorGUILayout.EndHorizontal ();

//		}
//		if (GUILayout.Button ("+")) {
//			prefabs = partbuttons.prefab.ToList ();
//			prefabs.Add (null);
//			partbuttons.prefab = prefabs.ToArray ();
//		}
//	}
//}

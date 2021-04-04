using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(MapGenerator))]
public class MapEditor : Editor
{
	public override void OnInspectorGUI(){
		MapGenerator mapGen = (MapGenerator)target;

		DrawDefaultInspector();

		if(DrawDefaultInspector() && mapGen.AutoUpdate) mapGen.GenerateMap();
		if(GUILayout.Button("Generate")) mapGen.GenerateMap();
	}
}

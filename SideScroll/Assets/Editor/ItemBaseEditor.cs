using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof (ItemDatabase))]
public class ItemBaseEditor : Editor
{
    private ItemDatabase itemDatabase;
    
    private void Awake() 
    {
        itemDatabase = (ItemDatabase)target;
    }
    public override void OnInspectorGUI()
    {
        
        GUILayout.BeginHorizontal();
        
         if(GUILayout.Button("<<"))
        {
            itemDatabase.PreviousItem();
        }
         if(GUILayout.Button(">>"))
        {
            itemDatabase.NextItem();
        }
        GUILayout.EndHorizontal();
        if(GUILayout.Button("New Item"))
        {
            itemDatabase.CreateItem();
        }
         if(GUILayout.Button("Remove Item"))
        {
            itemDatabase.RemoveItem();
        }
        base.OnInspectorGUI();
    }
   
}

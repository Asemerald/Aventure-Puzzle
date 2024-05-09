#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Array))]
public class ArrayEditor : Editor
{
    SerializedProperty generationMode;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        Array data = (Array)target;

        //base.OnInspectorGUI();

        //General permanent properties
        EditorGUILayout.PropertyField(serializedObject.FindProperty("prefab"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("preservePrefab"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("generateOnStart"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("makeNewObject"));
        if (data.makeNewObject)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("inPlace"));
        }
        EditorGUILayout.PropertyField(serializedObject.FindProperty("namingMode"));
        if (data.namingMode == NamingModeEnum.CustomName)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("customName"));
        }
        EditorGUILayout.PropertyField(serializedObject.FindProperty("generationMode"));

        // 1D conditionnal properties
        if(data.generationMode == GenerationModeEnum.Line1D)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("count1D"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("objectOffset"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("resetRotation"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("applyParentRotation"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("center"));
        }

        // 2D conditionnal properties
        if(data.generationMode == GenerationModeEnum.Grid2D)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("count2D"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("objectOffset"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("rowOffset"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("resetRotation"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("applyParentRotation"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("center"));
        }

        // 3D conditionnal properties
        if(data.generationMode == GenerationModeEnum.Volume3D)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("count3D"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("objectOffset"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("rowOffset"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("floorOffset"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("resetRotation"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("applyParentRotation"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("center"));
        }

        if(data.generationMode == GenerationModeEnum.Helix)
        {
            EditorGUILayout.LabelField("Not supported yet...");
        }

        //Randomizer permanent properties
        EditorGUILayout.PropertyField(serializedObject.FindProperty("randomPrefab"));
        if (data.randomPrefab)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("startPrefabs"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("midPrefabs"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("endPrefabs"));
        }

        EditorGUILayout.PropertyField(serializedObject.FindProperty("randomSprite"));
        if (data.randomSprite)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("startSprites"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("midSprites"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("endSprites"));
        }
        
        GUILayout.Space(20);
        if (data.prefab == null)
        {
            EditorGUILayout.LabelField("Can not generate anything if no prefab is specified");
        }
        if (GUILayout.Button("Generate") && data.prefab != null){
            //Debug.Log("HallooooOooOOOOOooOOOO!");
            data.Generate();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif

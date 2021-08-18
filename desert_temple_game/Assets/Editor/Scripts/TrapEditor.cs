using System;
using System.Reflection;
using UnityEditor;

[CustomEditor(typeof(TrapBase))]
public class TrapEditor : Editor
{

    MethodInfo[] methodInfos;
    string[] methodNames;

    SerializedProperty trapActivationMode;
    SerializedProperty sphereRadius;
    SerializedProperty range;
    SerializedProperty rayDirection;
    SerializedProperty effectSelection;
    SerializedProperty continuous;
    SerializedProperty effectRate;

    private void OnEnable()
    {
        methodInfos = typeof(TrapEffects).GetMethods(BindingFlags.Public | BindingFlags.Static);

        Array.Sort(methodInfos,
            delegate (MethodInfo methodInfo1, MethodInfo methodInfo2)
            { return methodInfo1.Name.CompareTo(methodInfo2.Name); });

        methodNames = new string[methodInfos.Length];
        for (int i = 0; i < methodInfos.Length; i++)
        {
            methodNames[i] = methodInfos[i].Name;
        }

        trapActivationMode = serializedObject.FindProperty("trapActivationMode");
        sphereRadius = serializedObject.FindProperty("sphereRadius");
        range = serializedObject.FindProperty("range");
        rayDirection = serializedObject.FindProperty("rayDirection");
        effectSelection = serializedObject.FindProperty("effectSelection");
        continuous = serializedObject.FindProperty("continuous");
        effectRate = serializedObject.FindProperty("effectRate");

    }

    

    public override void OnInspectorGUI()
    {

        serializedObject.Update();
        TrapBase trapBase = serializedObject.targetObject as TrapBase;

        trapBase.effectSelection = EditorGUILayout.Popup("Effect", effectSelection.intValue, methodNames);

        EditorGUILayout.PropertyField(trapActivationMode);

        switch (trapBase.trapActivationMode) 
        {
            case TrapBase.TrapActivationMode.RADIUS:
                EditorGUILayout.PropertyField(sphereRadius);
                break;
            case TrapBase.TrapActivationMode.LINE:
                EditorGUILayout.PropertyField(range);
                EditorGUILayout.PropertyField(rayDirection);
                break;
        
        }

        EditorGUILayout.PropertyField(continuous);

        if (trapBase.continuous)
            EditorGUILayout.PropertyField(effectRate);

        serializedObject.ApplyModifiedProperties();


    }
}

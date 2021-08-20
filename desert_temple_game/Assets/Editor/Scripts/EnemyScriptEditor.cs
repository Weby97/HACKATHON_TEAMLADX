using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyScript))]
public class EnemyScriptEditor : Editor
{
    SerializedProperty healthModifier;
    SerializedProperty speedModifier;
    SerializedProperty damageModifier;

    public void OnEnable()
    {
        healthModifier = serializedObject.FindProperty("overrideHealth");
        speedModifier = serializedObject.FindProperty("overrideSpeed");
        damageModifier = serializedObject.FindProperty("overrideDamage");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        EnemyScript enemyScript = serializedObject.targetObject as EnemyScript;

        if (enemyScript.overrideEnemyData)
        {
            EditorGUILayout.PropertyField(healthModifier);
            EditorGUILayout.PropertyField(speedModifier);
            EditorGUILayout.PropertyField(damageModifier);
        }

        serializedObject.ApplyModifiedProperties();
    }

}

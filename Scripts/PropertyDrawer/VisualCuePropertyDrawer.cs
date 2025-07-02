using System.Numerics;
using System.Diagnostics;
using System.Globalization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using Microgestures;
using UnityEngine.UIElements;


#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
[CustomPropertyDrawer (typeof(VisualCue))]
public class VisualCuePropertyDrawer : ConditionnalPropertyDrawer
{    
    SerializedProperty gameObjectProp;
    // SerializedProperty behaviorProp;
    SerializedProperty placeholderProp;

    protected override void initializeProperties(SerializedProperty property) {
        gameObjectProp = property.FindPropertyRelative ("gameObject");
        // behaviorProp = property.FindPropertyRelative ("behavior");
        placeholderProp = property.FindPropertyRelative ("placeholder");
    }

    protected override void OnConditionnalGUI (SerializedProperty property) {
        if (property.isArray) {
            EditorGUI.PropertyField(tools.getCurrentPosition(), property, true);
        } else {
            initializePropertyHeight(gameObjectProp, placeholderProp);
            tools.initialize();
            tools.insertField(gameObjectProp);
            // beginHorizontal(maxHeightInProps(gameObjectProp, behaviorProp));
            // tools.beginHorizontal(tools.maxHeightInProps(gameObjectProp));
            // tools.insertField(gameObjectProp, 0.4f);
            // tools.insertNone(0.075f);
            // insertLabel("Behavior", 0.200f);
            // insertField(behaviorProp, 0.325f);
            // tools.endHorizontal();
            tools.insertField(placeholderProp);
        }
    }
}

#endif
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
[CustomPropertyDrawer (typeof(TransformElements))]
public class TransformElementsPropertyDrawer : ConditionnalPropertyDrawer
{    
    SerializedProperty positionXProp;
    SerializedProperty positionYProp;
    SerializedProperty positionZProp;
    SerializedProperty rotationXProp;
    SerializedProperty rotationYProp;
    SerializedProperty rotationZProp;

    protected override void initializeProperties(SerializedProperty property) {
        positionXProp = property.FindPropertyRelative ("positionX");
        positionYProp = property.FindPropertyRelative ("positionY");
        positionZProp = property.FindPropertyRelative ("positionZ");
        rotationXProp = property.FindPropertyRelative ("rotationX");
        rotationYProp = property.FindPropertyRelative ("rotationY");
        rotationZProp = property.FindPropertyRelative ("rotationZ");
    }

    protected override void OnConditionnalGUI (SerializedProperty property) {
        if (property.isArray) {
            EditorGUI.PropertyField(tools.getCurrentPosition(), property, true);
        } else {
            initializePropertyHeight(positionXProp, rotationXProp);
            tools.initialize();
            tools.beginHorizontal();
            tools.insertLabel("Position", 110);
            tools.insertLabel("X", 5);
            tools.insertField(positionXProp, 0.15f);
            tools.insertLabel("Y", 5);
            tools.insertField(positionYProp, 0.15f);
            tools.insertLabel("Z", 5);
            tools.insertField(positionZProp, 0.15f);
            tools.endHorizontal();
            tools.beginHorizontal();
            tools.insertLabel("Rotation", 110);
            tools.insertLabel("X", 5);
            tools.insertField(rotationXProp, 0.15f);
            tools.insertLabel("Y", 5);
            tools.insertField(rotationYProp, 0.15f);
            tools.insertLabel("Z", 5);
            tools.insertField(rotationZProp, 0.15f);
            tools.endHorizontal();
        }
    }
}

#endif
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
[CustomPropertyDrawer (typeof(Command))]
public class CommandPropertyDrawer : ConditionnalPropertyDrawer
{    
    SerializedProperty textProp;
    SerializedProperty fontSizeProp;
    SerializedProperty textColorProp;
    SerializedProperty outlineColorProp;
    SerializedProperty outlineWidthProp;
    SerializedProperty textLocationProp;
    SerializedProperty transformElementsProp;

    private TextLocation textLocation;

    protected override void initializeProperties(SerializedProperty property) {
        textProp = property.FindPropertyRelative ("text");
        fontSizeProp = property.FindPropertyRelative ("fontSize");
        textColorProp = property.FindPropertyRelative ("textColor");
        outlineColorProp = property.FindPropertyRelative ("outlineColor");
        outlineWidthProp = property.FindPropertyRelative ("outlineWidth");
        textLocationProp = property.FindPropertyRelative ("textLocation");
        transformElementsProp = property.FindPropertyRelative ("transformElements");

        textLocation = (TextLocation) textLocationProp.enumValueIndex;
    }

    protected override void OnConditionnalGUI (SerializedProperty property) {
        if (property.isArray) {
            EditorGUI.PropertyField(tools.getCurrentPosition(), property, true);
        } else {
            initializePropertyHeight(textProp, textLocationProp, outlineColorProp, outlineColorProp, transformElementsProp);
            tools.initialize();
            tools.beginHorizontal();
            tools.insertLabel("Command name", 110);
            textProp.stringValue = (string) tools.insertTextField(textProp.stringValue);
            tools.insertLabel("Font size",65);
            fontSizeProp.floatValue = (float) tools.insertFloat(fontSizeProp.floatValue, 0.1f);
            tools.endHorizontal();
            tools.beginHorizontal();
            tools.insertLabel("Text location", 85);
            textLocationProp.enumValueIndex = (int) (TextLocation) tools.insertEnum(textLocation, 0.15f);
            tools.insertLabel("Text color", 85);
            textColorProp.colorValue = (Color) tools.insertColor(textColorProp.colorValue);
            tools.endHorizontal();
            tools.beginHorizontal();
            tools.insertLabel("Outline color", 85);
            outlineColorProp.colorValue = (Color) tools.insertColor(outlineColorProp.colorValue);
            tools.insertLabel("Outline width", 85);
            outlineWidthProp.floatValue = (float) tools.insertFloat(outlineWidthProp.floatValue, 0.1f);
            tools.endHorizontal();
            tools.insertField(transformElementsProp);
        }
    }
}


#endif
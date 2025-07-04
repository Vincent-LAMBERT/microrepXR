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
[CustomPropertyDrawer (typeof(Placeholder))]
public class PlaceholderPropertyDrawer : ConditionnalPropertyDrawer
{
    
    SerializedProperty fingersStatusProp;
    SerializedProperty uniqueLocationProp;
    SerializedProperty joinedLocationProp;
    SerializedProperty AwayLocationProp;

    private bool selectedUnique;
    private bool selectedJoined;

    protected override void initializeProperties(SerializedProperty property) {
        fingersStatusProp = property.FindPropertyRelative ("fingersStatus");
        uniqueLocationProp = property.FindPropertyRelative ("uniqueLocation");
        joinedLocationProp = property.FindPropertyRelative ("joinedLocation");
        AwayLocationProp = property.FindPropertyRelative ("awayLocation");

        selectedUnique = (fingersStatusProp.enumValueIndex == (int) FingersStatus.Unique);
        selectedJoined = (fingersStatusProp.enumValueIndex == (int) FingersStatus.Joined);

    }

    protected override void OnConditionnalGUI (SerializedProperty property) {
        if (property.isArray) {
            EditorGUI.PropertyField(tools.getCurrentPosition(), property, false);
        } else {
            tools.initialize();
            tools.beginHorizontal();
            if (tools.insertRadio(selectedUnique && !selectedJoined)) {
                selectedUnique = true;
                selectedJoined = false;
            };
            tools.insertLabel("Always visible on a specific finger", 300);
            tools.endHorizontal();
            tools.beginHorizontal();
            if (tools.insertRadio(!selectedUnique && selectedJoined)) {
                selectedUnique = false;
                selectedJoined = true;
            };
            tools.insertLabel("Visible on two fingers that are joined", 300);
            tools.endHorizontal();
            tools.beginHorizontal();
            if (tools.insertRadio(!selectedUnique && !selectedJoined)) {
                selectedUnique = false;
                selectedJoined = false;
            };
            tools.insertLabel("Visible between two fingers that are NOT joined", 300);
            tools.endHorizontal();
            
            if (selectedUnique) {
                fingersStatusProp.enumValueIndex = 0;
                initializePropertyHeight(fingersStatusProp, uniqueLocationProp);
                tools.insertField(uniqueLocationProp);
            } else if (selectedJoined) {
                fingersStatusProp.enumValueIndex = 1;
                initializePropertyHeight(fingersStatusProp, joinedLocationProp);
                tools.insertField(joinedLocationProp);
            } else {
                fingersStatusProp.enumValueIndex = 2;
                initializePropertyHeight(fingersStatusProp, AwayLocationProp);
                tools.insertField(AwayLocationProp);
            }
        }
    }
}

#endif

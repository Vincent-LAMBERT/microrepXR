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
    
    SerializedProperty placeholderBehaviorProp;
    SerializedProperty uniqueLocationProp;
    SerializedProperty joinedLocationProp;
    SerializedProperty awayLocationProp;

    private bool selectedAlwaysVisibleUnique;
    private bool selectedVisibleWhenNotJoined;
    private bool selectedVisibleWhenTwoJoined;
    private bool selectedVisibleWhenTwoAway;

    protected override void initializeProperties(SerializedProperty property) {
        placeholderBehaviorProp = property.FindPropertyRelative ("placeholderBehavior");
        uniqueLocationProp = property.FindPropertyRelative ("uniqueLocation");
        joinedLocationProp = property.FindPropertyRelative ("joinedLocation");
        awayLocationProp = property.FindPropertyRelative ("awayLocation");

        selectedAlwaysVisibleUnique = (placeholderBehaviorProp.enumValueIndex == (int) PlaceholderBehavior.AlwaysVisibleUnique);
        selectedVisibleWhenNotJoined = (placeholderBehaviorProp.enumValueIndex == (int) PlaceholderBehavior.VisibleWhenNotJoined);
        selectedVisibleWhenTwoJoined = (placeholderBehaviorProp.enumValueIndex == (int) PlaceholderBehavior.VisibleWhenTwoJoined);
        selectedVisibleWhenTwoAway = (placeholderBehaviorProp.enumValueIndex == (int) PlaceholderBehavior.VisibleWhenTwoAway);
    }

    protected override void OnConditionnalGUI (SerializedProperty property) {
        if (property.isArray) {
            EditorGUI.PropertyField(tools.getCurrentPosition(), property, false);
        } else {
            tools.initialize();
            tools.beginHorizontal();
            if (tools.insertRadio(selectedAlwaysVisibleUnique)) {
                selectedAlwaysVisibleUnique = true;
                selectedVisibleWhenNotJoined = false;
                selectedVisibleWhenTwoJoined = false;
                selectedVisibleWhenTwoAway = false;
            };
            tools.insertLabel("Always visible on a specific finger", 300);
            tools.endHorizontal();
            tools.beginHorizontal();
            if (tools.insertRadio(selectedVisibleWhenNotJoined)) {
                selectedAlwaysVisibleUnique = false;
                selectedVisibleWhenNotJoined = true;
                selectedVisibleWhenTwoJoined = false;
                selectedVisibleWhenTwoAway = false;
            };
            tools.insertLabel("Visible on a finger that is NOT joined with another", 300);
            tools.endHorizontal();
            tools.beginHorizontal();
            if (tools.insertRadio(selectedVisibleWhenTwoJoined)) {
                selectedAlwaysVisibleUnique = false;
                selectedVisibleWhenNotJoined = false;
                selectedVisibleWhenTwoJoined = true;
                selectedVisibleWhenTwoAway = false;
            };
            tools.insertLabel("Visible on two fingers that are joined", 300);
            tools.endHorizontal();
            tools.beginHorizontal();
            if (tools.insertRadio(selectedVisibleWhenTwoAway)) {
                selectedAlwaysVisibleUnique = false;
                selectedVisibleWhenNotJoined = false;
                selectedVisibleWhenTwoJoined = false;
                selectedVisibleWhenTwoAway = true;
            };
            tools.insertLabel("Visible between two fingers that are NOT joined", 300);
            tools.endHorizontal();
            
            if (selectedAlwaysVisibleUnique) {
                placeholderBehaviorProp.enumValueIndex = 0;
                initializePropertyHeight(placeholderBehaviorProp, uniqueLocationProp);
                tools.insertField(uniqueLocationProp);
            } else if (selectedVisibleWhenNotJoined) {
                placeholderBehaviorProp.enumValueIndex = 1;
                initializePropertyHeight(placeholderBehaviorProp, uniqueLocationProp);
                tools.insertField(uniqueLocationProp);
            } else if (selectedVisibleWhenTwoJoined) {
                placeholderBehaviorProp.enumValueIndex = 2;
                initializePropertyHeight(placeholderBehaviorProp, joinedLocationProp);
                tools.insertField(joinedLocationProp);
            } else {
                placeholderBehaviorProp.enumValueIndex = 3;
                initializePropertyHeight(placeholderBehaviorProp, awayLocationProp);
                tools.insertField(awayLocationProp);
            }
        }
    }
}

#endif

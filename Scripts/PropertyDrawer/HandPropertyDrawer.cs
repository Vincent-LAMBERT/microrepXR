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
using MixedReality.Toolkit;
// using UnityEngine.XR.Hands; // new for Handedness and XRHandJoint
using UnityEngine.UIElements;


#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
[CustomPropertyDrawer (typeof(Hand))]
public class HandPropertyDrawer : ConditionnalPropertyDrawer
{    
    SerializedProperty handednessProp;

    private bool selectedLeft;
    private bool selectedRight;

    protected override void initializeProperties(SerializedProperty property) {
        handednessProp = property.FindPropertyRelative ("handedness");
        
        selectedLeft = (handednessProp.enumValueIndex != (int) Handedness.Right);
        selectedRight = (handednessProp.enumValueIndex == (int) Handedness.Right);
        
        propertyHeight = 20;
    }

    protected override void OnConditionnalGUI (SerializedProperty property) {
        if (property.isArray) {
            EditorGUI.PropertyField(tools.getCurrentPosition(), property, true);
        } else {
            propertyHeight = 20;
            tools.initialize();
            tools.beginHorizontal();
            tools.insertLabel("Handedness", 85);
            if (tools.insertRadio(selectedLeft)) {
                selectedLeft = true;
                selectedRight = false;
            };
            tools.insertLabel("Left");
            if (tools.insertRadio(selectedRight)) {
                selectedLeft = false;
                selectedRight = true;
            };
            tools.insertLabel("Right");
            tools.endHorizontal();

            if (selectedLeft) {
                handednessProp.enumValueIndex = (int) Handedness.Left;
            } else {
                handednessProp.enumValueIndex = (int) Handedness.Right;
            }
        }
    }
}


#endif
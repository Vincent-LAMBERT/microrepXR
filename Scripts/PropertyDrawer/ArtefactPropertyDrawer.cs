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
[CustomPropertyDrawer (typeof(Artefact))]
public class ArtefactPropertyDrawer : ConditionnalPropertyDrawer
{    
    SerializedProperty gameObjectProp;
    SerializedProperty transformElementsProp;
    SerializedProperty placeholderProp;
    SerializedProperty commandProp;

    protected override void initializeProperties(SerializedProperty property) {
        gameObjectProp = property.FindPropertyRelative ("gameObject");
        transformElementsProp = property.FindPropertyRelative ("transformElements");
        placeholderProp = property.FindPropertyRelative ("placeholder");
        commandProp = property.FindPropertyRelative ("command");
    }

    protected override void OnConditionnalGUI (SerializedProperty property) {
        if (property.isArray) {
            EditorGUI.PropertyField(tools.getCurrentPosition(), property, true);
        } else {
            initializePropertyHeight(gameObjectProp, transformElementsProp, placeholderProp, commandProp);
            tools.initialize();
            tools.insertNone(0.2f);
            tools.insertField(gameObjectProp);
            tools.insertField(transformElementsProp);
            tools.insertField(placeholderProp);
            tools.insertField(commandProp);
        }
    }
}

#endif
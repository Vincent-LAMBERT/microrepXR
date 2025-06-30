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
abstract public class ConditionnalPropertyDrawer : PropertyDrawer
{
    // Height of the property.
    protected float propertyHeight;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return propertyHeight;
    }

    protected void initializePropertyHeight(params SerializedProperty[] props) {
        propertyHeight = 0;

        foreach (SerializedProperty prop in props)
        {
            propertyHeight += (int) EditorGUI.GetPropertyHeight(prop) + PropertyDrawerTools.marginY;
        }
    }

    protected abstract void initializeProperties(SerializedProperty property);
    
    protected Rect initializePositions(Rect position, int ratio) {
        Rect insidePositions = Rect.zero;
        insidePositions.x = position.x;
        insidePositions.y = position.y;
        insidePositions.width = position.width;
        insidePositions.height = position.height/ratio;
        return insidePositions;
    }
    
    protected abstract void OnConditionnalGUI(SerializedProperty property);

    protected PropertyDrawerTools tools;

    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
        label = EditorGUI.BeginProperty(position, label, property);

        initializeProperties(property);
        tools = new PropertyDrawerTools(initializePositions(position, 3));

        OnConditionnalGUI(property);
        
        EditorGUI.EndProperty();
    }
}

#endif

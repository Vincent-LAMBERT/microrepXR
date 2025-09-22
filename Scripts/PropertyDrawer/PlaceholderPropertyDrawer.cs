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
    SerializedProperty placeholderIsBetweenFingersProp;
    SerializedProperty placeholderLocationProp;
    SerializedProperty placeholderOnIfBehaviorsProp;

    protected override void initializeProperties(SerializedProperty property) {
        placeholderIsBetweenFingersProp = property.FindPropertyRelative ("placeholderIsBetweenFingers");
        placeholderLocationProp = property.FindPropertyRelative ("placeholderLocation");
        placeholderOnIfBehaviorsProp = property.FindPropertyRelative ("placeholderOnIfBehaviors");
    }

    protected override void OnConditionnalGUI (SerializedProperty property) {
        if (property.isArray) {
            EditorGUI.PropertyField(tools.getCurrentPosition(), property, false);
        } else {
            initializePropertyHeight(placeholderIsBetweenFingersProp, placeholderIsBetweenFingersProp, placeholderIsBetweenFingersProp, placeholderIsBetweenFingersProp);
            tools.initialize();
            tools.beginHorizontal();
            tools.insertLabel("Visible", 50);
            OnBetweenEnum onBetweenEnum = (OnBetweenEnum) tools.insertEnum(OnBetweenEnum.On, 0.15f);
            if (onBetweenEnum == OnBetweenEnum.Between) {
                placeholderIsBetweenFingersProp.boolValue = true;
                FingerEnum fingerEnum = (FingerEnum) tools.insertEnum(FingerEnum.Index, 0.15f);
                if (OneZoneActorEnum.TryParse(fingerEnum.ToString(), out OneZoneActorEnum oneZoneActor)) {
                    OneZoneActorZone zoneEnum = (OneZoneActorZone) tools.insertEnum(OneZoneActorZone.Tip, 0.15f);
                } else if (TwoZoneActorEnum.TryParse(fingerEnum.ToString(), out TwoZoneActorEnum twoZoneActor)) {
                    TwoZoneActorZone zoneEnum = (TwoZoneActorZone) tools.insertEnum(TwoZoneActorZone.Tip, 0.15f);
                } else if (ThreeZoneActorEnum.TryParse(fingerEnum.ToString(), out ThreeZoneActorEnum threeZoneActor)) {
                    ThreeZoneActorZone zoneEnum = (ThreeZoneActorZone) tools.insertEnum(ThreeZoneActorZone.Tip, 0.15f);
                } else {
                    tools.insertLabel("Error: not a finger", 100);
                }
            } else {
                placeholderIsBetweenFingersProp.boolValue = false;
                FingerEnum fingerEnumB1 = (FingerEnum) tools.insertEnum(FingerEnum.Index, 0.15f);
                if (OneZoneActorEnum.TryParse(fingerEnumB1.ToString(), out OneZoneActorEnum oneZoneActorB1)) {
                    OneZoneActorZone zoneEnumB1 = (OneZoneActorZone) tools.insertEnum(OneZoneActorZone.Tip, 0.15f);
                } else if (TwoZoneActorEnum.TryParse(fingerEnumB1.ToString(), out TwoZoneActorEnum twoZoneActorB1)) {
                    TwoZoneActorZone zoneEnumB1 = (TwoZoneActorZone) tools.insertEnum(TwoZoneActorZone.Tip, 0.15f);
                } else if (ThreeZoneActorEnum.TryParse(fingerEnumB1.ToString(), out ThreeZoneActorEnum threeZoneActorB1)) {
                    ThreeZoneActorZone zoneEnumB1 = (ThreeZoneActorZone) tools.insertEnum(ThreeZoneActorZone.Tip, 0.15f);
                } else {
                    tools.insertLabel("Error: not a finger", 100);
                }
                tools.insertLabel("AND", 40);
                FingerEnum fingerEnumB2 = (FingerEnum) tools.insertEnum(FingerEnum.Index, 0.15f);
                if (OneZoneActorEnum.TryParse(fingerEnumB2.ToString(), out OneZoneActorEnum oneZoneActorB2)) {
                    OneZoneActorZone zoneEnumB2 = (OneZoneActorZone) tools.insertEnum(OneZoneActorZone.Tip, 0.15f);
                } else if (TwoZoneActorEnum.TryParse(fingerEnumB2.ToString(), out TwoZoneActorEnum twoZoneActorB2)) {
                    TwoZoneActorZone zoneEnumB2 = (TwoZoneActorZone) tools.insertEnum(TwoZoneActorZone.Tip, 0.15f);
                } else if (ThreeZoneActorEnum.TryParse(fingerEnumB2.ToString(), out ThreeZoneActorEnum threeZoneActorB2)) {
                    ThreeZoneActorZone zoneEnumB2 = (ThreeZoneActorZone) tools.insertEnum(ThreeZoneActorZone.Tip, 0.15f);
                } else {
                    tools.insertLabel("Error: not a finger", 100);
                }
            }
            tools.endHorizontal();
        }
    }
}

public enum OnBetweenEnum {
    On,
    Between
}

#endif

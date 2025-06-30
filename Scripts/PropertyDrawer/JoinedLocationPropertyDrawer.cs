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
[CustomPropertyDrawer (typeof(JoinedLocation))]
public class JoinedLocationPropertyDrawer : LocationPropertyDrawer
{    
    JoinedActorEnum all_actor;

    MainJoinedFingerEnum mainActor = MainJoinedFingerEnum.Index;
    IndexJoinedFingerEnum indexMate = IndexJoinedFingerEnum.Middle;
    MiddleJoinedFingerEnum middleMate = MiddleJoinedFingerEnum.Index;
    RingJoinedFingerEnum ringMate = RingJoinedFingerEnum.Middle;
    LittleJoinedFingerEnum littleMate = LittleJoinedFingerEnum.Ring;

    protected override void initializeProperties(SerializedProperty property) {
        actorProp = property.FindPropertyRelative ("actor");

        all_actor = Location.getJoinedActorEnum((ActorEnum) actorProp.enumValueIndex);

        switch (all_actor) {
            case JoinedActorEnum.IndexJoinedMiddle:
                mainActor = MainJoinedFingerEnum.Index;
                indexMate = IndexJoinedFingerEnum.Middle;
                break;
            case JoinedActorEnum.MiddleJoinedIndex:
                mainActor = MainJoinedFingerEnum.Middle;
                middleMate = MiddleJoinedFingerEnum.Index;
                break;
            case JoinedActorEnum.MiddleJoinedRing:
                mainActor = MainJoinedFingerEnum.Middle;
                middleMate = MiddleJoinedFingerEnum.Ring;
                break;
            case JoinedActorEnum.RingJoinedMiddle:
                mainActor = MainJoinedFingerEnum.Ring;
                ringMate = RingJoinedFingerEnum.Middle;
                break;
            case JoinedActorEnum.RingJoinedLittle:
                mainActor = MainJoinedFingerEnum.Ring;
                ringMate = RingJoinedFingerEnum.Little;
                break;
            case JoinedActorEnum.LittleJoinedRing:
                mainActor = MainJoinedFingerEnum.Little;
                littleMate = LittleJoinedFingerEnum.Ring;
                break;
            default:
                throw new Exception("Error on JoinedLocationProperyDrawer");
        };
    }

    protected override void OnConditionnalGUI (SerializedProperty property) {
        if (property.isArray) {
            EditorGUI.PropertyField(tools.getCurrentPosition(), property, true);
        } else {
            initializeLocationPropertyHeight();
            tools.initialize();
            tools.beginHorizontal();
            mainActor = (MainJoinedFingerEnum) tools.insertEnum(mainActor, 0.5f);

            switch (mainActor) {
                case MainJoinedFingerEnum.Index:
                    indexMate = (IndexJoinedFingerEnum) tools.insertEnum(indexMate, 0.5f);
                    actorProp.enumValueIndex = (int) ActorEnum.IndexJoinedMiddle;
                    break;
                case MainJoinedFingerEnum.Middle:
                    middleMate = (MiddleJoinedFingerEnum) tools.insertEnum(middleMate, 0.5f);
                    switch (middleMate) {
                        case MiddleJoinedFingerEnum.Index:
                            actorProp.enumValueIndex = (int) ActorEnum.MiddleJoinedIndex;
                            break;
                        case MiddleJoinedFingerEnum.Ring:
                            actorProp.enumValueIndex = (int) ActorEnum.MiddleJoinedRing;
                            break;
                    }
                    break;
                case MainJoinedFingerEnum.Ring:
                    ringMate = (RingJoinedFingerEnum) tools.insertEnum(ringMate, 0.5f);
                    switch (ringMate) {
                        case RingJoinedFingerEnum.Middle:
                            actorProp.enumValueIndex = (int) ActorEnum.RingJoinedMiddle;
                            break;
                        case RingJoinedFingerEnum.Little:
                            actorProp.enumValueIndex = (int) ActorEnum.RingJoinedLittle;
                            break;
                    }
                    break;
                case MainJoinedFingerEnum.Little:
                    littleMate = (LittleJoinedFingerEnum) tools.insertEnum(littleMate, 0.5f);
                    actorProp.enumValueIndex = (int) ActorEnum.LittleJoinedRing;
                    break;
            }

            tools.endHorizontal();
        }
    }
}


#endif
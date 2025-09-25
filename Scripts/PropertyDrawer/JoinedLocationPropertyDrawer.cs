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

    JoinableFingerEnum mainActor = JoinableFingerEnum.Index;
    JoinedWithIndexEnum indexMate = JoinedWithIndexEnum.Middle;
    JoinedWithMiddleEnum middleMate = JoinedWithMiddleEnum.Index;
    JoinedWithRingEnum ringMate = JoinedWithRingEnum.Middle;
    JoinedWithLittleEnum littleMate = JoinedWithLittleEnum.Ring;

    protected override void initializeProperties(SerializedProperty property) {
        actorProp = property.FindPropertyRelative ("actor");
        oneZoneActorZoneProp = property.FindPropertyRelative ("oneZoneActorZone");
        threeZoneActorZoneProp = property.FindPropertyRelative ("threeZoneActorZone");

        all_actor = Location.getJoinedActorEnum((ActorEnum) actorProp.enumValueIndex);
        oneZoneActor = false;
        threeZoneActor = false;

        switch (all_actor) {
            case JoinedActorEnum.IndexJoinedMiddle:
                mainActor = JoinableFingerEnum.Index;
                indexMate = JoinedWithIndexEnum.Middle;
                threeZoneActor = true;
                break;
            case JoinedActorEnum.MiddleJoinedIndex:
                mainActor = JoinableFingerEnum.Middle;
                middleMate = JoinedWithMiddleEnum.Index;
                threeZoneActor = true;
                break;
            case JoinedActorEnum.MiddleJoinedRing:
                mainActor = JoinableFingerEnum.Middle;
                middleMate = JoinedWithMiddleEnum.Ring;
                threeZoneActor = true;
                break;
            case JoinedActorEnum.RingJoinedMiddle:
                mainActor = JoinableFingerEnum.Ring;
                ringMate = JoinedWithRingEnum.Middle;
                threeZoneActor = true;
                break;
            case JoinedActorEnum.RingJoinedLittle:
                mainActor = JoinableFingerEnum.Ring;
                ringMate = JoinedWithRingEnum.Little;
                threeZoneActor = true;
                break;
            case JoinedActorEnum.LittleJoinedRing:
                mainActor = JoinableFingerEnum.Little;
                littleMate = JoinedWithLittleEnum.Ring;
                threeZoneActor = true;
                break;
            default:
                throw new Exception("Error on JoinedLocationProperyDrawer");
        };

        if (oneZoneActor) {
            oneZoneActorZone = (OneZoneActorZone) oneZoneActorZoneProp.enumValueIndex;
        } else if (twoZoneActor) {
            twoZoneActorZone = (TwoZoneActorZone) twoZoneActorZoneProp.enumValueIndex;
        } else if (threeZoneActor) {
            threeZoneActorZone = (ThreeZoneActorZone) threeZoneActorZoneProp.enumValueIndex;
        }
    }

    protected override void OnConditionnalGUI (SerializedProperty property) {
        if (property.isArray) {
            EditorGUI.PropertyField(tools.getCurrentPosition(), property, true);
        } else {
            initializeLocationPropertyHeight();
            tools.initialize();
            oneZoneActor = false;
            threeZoneActor = false;
            tools.beginHorizontal();
            mainActor = (JoinableFingerEnum) tools.insertEnum(mainActor, 0.5f);
            
            switch (mainActor) {
                case JoinableFingerEnum.Index:
                    indexMate = (JoinedWithIndexEnum) tools.insertEnum(indexMate, 0.5f);
                    switch (indexMate) {
                        case JoinedWithIndexEnum.Middle:
                            actorProp.enumValueIndex = (int) ActorEnum.IndexJoinedMiddle;
                            threeZoneActor = true;
                            break;
                        default :
                            throw new Exception("Error on JoinedLocationPropertyDrawer");
                    }
                    break;
                case JoinableFingerEnum.Middle:
                    middleMate = (JoinedWithMiddleEnum) tools.insertEnum(middleMate, 0.5f);
                    switch (middleMate) {
                        case JoinedWithMiddleEnum.Index:
                            actorProp.enumValueIndex = (int) ActorEnum.MiddleJoinedIndex;
                            threeZoneActor = true;
                            break;
                        case JoinedWithMiddleEnum.Ring:
                            actorProp.enumValueIndex = (int) ActorEnum.MiddleJoinedRing;
                            threeZoneActor = true;
                            break;
                        default :
                            throw new Exception("Error on JoinedLocationPropertyDrawer");
                    }
                    break;
                case JoinableFingerEnum.Ring:
                    ringMate = (JoinedWithRingEnum) tools.insertEnum(ringMate, 0.5f);
                    switch (ringMate) {
                        case JoinedWithRingEnum.Middle:
                            actorProp.enumValueIndex = (int) ActorEnum.RingJoinedMiddle;
                            threeZoneActor = true;
                            break;
                        case JoinedWithRingEnum.Little:
                            actorProp.enumValueIndex = (int) ActorEnum.RingJoinedLittle;
                            threeZoneActor = true;
                            break;
                        default :
                            throw new Exception("Error on JoinedLocationPropertyDrawer");
                    }
                    break;
                case JoinableFingerEnum.Little:
                    littleMate = (JoinedWithLittleEnum) tools.insertEnum(littleMate, 0.5f);
                    switch (littleMate) {
                        case JoinedWithLittleEnum.Ring:
                            actorProp.enumValueIndex = (int) ActorEnum.LittleJoinedRing;
                            threeZoneActor = true;
                            break;
                        default :
                            throw new Exception("Error on JoinedLocationPropertyDrawer");
                    }
                    break;
                default:
                    throw new Exception("Error on JoinedLocationPropertyDrawer");
            };


            tools.endHorizontal();
            
            tools.beginHorizontal();
            if (oneZoneActor) {
                oneZoneActorZoneProp.enumValueIndex = 
                    (int) (OneZoneActorZone) tools.insertEnum(oneZoneActorZone, 1f);
            } else if (threeZoneActor) {
                threeZoneActorZoneProp.enumValueIndex = 
                    (int) (ThreeZoneActorZone) tools.insertEnum(threeZoneActorZone, 1f);
            }
            tools.endHorizontal();
        }
    }
}


#endif
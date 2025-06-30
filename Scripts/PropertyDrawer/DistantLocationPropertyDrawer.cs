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
[CustomPropertyDrawer (typeof(AwayLocation))]
public class AwayLocationPropertyDrawer : LocationPropertyDrawer
{    
    AwayActorEnum all_actor;

    FingerEnum mainActor = FingerEnum.Thumb;
    AwayToThumbEnum thumbMate = AwayToThumbEnum.Index;
    AwayToIndexEnum indexMate = AwayToIndexEnum.Thumb;
    AwayToMiddleEnum middleMate = AwayToMiddleEnum.Thumb;
    AwayToRingEnum ringMate = AwayToRingEnum.Thumb;
    AwayToLittleEnum littleMate = AwayToLittleEnum.Thumb;

    protected override void initializeProperties(SerializedProperty property) {
        actorProp = property.FindPropertyRelative ("actor");
        oneZoneActorZoneProp = property.FindPropertyRelative ("oneZoneActorZone");
        threeZoneActorZoneProp = property.FindPropertyRelative ("threeZoneActorZone");
        fourZoneActorZoneProp = property.FindPropertyRelative ("fourZoneActorZone");

        all_actor = Location.getAwayActorEnum((ActorEnum) actorProp.enumValueIndex);

        oneZoneActor = false;
        threeZoneActor = false;
        fourZoneActor = false;

        switch (all_actor) {
            case AwayActorEnum.ThumbAwayIndex:
                mainActor = FingerEnum.Thumb;
                thumbMate = AwayToThumbEnum.Index;
                fourZoneActor = true;
                break;
            case AwayActorEnum.IndexAwayThumb:
                mainActor = FingerEnum.Index;
                indexMate = AwayToIndexEnum.Thumb;
                fourZoneActor = true;
                break;
            case AwayActorEnum.ThumbAwayMiddle:
                mainActor = FingerEnum.Thumb;
                thumbMate = AwayToThumbEnum.Middle;
                fourZoneActor = true;
                break;
            case AwayActorEnum.MiddleAwayThumb:
                mainActor = FingerEnum.Middle;
                middleMate = AwayToMiddleEnum.Thumb;
                fourZoneActor = true;
                break;
            case AwayActorEnum.ThumbAwayRing:
                mainActor = FingerEnum.Thumb;
                thumbMate = AwayToThumbEnum.Ring;
                fourZoneActor = true;
                break;
            case AwayActorEnum.RingAwayThumb:
                mainActor = FingerEnum.Ring;
                ringMate = AwayToRingEnum.Thumb;
                fourZoneActor = true;
                break;
            case AwayActorEnum.ThumbAwayLittle:
                mainActor = FingerEnum.Thumb;
                thumbMate = AwayToThumbEnum.Little;
                threeZoneActor = true;
                break;
            case AwayActorEnum.LittleAwayThumb:
                mainActor = FingerEnum.Little;
                littleMate = AwayToLittleEnum.Thumb;
                threeZoneActor = true;
                break;
            case AwayActorEnum.IndexAwayMiddle:
                mainActor = FingerEnum.Index;
                indexMate = AwayToIndexEnum.Middle;
                oneZoneActor = true;
                break;
            case AwayActorEnum.MiddleAwayIndex:
                mainActor = FingerEnum.Middle;
                middleMate = AwayToMiddleEnum.Index;
                oneZoneActor = true;
                break;
            case AwayActorEnum.MiddleAwayRing:
                mainActor = FingerEnum.Middle;
                middleMate = AwayToMiddleEnum.Ring;
                oneZoneActor = true;
                break;
            case AwayActorEnum.RingAwayMiddle:
                mainActor = FingerEnum.Ring;
                ringMate = AwayToRingEnum.Middle;
                oneZoneActor = true;
                break;
            case AwayActorEnum.RingAwayLittle:
                mainActor = FingerEnum.Ring;
                ringMate = AwayToRingEnum.Little;
                oneZoneActor = true;
                break;
            case AwayActorEnum.LittleAwayRing:
                mainActor = FingerEnum.Little;
                littleMate = AwayToLittleEnum.Ring;
                oneZoneActor = true;
                break;
            default:
                throw new Exception("Error on AwayLocationPropertyDrawer");
        };

        if (oneZoneActor) {
            oneZoneActorZone = (OneZoneActorZone) oneZoneActorZoneProp.enumValueIndex;
        } else if (twoZoneActor) {
            twoZoneActorZone = (TwoZoneActorZone) twoZoneActorZoneProp.enumValueIndex;
        } else if (threeZoneActor) {
            threeZoneActorZone = (ThreeZoneActorZone) threeZoneActorZoneProp.enumValueIndex;
        } else {
            fourZoneActorZone = (FourZoneActorZone) fourZoneActorZoneProp.enumValueIndex;
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
            fourZoneActor = false;
            tools.beginHorizontal();
            mainActor = (FingerEnum) tools.insertEnum(mainActor, 0.5f);
            
            switch (mainActor) {
                case FingerEnum.Thumb:
                    thumbMate = (AwayToThumbEnum) tools.insertEnum(thumbMate, 0.5f);
                    switch (thumbMate) {
                        case AwayToThumbEnum.Index:
                            actorProp.enumValueIndex = (int) ActorEnum.ThumbAwayIndex;
                            fourZoneActor = true;
                            break;
                        case AwayToThumbEnum.Middle:
                            actorProp.enumValueIndex = (int) ActorEnum.ThumbAwayIndex;
                            fourZoneActor = true;
                            break;
                        case AwayToThumbEnum.Ring:
                            actorProp.enumValueIndex = (int) ActorEnum.ThumbAwayIndex;
                            fourZoneActor = true;
                            break;
                        case AwayToThumbEnum.Little:
                            actorProp.enumValueIndex = (int) ActorEnum.ThumbAwayLittle;
                            threeZoneActor = true;
                            break;
                        default :
                            throw new Exception("Error on AwayLocationPropertyDrawer");
                    }
                    break;
                case FingerEnum.Index:
                    indexMate = (AwayToIndexEnum) tools.insertEnum(indexMate, 0.5f);
                    switch (indexMate) {
                        case AwayToIndexEnum.Thumb:
                            actorProp.enumValueIndex = (int) ActorEnum.IndexAwayThumb;
                            fourZoneActor = true;
                            break;
                        case AwayToIndexEnum.Middle:
                            actorProp.enumValueIndex = (int) ActorEnum.IndexAwayMiddle;
                            oneZoneActor = true;
                            break;
                        default :
                            throw new Exception("Error on AwayLocationPropertyDrawer");
                    }
                    break;
                case FingerEnum.Middle:
                    middleMate = (AwayToMiddleEnum) tools.insertEnum(middleMate, 0.5f);
                    switch (middleMate) {
                        case AwayToMiddleEnum.Thumb:
                            actorProp.enumValueIndex = (int) ActorEnum.MiddleAwayThumb;
                            fourZoneActor = true;
                            break;
                        case AwayToMiddleEnum.Index:
                            actorProp.enumValueIndex = (int) ActorEnum.MiddleAwayIndex;
                            oneZoneActor = true;
                            break;
                        case AwayToMiddleEnum.Ring:
                            actorProp.enumValueIndex = (int) ActorEnum.MiddleAwayRing;
                            oneZoneActor = true;
                            break;
                        default :
                            throw new Exception("Error on AwayLocationPropertyDrawer");
                    }
                    break;
                case FingerEnum.Ring:
                    ringMate = (AwayToRingEnum) tools.insertEnum(ringMate, 0.5f);
                    switch (ringMate) {
                        case AwayToRingEnum.Thumb:
                            actorProp.enumValueIndex = (int) ActorEnum.RingAwayThumb;
                            fourZoneActor = true;
                            break;
                        case AwayToRingEnum.Middle:
                            actorProp.enumValueIndex = (int) ActorEnum.RingAwayMiddle;
                            oneZoneActor = true;
                            break;
                        case AwayToRingEnum.Little:
                            actorProp.enumValueIndex = (int) ActorEnum.RingAwayLittle;
                            oneZoneActor = true;
                            break;
                        default :
                            throw new Exception("Error on AwayLocationPropertyDrawer");
                    }
                    break;
                case FingerEnum.Little:
                    littleMate = (AwayToLittleEnum) tools.insertEnum(littleMate, 0.5f);
                    switch (littleMate) {
                        case AwayToLittleEnum.Thumb:
                            actorProp.enumValueIndex = (int) ActorEnum.LittleAwayThumb;
                            threeZoneActor = true;
                            break;
                        case AwayToLittleEnum.Ring:
                            actorProp.enumValueIndex = (int) ActorEnum.LittleAwayRing;
                            oneZoneActor = true;
                            break;
                        default :
                            throw new Exception("Error on AwayLocationPropertyDrawer");
                    }
                    break;
                default:
                    throw new Exception("Error on AwayLocationPropertyDrawer");
            };


            tools.endHorizontal();
            
            if (oneZoneActor) {
                oneZoneActorZoneProp.enumValueIndex = 
                    (int) (OneZoneActorZone) tools.insertEnum(oneZoneActorZone, 1f);
            } else if (threeZoneActor) {
                threeZoneActorZoneProp.enumValueIndex = 
                    (int) (ThreeZoneActorZone) tools.insertEnum(threeZoneActorZone, 1f);
            } else {
                fourZoneActorZoneProp.enumValueIndex = 
                    (int) (FourZoneActorZone) tools.insertEnum(fourZoneActorZone, 1f);
            }
        }
    }
}

#endif

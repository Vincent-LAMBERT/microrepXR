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
[CustomPropertyDrawer (typeof(UniqueLocation))]
public class UniqueLocationPropertyDrawer : LocationPropertyDrawer
{    
    FingerEnum actor;

    protected override void initializeProperties(SerializedProperty property) {
        actorProp = property.FindPropertyRelative ("actor");
        twoZoneActorZoneProp = property.FindPropertyRelative ("twoZoneActorZone");
        threeZoneActorZoneProp = property.FindPropertyRelative ("threeZoneActorZone");

        actor = Location.getFingerEnum((ActorEnum) actorProp.enumValueIndex);

        twoZoneActor = (actorProp.enumValueIndex == (int) ActorEnum.Little)
                        || (actorProp.enumValueIndex == (int) ActorEnum.Thumb);
        
        if (twoZoneActor) {
            twoZoneActorZone = (TwoZoneActorZone) twoZoneActorZoneProp.enumValueIndex;
        } else {
            threeZoneActorZone = (ThreeZoneActorZone) threeZoneActorZoneProp.enumValueIndex;
        }
    }

    protected override void OnConditionnalGUI (SerializedProperty property) {
        if (property.isArray) {
            EditorGUI.PropertyField(tools.getCurrentPosition(), property, true);
        } else {
            initializeLocationPropertyHeight();
            tools.initialize();
            tools.beginHorizontal();
        
            actorProp.enumValueIndex = (int) (FingerEnum) tools.insertEnum(
                    (FingerEnum) Location.getFingerEnum((ActorEnum) actorProp.enumValueIndex), 0.5f);

            twoZoneActor = (actorProp.enumValueIndex == (int) ActorEnum.Little)
                        || (actorProp.enumValueIndex == (int) ActorEnum.Thumb);

            if (twoZoneActor) {
                twoZoneActorZoneProp.enumValueIndex = 
                    (int) (TwoZoneActorZone) tools.insertEnum(twoZoneActorZone, 0.5f);
            } else {
                threeZoneActorZoneProp.enumValueIndex = 
                    (int) (ThreeZoneActorZone) tools.insertEnum(threeZoneActorZone, 0.5f);
            }
            
            tools.endHorizontal();
        }
    }
}


#endif
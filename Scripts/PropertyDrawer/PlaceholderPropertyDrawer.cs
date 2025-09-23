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
    SerializedProperty placeholderComputeRotationPlaneProp;
    private bool placeholderIsBetweenFingers;
    private bool placeholderComputeRotationPlane;

    SerializedProperty placeholderLocationProp;
    SerializedProperty actorEnumProp;
    SerializedProperty oneZoneActorZoneProp;
    SerializedProperty twoZoneActorZoneProp;
    SerializedProperty threeZoneActorZoneProp;
    private ActorEnum actorEnum;
    private OneZoneActorZone oneZoneActorZone;
    private TwoZoneActorZone twoZoneActorZone;
    private ThreeZoneActorZone threeZoneActorZone;

    SerializedProperty activateOnIfBehaviorOneProp;
    private bool activateOnIfBehaviorOne;
    SerializedProperty onIfBehaviorOneProp;
    SerializedProperty onIfBehaviorLocationOneProp;
    SerializedProperty actorEnumPropBehaviorOne;
    private ActorEnum actorEnumBehaviorOne;

    SerializedProperty activateOnIfBehaviorTwoProp;
    private bool activateOnIfBehaviorTwo;
    SerializedProperty onIfBehaviorTwoProp;
    SerializedProperty onIfBehaviorLocationTwoProp;
    SerializedProperty actorEnumPropBehaviorTwo;
    private ActorEnum actorEnumBehaviorTwo;

    SerializedProperty activateOnIfBehaviorThreeProp;
    private bool activateOnIfBehaviorThree;
    SerializedProperty onIfBehaviorThreeProp;
    SerializedProperty onIfBehaviorLocationThreeProp;
    SerializedProperty actorEnumPropBehaviorThree;
    private ActorEnum actorEnumBehaviorThree;

    protected override void initializeProperties(SerializedProperty property) {
        placeholderIsBetweenFingersProp = property.FindPropertyRelative ("placeholderIsBetweenFingers");
        placeholderComputeRotationPlaneProp = property.FindPropertyRelative ("placeholderComputeRotationPlane");
        placeholderIsBetweenFingers = placeholderIsBetweenFingersProp.boolValue;
        placeholderComputeRotationPlane = placeholderComputeRotationPlaneProp.boolValue;

        placeholderLocationProp = property.FindPropertyRelative ("placeholderLocation");
        actorEnumProp = placeholderLocationProp.FindPropertyRelative("actor");
        oneZoneActorZoneProp = placeholderLocationProp.FindPropertyRelative("oneZoneActorZone");
        twoZoneActorZoneProp = placeholderLocationProp.FindPropertyRelative("twoZoneActorZone");
        threeZoneActorZoneProp = placeholderLocationProp.FindPropertyRelative("threeZoneActorZone");
        actorEnum = (ActorEnum) actorEnumProp.enumValueIndex;
        oneZoneActorZone = (OneZoneActorZone) oneZoneActorZoneProp.enumValueIndex;
        twoZoneActorZone = (TwoZoneActorZone) twoZoneActorZoneProp.enumValueIndex;
        threeZoneActorZone = (ThreeZoneActorZone) threeZoneActorZoneProp.enumValueIndex;

        activateOnIfBehaviorOneProp = property.FindPropertyRelative ("activateOnIfBehaviorOne");
        activateOnIfBehaviorOne = activateOnIfBehaviorOneProp.boolValue;
        onIfBehaviorOneProp = property.FindPropertyRelative ("onIfBehaviorOne");
        onIfBehaviorLocationOneProp = property.FindPropertyRelative ("onIfBehaviorLocationOne");
        actorEnumPropBehaviorOne = onIfBehaviorLocationOneProp.FindPropertyRelative("actor");
        actorEnumBehaviorOne = (ActorEnum) actorEnumPropBehaviorOne.enumValueIndex;

        activateOnIfBehaviorTwoProp = property.FindPropertyRelative ("activateOnIfBehaviorTwo");
        activateOnIfBehaviorTwo = activateOnIfBehaviorTwoProp.boolValue;
        onIfBehaviorTwoProp = property.FindPropertyRelative ("onIfBehaviorTwo");
        onIfBehaviorLocationTwoProp = property.FindPropertyRelative ("onIfBehaviorLocationTwo");
        actorEnumPropBehaviorTwo = onIfBehaviorLocationTwoProp.FindPropertyRelative("actor");
        actorEnumBehaviorTwo = (ActorEnum) actorEnumPropBehaviorTwo.enumValueIndex;

        activateOnIfBehaviorThreeProp = property.FindPropertyRelative ("activateOnIfBehaviorThree");
        activateOnIfBehaviorThree = activateOnIfBehaviorThreeProp.boolValue;
        onIfBehaviorThreeProp = property.FindPropertyRelative ("onIfBehaviorThree");
        onIfBehaviorLocationThreeProp = property.FindPropertyRelative ("onIfBehaviorLocationThree");
        actorEnumPropBehaviorThree = onIfBehaviorLocationThreeProp.FindPropertyRelative("actor");
        actorEnumBehaviorThree = (ActorEnum) actorEnumPropBehaviorThree.enumValueIndex;
    }

    protected override void OnConditionnalGUI (SerializedProperty property) {
        if (property.isArray) {
            EditorGUI.PropertyField(tools.getCurrentPosition(), property, false);
        } else {
            initializePropertyHeight(placeholderIsBetweenFingersProp, placeholderIsBetweenFingersProp, placeholderIsBetweenFingersProp, placeholderIsBetweenFingersProp, placeholderIsBetweenFingersProp, placeholderIsBetweenFingersProp);
            tools.initialize();
            tools.beginHorizontal();
            tools.insertLabel("Place", 50);
            
            OnBetweenEnum onBetween = placeholderIsBetweenFingers ? OnBetweenEnum.Between : OnBetweenEnum.On;
            placeholderIsBetweenFingersProp.boolValue = ((OnBetweenEnum) tools.insertEnum(onBetween, 0.15f) == OnBetweenEnum.Between) ? true : false;

            if (!placeholderIsBetweenFingersProp.boolValue) {
                FingerEnum fingerEnum = Location.getFingerEnum(actorEnum);
                fingerEnum = (FingerEnum) tools.insertEnum(fingerEnum, 0.15f);
                (oneZoneActorZone, twoZoneActorZone, threeZoneActorZone) = insertZone(actorEnum, oneZoneActorZone, twoZoneActorZone, threeZoneActorZone);
                actorEnumProp.enumValueIndex = (int) Location.getActorEnum(fingerEnum);
                oneZoneActorZoneProp.enumValueIndex = (int) oneZoneActorZone;
                twoZoneActorZoneProp.enumValueIndex = (int) twoZoneActorZone;
                threeZoneActorZoneProp.enumValueIndex = (int) threeZoneActorZone;
                tools.endHorizontal();
            } else {
                if (placeholderComputeRotationPlaneProp.boolValue) {
                    FingerEnum fingerEnumB1;
                    FingerEnum fingerEnumB2;
                    (fingerEnumB1, fingerEnumB2) = Location.getAwayFingers(actorEnum);
                    fingerEnumB1 = (FingerEnum) tools.insertEnum(fingerEnumB1, 0.15f);
                    tools.insertLabel("AND", 40);
                    fingerEnumB2 = (FingerEnum) tools.insertEnum(fingerEnumB2, 0.15f);
                    (oneZoneActorZone, twoZoneActorZone, threeZoneActorZone) = insertZone(actorEnum, oneZoneActorZone, twoZoneActorZone, threeZoneActorZone);
                    actorEnumProp.enumValueIndex = (int) Location.getAwayActorEnum(fingerEnumB1, fingerEnumB2);
                } else {
                    JoinableFingerEnum fingerEnumB1;
                    JoinableFingerEnum fingerEnumB2;
                    (fingerEnumB1, fingerEnumB2) = Location.getJoinedFingers(actorEnum);
                    fingerEnumB1 = (JoinableFingerEnum) tools.insertEnum(fingerEnumB1, 0.15f);
                    tools.insertLabel("AND", 40);
                    fingerEnumB2 = (JoinableFingerEnum) tools.insertEnum(fingerEnumB2, 0.15f);
                    (oneZoneActorZone, twoZoneActorZone, threeZoneActorZone) = insertZone(actorEnum, oneZoneActorZone, twoZoneActorZone, threeZoneActorZone);
                    actorEnumProp.enumValueIndex = (int) Location.getJoinedActorEnum(fingerEnumB1, fingerEnumB2);
                }
                oneZoneActorZoneProp.enumValueIndex = (int) oneZoneActorZone;
                twoZoneActorZoneProp.enumValueIndex = (int) twoZoneActorZone;
                threeZoneActorZoneProp.enumValueIndex = (int) threeZoneActorZone;
                tools.endHorizontal();
                tools.beginHorizontal();
                placeholderComputeRotationPlaneProp.boolValue = tools.insertToggle(placeholderComputeRotationPlaneProp.boolValue, 0.03f);
                tools.insertLabel("Compute rotation plane (intended for far away fingers)", 350);
                tools.endHorizontal();
            }

            insertOnIfBehavior(activateOnIfBehaviorOneProp, onIfBehaviorOneProp, actorEnumPropBehaviorOne, activateOnIfBehaviorOne, actorEnumBehaviorOne, "Visible if :");
            insertOnIfBehavior(activateOnIfBehaviorTwoProp, onIfBehaviorTwoProp, actorEnumPropBehaviorTwo, activateOnIfBehaviorTwo, actorEnumBehaviorTwo);
            insertOnIfBehavior(activateOnIfBehaviorThreeProp, onIfBehaviorThreeProp, actorEnumPropBehaviorThree, activateOnIfBehaviorThree, actorEnumBehaviorThree);
            tools.endHorizontal();
        }
    }

    public Tuple<OneZoneActorZone, TwoZoneActorZone, ThreeZoneActorZone> insertZone(ActorEnum actorEnum, OneZoneActorZone oneZoneActorZone, TwoZoneActorZone twoZoneActorZone, ThreeZoneActorZone threeZoneActorZone) {
        if (OneZoneActorEnum.TryParse(actorEnum.ToString(), out OneZoneActorEnum oneZoneActorB1)) {
            oneZoneActorZone = (OneZoneActorZone) tools.insertEnum(oneZoneActorZone, 0.15f);
        } else if (TwoZoneActorEnum.TryParse(actorEnum.ToString(), out TwoZoneActorEnum twoZoneActorB1)) {
            twoZoneActorZone = (TwoZoneActorZone) tools.insertEnum(twoZoneActorZone, 0.15f);
        } else if (ThreeZoneActorEnum.TryParse(actorEnum.ToString(), out ThreeZoneActorEnum threeZoneActorB1)) {
            threeZoneActorZone = (ThreeZoneActorZone) tools.insertEnum(threeZoneActorZone, 0.15f);
        } else {
            UnityEngine.Debug.LogError("Error: not a finger: " + actorEnum.ToString());
            tools.insertLabel("Error: not a finger", 100);
        }
        return new Tuple<OneZoneActorZone, TwoZoneActorZone, ThreeZoneActorZone>(oneZoneActorZone, twoZoneActorZone, threeZoneActorZone);
    }

    public void insertOnIfBehavior(SerializedProperty activateOnIfBehaviorProp, SerializedProperty onIfBehaviorProp, SerializedProperty actorEnumPropBehavior, bool activateOnIfBehavior, ActorEnum actorEnumBehavior, string textIf="") {
        tools.beginHorizontal();
        tools.insertLabel(textIf, 70);
        if (activateOnIfBehavior) {
            switch ((PlaceholderOnIfBehavior) onIfBehaviorProp.enumValueIndex) {
                case PlaceholderOnIfBehavior.FarAway:
                    FingerEnum fingerEnumb1;
                    FingerEnum fingerEnumb2;
                    (fingerEnumb1, fingerEnumb2) = Location.getAwayFingers(actorEnumBehavior);
                    fingerEnumb1 = (FingerEnum) tools.insertEnum(fingerEnumb1, 0.15f);
                    tools.insertLabel("AND", 40);
                    fingerEnumb2 = insertSpecificEnumAwayFingers(fingerEnumb1, fingerEnumb2);
                    actorEnumPropBehavior.enumValueIndex = (int) Location.getAwayActorEnum(fingerEnumb1, fingerEnumb2);
                    break;
                case PlaceholderOnIfBehavior.Joined:
                    JoinableFingerEnum fingerEnumbJ1;
                    JoinableFingerEnum fingerEnumbJ2;
                    (fingerEnumbJ1, fingerEnumbJ2) = Location.getJoinedFingers(actorEnumBehavior);
                    fingerEnumbJ1 = (JoinableFingerEnum) tools.insertEnum(fingerEnumbJ1, 0.15f);
                    tools.insertLabel("AND", 40);
                    fingerEnumbJ2 = insertSpecificEnumJoinedFingers(fingerEnumbJ1, fingerEnumbJ2);
                    actorEnumPropBehavior.enumValueIndex = (int) Location.getJoinedActorEnum(fingerEnumbJ1, fingerEnumbJ2);
                    break;
                case PlaceholderOnIfBehavior.NotJoined:
                    JoinableFingerEnum fingerEnumbJ3;
                    JoinableFingerEnum fingerEnumbJ4;
                    (fingerEnumbJ3, fingerEnumbJ4) = Location.getJoinedFingers(actorEnumBehavior);
                    fingerEnumbJ3 = (JoinableFingerEnum) tools.insertEnum(fingerEnumbJ3, 0.15f);
                    tools.insertLabel("AND", 40);
                    fingerEnumbJ4 = insertSpecificEnumJoinedFingers(fingerEnumbJ3, fingerEnumbJ4);
                    actorEnumPropBehavior.enumValueIndex = (int) Location.getJoinedActorEnum(fingerEnumbJ3, fingerEnumbJ4);
                    break;
            }
            onIfBehaviorProp.enumValueIndex = (int) (PlaceholderOnIfBehavior) tools.insertEnum((PlaceholderOnIfBehavior) onIfBehaviorProp.enumValueIndex, 0.15f);
        }
        activateOnIfBehaviorProp.boolValue = tools.insertToggle(activateOnIfBehaviorProp.boolValue, 0.03f);
        if (activateOnIfBehaviorProp.boolValue) {
            tools.insertLabel("delete", 50);
        } else {
            tools.insertLabel("add if statement", 110);
        }
        tools.endHorizontal();
    }

    public FingerEnum insertSpecificEnumAwayFingers(FingerEnum fingerEnumb1, FingerEnum fingerEnumb2) {
        switch (fingerEnumb1) {
            case FingerEnum.Thumb:
                AwayToThumbEnum awayToThumb = Location.getAwayToThumbEnum(Location.getActorEnum(fingerEnumb2));
                awayToThumb = (AwayToThumbEnum) tools.insertEnum(awayToThumb, 0.15f);
                fingerEnumb2 = Location.getFingerEnum(Location.getActorEnum(awayToThumb));
                break;
            case FingerEnum.Index:
                AwayToIndexEnum awayToIndex = Location.getAwayToIndexEnum(Location.getActorEnum(fingerEnumb2));
                awayToIndex = (AwayToIndexEnum) tools.insertEnum(awayToIndex, 0.15f);
                fingerEnumb2 = Location.getFingerEnum(Location.getActorEnum(awayToIndex));
                break;
            case FingerEnum.Middle:
                AwayToMiddleEnum awayToMiddle = Location.getAwayToMiddleEnum(Location.getActorEnum(fingerEnumb2));
                awayToMiddle = (AwayToMiddleEnum) tools.insertEnum(awayToMiddle, 0.15f);
                fingerEnumb2 = Location.getFingerEnum(Location.getActorEnum(awayToMiddle));
                break;
            case FingerEnum.Ring:
                AwayToRingEnum awayToRing = Location.getAwayToRingEnum(Location.getActorEnum(fingerEnumb2));
                awayToRing = (AwayToRingEnum) tools.insertEnum(awayToRing, 0.15f);
                fingerEnumb2 = Location.getFingerEnum(Location.getActorEnum(awayToRing));
                break;
            case FingerEnum.Little:
                AwayToLittleEnum awayToLittle = Location.getAwayToLittleEnum(Location.getActorEnum(fingerEnumb2));
                awayToLittle = (AwayToLittleEnum) tools.insertEnum(awayToLittle, 0.15f);
                fingerEnumb2 = Location.getFingerEnum(Location.getActorEnum(awayToLittle));
                break;
            default:
                UnityEngine.Debug.LogError("Error: not a finger: " + fingerEnumb2.ToString());
                tools.insertLabel("Error: not a finger", 100);
                break;
        }
        return fingerEnumb2;
    }

    public JoinableFingerEnum insertSpecificEnumJoinedFingers(JoinableFingerEnum fingerEnumbJ1, JoinableFingerEnum fingerEnumbJ2) {
        switch (fingerEnumbJ1) {
            case JoinableFingerEnum.Index:
                JoinedWithIndexEnum joinedToIndex = Location.getJoinedWithIndexEnum(Location.getActorEnum(fingerEnumbJ2));
                joinedToIndex = (JoinedWithIndexEnum) tools.insertEnum(joinedToIndex, 0.15f);
                fingerEnumbJ2 = Location.getJoinableFingerEnum(Location.getActorEnum(joinedToIndex));
                break;
            case JoinableFingerEnum.Middle:
                JoinedWithMiddleEnum joinedToMiddle = Location.getJoinedWithMiddleEnum(Location.getActorEnum(fingerEnumbJ2));
                joinedToMiddle = (JoinedWithMiddleEnum) tools.insertEnum(joinedToMiddle, 0.15f);
                fingerEnumbJ2 = Location.getJoinableFingerEnum(Location.getActorEnum(joinedToMiddle));
                break;
            case JoinableFingerEnum.Ring:
                JoinedWithRingEnum joinedToRing = Location.getJoinedWithRingEnum(Location.getActorEnum(fingerEnumbJ2));
                joinedToRing = (JoinedWithRingEnum) tools.insertEnum(joinedToRing, 0.15f);
                fingerEnumbJ2 = Location.getJoinableFingerEnum(Location.getActorEnum(joinedToRing));
                break;
            case JoinableFingerEnum.Little:
                JoinedWithLittleEnum joinedToLittle = Location.getJoinedWithLittleEnum(Location.getActorEnum(fingerEnumbJ2));
                joinedToLittle = (JoinedWithLittleEnum) tools.insertEnum(joinedToLittle, 0.15f);
                fingerEnumbJ2 = Location.getJoinableFingerEnum(Location.getActorEnum(joinedToLittle));
                break;
            default:
                UnityEngine.Debug.LogError("Error: not a finger: " + fingerEnumbJ2.ToString());
                tools.insertLabel("Error: not a finger", 100);
                break;
        }
        return fingerEnumbJ2;
    }
}

public enum OnBetweenEnum {
    On,
    Between
}

#endif

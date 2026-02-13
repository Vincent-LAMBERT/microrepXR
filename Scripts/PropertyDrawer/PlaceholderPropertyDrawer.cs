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

    SerializedProperty activateOnIfOneProp;
    private bool activateOnIfOne;
    SerializedProperty ifTypeOneProp;
    SerializedProperty handOrientationOneProp;
    SerializedProperty fingerOnIfBehaviorOneProp;
    SerializedProperty placeholderOnIfBehaviorOneProp;
    SerializedProperty placeholderOnIfBehaviorLocationOneProp;
    SerializedProperty actorEnumPropBehaviorOne;
    private ActorEnum actorEnumBehaviorOne;

    SerializedProperty activateOnIfTwoProp;
    private bool activateOnIfTwo;
    SerializedProperty ifTypeTwoProp;
    SerializedProperty handOrientationTwoProp;
    SerializedProperty fingerOnIfBehaviorTwoProp;
    SerializedProperty placeholderOnIfBehaviorTwoProp;
    SerializedProperty placeholderOnIfBehaviorLocationTwoProp;
    SerializedProperty actorEnumPropBehaviorTwo;
    private ActorEnum actorEnumBehaviorTwo;

    SerializedProperty activateOnIfThreeProp;
    private bool activateOnIfThree;
    SerializedProperty ifTypeThreeProp;
    SerializedProperty handOrientationThreeProp;
    SerializedProperty fingerOnIfBehaviorThreeProp;
    SerializedProperty placeholderOnIfBehaviorThreeProp;
    SerializedProperty placeholderOnIfBehaviorLocationThreeProp;
    SerializedProperty actorEnumPropBehaviorThree;
    private ActorEnum actorEnumBehaviorThree;

    SerializedProperty activateOnIfFourProp;
    private bool activateOnIfFour;
    SerializedProperty ifTypeFourProp;
    SerializedProperty handOrientationFourProp;
    SerializedProperty fingerOnIfBehaviorFourProp;
    SerializedProperty placeholderOnIfBehaviorFourProp;
    SerializedProperty placeholderOnIfBehaviorLocationFourProp;
    SerializedProperty actorEnumPropBehaviorFour;
    private ActorEnum actorEnumBehaviorFour;

    SerializedProperty activateOnIfFiveProp;
    private bool activateOnIfFive;
    SerializedProperty ifTypeFiveProp;
    SerializedProperty handOrientationFiveProp;
    SerializedProperty fingerOnIfBehaviorFiveProp;
    SerializedProperty placeholderOnIfBehaviorFiveProp;
    SerializedProperty placeholderOnIfBehaviorLocationFiveProp;
    SerializedProperty actorEnumPropBehaviorFive;
    private ActorEnum actorEnumBehaviorFive;

    SerializedProperty activateOnIfSixProp;
    private bool activateOnIfSix;
    SerializedProperty ifTypeSixProp;
    SerializedProperty handOrientationSixProp;
    SerializedProperty fingerOnIfBehaviorSixProp;
    SerializedProperty placeholderOnIfBehaviorSixProp;
    SerializedProperty placeholderOnIfBehaviorLocationSixProp;
    SerializedProperty actorEnumPropBehaviorSix;
    private ActorEnum actorEnumBehaviorSix;

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

        activateOnIfOneProp = property.FindPropertyRelative ("activateOnIfOne");
        activateOnIfOne = activateOnIfOneProp.boolValue;
        ifTypeOneProp = property.FindPropertyRelative ("ifTypeOne");
        handOrientationOneProp = property.FindPropertyRelative ("handOrientationOne");
        fingerOnIfBehaviorOneProp = property.FindPropertyRelative ("fingerOnIfBehaviorOne");
        placeholderOnIfBehaviorOneProp = property.FindPropertyRelative ("placeholderOnIfBehaviorOne");
        placeholderOnIfBehaviorLocationOneProp = property.FindPropertyRelative ("placeholderOnIfBehaviorLocationOne");
        actorEnumPropBehaviorOne = placeholderOnIfBehaviorLocationOneProp.FindPropertyRelative("actor");
        actorEnumBehaviorOne = (ActorEnum) actorEnumPropBehaviorOne.enumValueIndex;

        activateOnIfTwoProp = property.FindPropertyRelative ("activateOnIfTwo");
        activateOnIfTwo = activateOnIfTwoProp.boolValue;
        ifTypeTwoProp = property.FindPropertyRelative ("ifTypeTwo");
        handOrientationTwoProp = property.FindPropertyRelative ("handOrientationTwo");
        fingerOnIfBehaviorTwoProp = property.FindPropertyRelative ("fingerOnIfBehaviorTwo");
        placeholderOnIfBehaviorTwoProp = property.FindPropertyRelative ("placeholderOnIfBehaviorTwo");
        placeholderOnIfBehaviorLocationTwoProp = property.FindPropertyRelative ("placeholderOnIfBehaviorLocationTwo");
        actorEnumPropBehaviorTwo = placeholderOnIfBehaviorLocationTwoProp.FindPropertyRelative("actor");
        actorEnumBehaviorTwo = (ActorEnum) actorEnumPropBehaviorTwo.enumValueIndex;

        activateOnIfThreeProp = property.FindPropertyRelative ("activateOnIfThree");
        activateOnIfThree = activateOnIfThreeProp.boolValue;
        ifTypeThreeProp = property.FindPropertyRelative ("ifTypeThree");
        handOrientationThreeProp = property.FindPropertyRelative ("handOrientationThree");
        fingerOnIfBehaviorThreeProp = property.FindPropertyRelative ("fingerOnIfBehaviorThree");
        placeholderOnIfBehaviorThreeProp = property.FindPropertyRelative ("placeholderOnIfBehaviorThree");
        placeholderOnIfBehaviorLocationThreeProp = property.FindPropertyRelative ("placeholderOnIfBehaviorLocationThree");
        actorEnumPropBehaviorThree = placeholderOnIfBehaviorLocationThreeProp.FindPropertyRelative("actor");
        actorEnumBehaviorThree = (ActorEnum) actorEnumPropBehaviorThree.enumValueIndex;

        activateOnIfFourProp = property.FindPropertyRelative ("activateOnIfFour");
        activateOnIfFour = activateOnIfFourProp.boolValue;
        ifTypeFourProp = property.FindPropertyRelative ("ifTypeFour");
        handOrientationFourProp = property.FindPropertyRelative ("handOrientationFour");
        fingerOnIfBehaviorFourProp = property.FindPropertyRelative ("fingerOnIfBehaviorFour");
        placeholderOnIfBehaviorFourProp = property.FindPropertyRelative ("placeholderOnIfBehaviorFour");
        placeholderOnIfBehaviorLocationFourProp = property.FindPropertyRelative ("placeholderOnIfBehaviorLocationFour");
        actorEnumPropBehaviorFour = placeholderOnIfBehaviorLocationFourProp.FindPropertyRelative("actor");
        actorEnumBehaviorFour = (ActorEnum) actorEnumPropBehaviorFour.enumValueIndex;

        activateOnIfFiveProp = property.FindPropertyRelative ("activateOnIfFive");
        activateOnIfFive = activateOnIfFiveProp.boolValue;
        ifTypeFiveProp = property.FindPropertyRelative ("ifTypeFive");
        handOrientationFiveProp = property.FindPropertyRelative ("handOrientationFive");
        fingerOnIfBehaviorFiveProp = property.FindPropertyRelative ("fingerOnIfBehaviorFive");
        placeholderOnIfBehaviorFiveProp = property.FindPropertyRelative ("placeholderOnIfBehaviorFive");
        placeholderOnIfBehaviorLocationFiveProp = property.FindPropertyRelative ("placeholderOnIfBehaviorLocationFive");
        actorEnumPropBehaviorFive = placeholderOnIfBehaviorLocationFiveProp.FindPropertyRelative("actor");
        actorEnumBehaviorFive = (ActorEnum) actorEnumPropBehaviorFive.enumValueIndex;

        activateOnIfSixProp = property.FindPropertyRelative ("activateOnIfSix");
        activateOnIfSix = activateOnIfSixProp.boolValue;
        ifTypeSixProp = property.FindPropertyRelative ("ifTypeSix");
        handOrientationSixProp = property.FindPropertyRelative ("handOrientationSix");
        fingerOnIfBehaviorSixProp = property.FindPropertyRelative ("fingerOnIfBehaviorSix");
        placeholderOnIfBehaviorSixProp = property.FindPropertyRelative ("placeholderOnIfBehaviorSix");
        placeholderOnIfBehaviorLocationSixProp = property.FindPropertyRelative ("placeholderOnIfBehaviorLocationSix");
        actorEnumPropBehaviorSix = placeholderOnIfBehaviorLocationSixProp.FindPropertyRelative("actor");
        actorEnumBehaviorSix = (ActorEnum) actorEnumPropBehaviorSix.enumValueIndex;
    }

    protected override void OnConditionnalGUI (SerializedProperty property) {
        if (property.isArray) {
            EditorGUI.PropertyField(tools.getCurrentPosition(), property, false);
        } else {
            initializePropertyHeight(placeholderIsBetweenFingersProp, placeholderIsBetweenFingersProp, placeholderIsBetweenFingersProp, placeholderIsBetweenFingersProp, placeholderIsBetweenFingersProp, placeholderIsBetweenFingersProp, placeholderIsBetweenFingersProp, placeholderIsBetweenFingersProp, placeholderIsBetweenFingersProp);
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
                    // fingerEnumB2 = insertSpecificEnumJoinedFingers(fingerEnumB1, fingerEnumB2);
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

            insertOnIfBehavior(activateOnIfOneProp, ifTypeOneProp, handOrientationOneProp, placeholderOnIfBehaviorOneProp, fingerOnIfBehaviorOneProp, actorEnumPropBehaviorOne, activateOnIfOne, actorEnumBehaviorOne, "Visible if :");
            insertOnIfBehavior(activateOnIfTwoProp, ifTypeTwoProp, handOrientationTwoProp, placeholderOnIfBehaviorTwoProp, fingerOnIfBehaviorTwoProp, actorEnumPropBehaviorTwo, activateOnIfTwo, actorEnumBehaviorTwo);
            insertOnIfBehavior(activateOnIfThreeProp, ifTypeThreeProp, handOrientationThreeProp, placeholderOnIfBehaviorThreeProp, fingerOnIfBehaviorThreeProp, actorEnumPropBehaviorThree, activateOnIfThree, actorEnumBehaviorThree);
            insertOnIfBehavior(activateOnIfFourProp, ifTypeFourProp, handOrientationFourProp, placeholderOnIfBehaviorFourProp, fingerOnIfBehaviorFourProp, actorEnumPropBehaviorFour, activateOnIfFour, actorEnumBehaviorFour);
            insertOnIfBehavior(activateOnIfFiveProp, ifTypeFiveProp, handOrientationFiveProp, placeholderOnIfBehaviorFiveProp, fingerOnIfBehaviorFiveProp, actorEnumPropBehaviorFive, activateOnIfFive, actorEnumBehaviorFive);
            insertOnIfBehavior(activateOnIfSixProp, ifTypeSixProp, handOrientationSixProp, placeholderOnIfBehaviorSixProp, fingerOnIfBehaviorSixProp, actorEnumPropBehaviorSix, activateOnIfSix, actorEnumBehaviorSix);
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

    public void insertOnIfBehavior(SerializedProperty activateOnIfProp, SerializedProperty ifTypeProp, SerializedProperty handOrientationProp, SerializedProperty placeholderOnIfBehaviorProp, SerializedProperty fingerOnIfBehaviorProp, SerializedProperty actorEnumPropBehavior, bool activateOnIf, ActorEnum actorEnumBehavior, string textIf="") {
        tools.beginHorizontal();
        tools.insertLabel(textIf, 70);
        if (activateOnIf) {
            ifTypeProp.enumValueIndex = (int) (IfType) tools.insertEnum((IfType) ifTypeProp.enumValueIndex, 0.15f);
            switch ((IfType) ifTypeProp.enumValueIndex) {
                case IfType.Fingers:
                    insertOnIfFingers(placeholderOnIfBehaviorProp, actorEnumPropBehavior, actorEnumBehavior);
                    break;
                case IfType.Finger:
                    insertOnIfFinger(fingerOnIfBehaviorProp, actorEnumPropBehavior, actorEnumBehavior);
                    break;
                case IfType.Hand:
                    insertOnIfHand(handOrientationProp);
                    break;
                // case IfType.Thumb:
                //     insertOnIfThumb();
                //     break;
            }
        }
        activateOnIfProp.boolValue = tools.insertToggle(activateOnIfProp.boolValue, 0.03f);
        if (activateOnIfProp.boolValue) {
            tools.insertLabel("delete", 50);
        } else {
            tools.insertLabel("add if statement", 110);
        }
        tools.endHorizontal();
    }

    public void insertOnIfFingers(SerializedProperty placeholderOnIfBehaviorProp, SerializedProperty actorEnumPropBehavior, ActorEnum actorEnumBehavior) {
        switch ((PlaceholderOnIfBehavior) placeholderOnIfBehaviorProp.enumValueIndex) {
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
        placeholderOnIfBehaviorProp.enumValueIndex = (int) (PlaceholderOnIfBehavior) tools.insertEnum((PlaceholderOnIfBehavior) placeholderOnIfBehaviorProp.enumValueIndex, 0.15f);
    }

    public void insertOnIfFinger(SerializedProperty fingerOnIfBehaviorProp, SerializedProperty actorEnumPropBehavior, ActorEnum actorEnumBehavior) {
        FingerEnum fingerEnum = Location.getFingerEnum(actorEnumBehavior);
        fingerEnum = (FingerEnum) tools.insertEnum(fingerEnum, 0.15f);
        actorEnumPropBehavior.enumValueIndex = (int) Location.getActorEnum(fingerEnum);
        fingerOnIfBehaviorProp.enumValueIndex = (int) (FingerOnIfBehavior) tools.insertEnum((FingerOnIfBehavior) fingerOnIfBehaviorProp.enumValueIndex, 0.15f);
    }

    public void insertOnIfHand(SerializedProperty handOrientationProp) {
        tools.insertLabel("FACES", 60);
        handOrientationProp.enumValueIndex = (int) (HandOrientation) tools.insertEnum((HandOrientation) handOrientationProp.enumValueIndex, 0.15f);
    }

    // public void insertOnIfThumb() {
    //     tools.insertLabel("CLOSE TO THE FINGERS", 155);
    // }

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

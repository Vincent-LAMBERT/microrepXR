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
using MixedReality.Toolkit;
// using UnityEngine.XR.Hands; // new for Handedness and XRHandJointID


namespace Microgestures
{
    [Serializable, AddComponentMenu("Placeholder", 0)]
    public class Placeholder
    {
        public bool placeholderIsBetweenFingers = false;
        public bool placeholderComputeRotationPlane = false;
        public Location placeholderLocation;

        public bool activateOnIfOne = false;
        public IfType ifTypeOne;
        public HandOrientation handOrientationOne;
        public FingerOnIfBehavior fingerOnIfBehaviorOne;
        public Location placeholderOnIfBehaviorLocationOne;
        public PlaceholderOnIfBehavior placeholderOnIfBehaviorOne;

        public bool activateOnIfTwo = false;
        public IfType ifTypeTwo;
        public HandOrientation handOrientationTwo;
        public FingerOnIfBehavior fingerOnIfBehaviorTwo;
        public Location placeholderOnIfBehaviorLocationTwo;
        public PlaceholderOnIfBehavior placeholderOnIfBehaviorTwo;

        public bool activateOnIfThree = false;
        public IfType ifTypeThree;
        public HandOrientation handOrientationThree;
        public FingerOnIfBehavior fingerOnIfBehaviorThree;
        public Location placeholderOnIfBehaviorLocationThree;
        public PlaceholderOnIfBehavior placeholderOnIfBehaviorThree;

        public bool activateOnIfFour = false;
        public IfType ifTypeFour;
        public HandOrientation handOrientationFour;
        public FingerOnIfBehavior fingerOnIfBehaviorFour;
        public Location placeholderOnIfBehaviorLocationFour;
        public PlaceholderOnIfBehavior placeholderOnIfBehaviorFour;

        public bool activateOnIfFive = false;
        public IfType ifTypeFive;
        public HandOrientation handOrientationFive;
        public FingerOnIfBehavior fingerOnIfBehaviorFive;

        public Location placeholderOnIfBehaviorLocationFive;
        public PlaceholderOnIfBehavior placeholderOnIfBehaviorFive;
        public bool activateOnIfSix = false;
        public IfType ifTypeSix;
        public HandOrientation handOrientationSix;
        public FingerOnIfBehavior fingerOnIfBehaviorSix;
        public Location placeholderOnIfBehaviorLocationSix;
        public PlaceholderOnIfBehavior placeholderOnIfBehaviorSix;

        public Placeholder()
        {
        }

        public Location getLocation()
        {
            return placeholderLocation;
        }

        public Stack<Behavior> getBehaviors(Handedness handedness)
        {
            Stack<Behavior> behaviors = new Stack<Behavior>();
            
            foreach (var (activate, ifType, handOrientation, location, placeholderOnIfBehavior, fingerOnIfBehavior) in new List<(bool, IfType, HandOrientation, Location, PlaceholderOnIfBehavior, FingerOnIfBehavior)>{
                (activateOnIfOne, ifTypeOne, handOrientationOne, placeholderOnIfBehaviorLocationOne, placeholderOnIfBehaviorOne, fingerOnIfBehaviorOne),
                (activateOnIfTwo, ifTypeTwo, handOrientationTwo, placeholderOnIfBehaviorLocationTwo, placeholderOnIfBehaviorTwo, fingerOnIfBehaviorTwo),
                (activateOnIfThree, ifTypeThree, handOrientationThree, placeholderOnIfBehaviorLocationThree, placeholderOnIfBehaviorThree, fingerOnIfBehaviorThree),
                (activateOnIfFour, ifTypeFour, handOrientationFour, placeholderOnIfBehaviorLocationFour, placeholderOnIfBehaviorFour, fingerOnIfBehaviorFour),
                (activateOnIfFive, ifTypeFive, handOrientationFive, placeholderOnIfBehaviorLocationFive, placeholderOnIfBehaviorFive, fingerOnIfBehaviorFive),
                (activateOnIfSix, ifTypeSix, handOrientationSix, placeholderOnIfBehaviorLocationSix, placeholderOnIfBehaviorSix, fingerOnIfBehaviorSix),
            })
            {
                if (!activate) continue;
                switch (ifType)
                {
                    case IfType.Fingers:
                        switch (placeholderOnIfBehavior)
                        {
                            case PlaceholderOnIfBehavior.FarAway:
                                behaviors.Push(Behavior.visibilityIfNotFarAway(handedness, location));
                                break;
                            case PlaceholderOnIfBehavior.Joined:
                                behaviors.Push(Behavior.visibilityIfFingersNotJoined(handedness, location));
                                break;
                            case PlaceholderOnIfBehavior.NotJoined:
                                behaviors.Push(Behavior.visibilityIfFingersJoined(handedness, location));
                                break;
                        }
                        break;
                    case IfType.Finger:
                        switch (fingerOnIfBehavior)
                        {
                            case FingerOnIfBehavior.Up:
                                behaviors.Push(Behavior.visibilityIfFingerDown(handedness, location));
                                break;
                            case FingerOnIfBehavior.Down:
                                behaviors.Push(Behavior.visibilityIfFingerUp(handedness, location));
                                break;
                        }
                        break;
                    case IfType.Hand:
                        behaviors.Push(Behavior.visibilityIfHandDoesNotFace(handedness, handOrientation));
                        break;
                    // case IfType.Thumb:
                    //     behaviors.Push(Behavior.visibilityOnThumbMovement(handedness));
                    //     break;
                }
                
            }
            return behaviors;
        }
    }

    public enum IfType
    {
        Fingers,
        Finger,
        Hand,
        // Thumb,
    }

    public enum PlaceholderOnIfBehavior
    {
        FarAway,
        Joined,
        NotJoined,
    }

    public enum FingerOnIfBehavior
    {
        Up,
        Down,
    }

    public enum HandOrientation
    {
        Front,
        Back,
        Left,
        Right
    }
}
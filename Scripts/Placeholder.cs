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
        public Location placeholderLocation;
        public List<Tuple<Location, PlaceholderOnIfBehavior>> placeholderOnIfBehaviors = new List<Tuple<Location, PlaceholderOnIfBehavior>>();

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
            
            if (placeholderOnIfBehaviors.Count == 0)
            {
                behaviors.Push(Behavior.nothing(handedness));
            }
            else
            {
                foreach (var (location, onIfBehavior) in placeholderOnIfBehaviors)
                {
                    switch (onIfBehavior)
                    {
                        case PlaceholderOnIfBehavior.FarAway:
                            behaviors.Push(Behavior.transparencyIfNotFarAway(handedness, location));
                            break;
                        case PlaceholderOnIfBehavior.Joined:
                            behaviors.Push(Behavior.transparencyIfFingersNotJoined(handedness, location));
                            break;
                        case PlaceholderOnIfBehavior.NotJoined:
                            behaviors.Push(Behavior.transparencyIfFingersJoined(handedness, location));
                            break;
                    }
                }
            }
            return behaviors;
        }
    }

    public enum PlaceholderOnIfBehavior
    {
        FarAway,
        Joined,
        NotJoined,
    }
}
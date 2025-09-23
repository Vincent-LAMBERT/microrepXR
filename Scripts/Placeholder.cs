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
        public bool activateOnIfBehaviorOne = false;
        public Location onIfBehaviorLocationOne;
        public PlaceholderOnIfBehavior onIfBehaviorOne;
        public bool activateOnIfBehaviorTwo = false;
        public Location onIfBehaviorLocationTwo;
        public PlaceholderOnIfBehavior onIfBehaviorTwo;
        public bool activateOnIfBehaviorThree = false;
        public Location onIfBehaviorLocationThree;
        public PlaceholderOnIfBehavior onIfBehaviorThree;

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
            
            foreach (var (activate, location, onIfBehavior) in new List<(bool, Location, PlaceholderOnIfBehavior)>{
                (activateOnIfBehaviorOne, onIfBehaviorLocationOne, onIfBehaviorOne),
                (activateOnIfBehaviorTwo, onIfBehaviorLocationTwo, onIfBehaviorTwo),
                (activateOnIfBehaviorThree, onIfBehaviorLocationThree, onIfBehaviorThree),
            })
            {
                if (!activate) continue;
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
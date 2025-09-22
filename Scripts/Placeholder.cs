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
        public PlaceholderBehavior placeholderBehavior;
        public UniqueLocation uniqueLocation;
        public JoinedLocation joinedLocation;
        public AwayLocation awayLocation;

        public Placeholder()
        {
            this.uniqueLocation = new UniqueLocation(ActorEnum.Index, ThreeZoneActorZone.Tip);
        }

        public Placeholder(UniqueLocation location) { this.uniqueLocation = location; }
        public Placeholder(JoinedLocation location) { this.joinedLocation = location; }
        public Placeholder(AwayLocation location) { this.awayLocation = location; }

        private PlaceholderBehavior getPlaceholderBehavior() { return placeholderBehavior; }
        private UniqueLocation getUniqueLocation() { return uniqueLocation; }
        private JoinedLocation getJoinedLocation() { return joinedLocation; }
        private AwayLocation getAwayLocation() { return awayLocation; }

        public Location getLocation() { 
            switch (this.getPlaceholderBehavior()) {
                case PlaceholderBehavior.AlwaysVisibleUnique:
                    return uniqueLocation;
                case PlaceholderBehavior.VisibleWhenNotJoined:
                    return uniqueLocation;
                case PlaceholderBehavior.VisibleWhenTwoJoined:
                    return joinedLocation;
                default :
                    return awayLocation;
            }
        }

        public Behavior getBehavior(Handedness handedness) {
            switch (this.getPlaceholderBehavior()) {
                case PlaceholderBehavior.AlwaysVisibleUnique:
                    return Behavior.nothing(handedness);
                case PlaceholderBehavior.VisibleWhenNotJoined:
                    return Behavior.transparencyOnFingerJoined(handedness);
                case PlaceholderBehavior.VisibleWhenTwoJoined:
                    return Behavior.transparencyOnDistance(handedness);
                default :
                    return Behavior.transparencyOnProximity(handedness);
            }
        }
    }

    public enum PlaceholderBehavior 
    {
        AlwaysVisibleUnique,
        VisibleWhenNotJoined,
        VisibleWhenTwoJoined,
        VisibleWhenTwoAway
    }
}
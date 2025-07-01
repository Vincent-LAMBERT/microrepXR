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
        public FingersStatus fingersStatus;
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

        private FingersStatus getFingerStatus() { return fingersStatus; }
        private UniqueLocation getUniqueLocation() { return uniqueLocation; }
        private JoinedLocation getJoinedLocation() { return joinedLocation; }
        private AwayLocation getAwayLocation() { return awayLocation; }

        public Location getLocation() { 
            switch (this.getFingerStatus()) {
                case FingersStatus.Unique:
                    return uniqueLocation;
                case FingersStatus.Joined:
                    return joinedLocation;
                default :
                    return awayLocation;
            }
        }

        public Behavior getFingerStatusBehavior(Handedness handedness) {
            switch (this.getFingerStatus()) {
                case FingersStatus.Unique:
                    return Behavior.nothing(handedness);
                case FingersStatus.Joined:
                    return Behavior.transparencyOnDistance(handedness);
                default :
                    return Behavior.transparencyOnProximity(handedness);
            }
        }
    }

    public enum FingersStatus 
    {
        Unique,
        Joined,
        Away
    }
}
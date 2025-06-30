using System.Numerics;
using System.Diagnostics;
using System.Globalization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using TMPro;
using UnityEngine;
using Microgestures;
// using MixedReality.Toolkit; // old for Handedness
using MixedReality.Toolkit;
// using UnityEngine.XR.Hands; // new for Handedness and XRHandJointID

namespace Microgestures 
{
    public class IndexJoinedMiddle : OneZoneActor
    {
        public override ActorEnum[] getActorTypes() 
            { return new ActorEnum[2]{
                ActorEnum.IndexJoinedMiddle,
                ActorEnum.MiddleJoinedIndex};
            }
        public IndexJoinedMiddle(Handedness handedness) : base(handedness){}

        override protected Tuple<TrackedHandJoint, float>[] getTip() 
            { return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.IndexTip, 0.5f), 
                Tuple.Create(TrackedHandJoint.MiddleTip, 0.5f)}; }
    }

    public class MiddleJoinedRing : OneZoneActor
    {
        public override ActorEnum[] getActorTypes() 
            { return new ActorEnum[2]{
                ActorEnum.MiddleJoinedRing,
                ActorEnum.RingJoinedMiddle};
            }
        public MiddleJoinedRing(Handedness handedness) : base(handedness){}

        override protected Tuple<TrackedHandJoint, float>[] getTip() 
            { return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.MiddleTip, 0.5f), 
                Tuple.Create(TrackedHandJoint.RingTip, 0.5f)}; }
    }

    public class RingJoinedLittle : OneZoneActor
    {
        public override ActorEnum[] getActorTypes() 
            { return new ActorEnum[2]{
                ActorEnum.RingJoinedLittle,
                ActorEnum.LittleJoinedRing};
            }
        public RingJoinedLittle(Handedness handedness) : base(handedness){}

        override protected Tuple<TrackedHandJoint, float>[] getTip() 
            { return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.RingIntermediate, 0.5f), 
                Tuple.Create(TrackedHandJoint.LittleTip, 0.5f)}; }
    }
}
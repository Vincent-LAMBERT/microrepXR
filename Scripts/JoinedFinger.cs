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
    public class IndexJoinedMiddle : ThreeZonesActor
    {
        public override ActorEnum[] getActorTypes() 
            { return new ActorEnum[2]{
                ActorEnum.IndexJoinedMiddle,
                ActorEnum.MiddleJoinedIndex};
            }
        public IndexJoinedMiddle(Handedness handedness) : base(handedness){}

        override public Tuple<TrackedHandJoint, float>[] getTip() 
            { return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.IndexTip, 0.5f), 
                Tuple.Create(TrackedHandJoint.MiddleTip, 0.5f)}; }
        // override public Tuple<TrackedHandJoint, float>[] getCenter() 
        //     { return new Tuple<TrackedHandJoint, float>[2]{
        //         Tuple.Create(TrackedHandJoint.IndexIntermediate, 0.5f), 
        //         Tuple.Create(TrackedHandJoint.MiddleIntermediate, 0.5f)}; }
        // override public Tuple<TrackedHandJoint, float>[] getBasis() 
        //     { return new Tuple<TrackedHandJoint, float>[2]{
        //         Tuple.Create(TrackedHandJoint.IndexProximal, 0.5f), 
        //         Tuple.Create(TrackedHandJoint.MiddleProximal, 0.5f)}; }
        override public Tuple<TrackedHandJoint, float>[] getCenter()
        {
            return new Tuple<TrackedHandJoint, float>[4]{
                Tuple.Create(TrackedHandJoint.IndexTip, 0.125f),
                Tuple.Create(TrackedHandJoint.IndexIntermediate, 0.375f),
                Tuple.Create(TrackedHandJoint.MiddleTip, 0.125f),
                Tuple.Create(TrackedHandJoint.MiddleIntermediate, 0.375f)};
        }
        override public Tuple<TrackedHandJoint, float>[] getBasis() 
            { return new Tuple<TrackedHandJoint, float>[4]{
                Tuple.Create(TrackedHandJoint.IndexIntermediate, 0.275f), 
                Tuple.Create(TrackedHandJoint.IndexProximal, 0.225f),
                Tuple.Create(TrackedHandJoint.MiddleIntermediate, 0.275f), 
                Tuple.Create(TrackedHandJoint.MiddleProximal, 0.225f)}; }
        
        // Had to change the placeholder values from MRTK2 to MRTK3
    }

    public class MiddleJoinedRing : ThreeZonesActor
    {
        public override ActorEnum[] getActorTypes() 
            { return new ActorEnum[2]{
                ActorEnum.MiddleJoinedRing,
                ActorEnum.RingJoinedMiddle};
            }
        public MiddleJoinedRing(Handedness handedness) : base(handedness){}

        override public Tuple<TrackedHandJoint, float>[] getTip() 
            { return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.MiddleTip, 0.5f), 
                Tuple.Create(TrackedHandJoint.RingTip, 0.5f)}; }
        // override public Tuple<TrackedHandJoint, float>[] getCenter() 
        //     { return new Tuple<TrackedHandJoint, float>[2]{
        //         Tuple.Create(TrackedHandJoint.MiddleIntermediate, 0.5f), 
        //         Tuple.Create(TrackedHandJoint.RingIntermediate, 0.5f)}; }
        // override public Tuple<TrackedHandJoint, float>[] getBasis() 
        //     { return new Tuple<TrackedHandJoint, float>[2]{
        //         Tuple.Create(TrackedHandJoint.MiddleProximal, 0.5f), 
        //         Tuple.Create(TrackedHandJoint.RingProximal, 0.5f)}; }
        override public Tuple<TrackedHandJoint, float>[] getCenter()
        {
            return new Tuple<TrackedHandJoint, float>[4]{
                Tuple.Create(TrackedHandJoint.MiddleTip, 0.125f),
                Tuple.Create(TrackedHandJoint.MiddleIntermediate, 0.375f),
                Tuple.Create(TrackedHandJoint.RingTip, 0.125f),
                Tuple.Create(TrackedHandJoint.RingIntermediate, 0.375f)};
        }
        override public Tuple<TrackedHandJoint, float>[] getBasis() 
            { return new Tuple<TrackedHandJoint, float>[4]{
                Tuple.Create(TrackedHandJoint.MiddleIntermediate, 0.275f), 
                Tuple.Create(TrackedHandJoint.MiddleProximal, 0.225f),
                Tuple.Create(TrackedHandJoint.RingIntermediate, 0.275f), 
                Tuple.Create(TrackedHandJoint.RingProximal, 0.225f)}; }
        
        // Had to change the placeholder values from MRTK2 to MRTK3
    }

    public class RingJoinedLittle : ThreeZonesActor
    {
        public override ActorEnum[] getActorTypes() 
            { return new ActorEnum[2]{
                ActorEnum.RingJoinedLittle,
                ActorEnum.LittleJoinedRing};
            }
        public RingJoinedLittle(Handedness handedness) : base(handedness){}

        override public Tuple<TrackedHandJoint, float>[] getTip() 
            { return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.RingTip, 0.5f), 
                Tuple.Create(TrackedHandJoint.LittleTip, 0.5f)}; }
        // override public Tuple<TrackedHandJoint, float>[] getCenter() 
        //     { return new Tuple<TrackedHandJoint, float>[2]{
        //         Tuple.Create(TrackedHandJoint.RingIntermediate, 0.5f), 
        //         Tuple.Create(TrackedHandJoint.LittleIntermediate, 0.5f)}; }
        // override public Tuple<TrackedHandJoint, float>[] getBasis() 
        //     { return new Tuple<TrackedHandJoint, float>[2]{
        //         Tuple.Create(TrackedHandJoint.RingProximal, 0.5f), 
        //         Tuple.Create(TrackedHandJoint.LittleProximal, 0.5f)}; }
        override public Tuple<TrackedHandJoint, float>[] getCenter()
        {
            return new Tuple<TrackedHandJoint, float>[4]{
                Tuple.Create(TrackedHandJoint.RingTip, 0.125f),
                Tuple.Create(TrackedHandJoint.RingIntermediate, 0.375f),
                Tuple.Create(TrackedHandJoint.LittleTip, 0.125f),
                Tuple.Create(TrackedHandJoint.LittleIntermediate, 0.375f)};
        }
        override public Tuple<TrackedHandJoint, float>[] getBasis() 
            { return new Tuple<TrackedHandJoint, float>[4]{
                Tuple.Create(TrackedHandJoint.RingIntermediate, 0.275f), 
                Tuple.Create(TrackedHandJoint.RingProximal, 0.225f),
                Tuple.Create(TrackedHandJoint.LittleIntermediate, 0.275f), 
                Tuple.Create(TrackedHandJoint.LittleProximal, 0.225f)}; }
        
        // Had to change the placeholder values from MRTK2 to MRTK3
    }
}
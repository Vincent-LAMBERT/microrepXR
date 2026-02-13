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
    public class Thumb : TwoZonesActor
    {
        public override ActorEnum[] getActorTypes() 
            { return new ActorEnum[1]{
                ActorEnum.Thumb};
            }

        override public Tuple<TrackedHandJoint, float>[] getTip() 
            { return new Tuple<TrackedHandJoint, float>[1]{
                Tuple.Create(TrackedHandJoint.ThumbTip, 1f)}; }
        override public Tuple<TrackedHandJoint, float>[] getProximal() 
            { return new Tuple<TrackedHandJoint, float>[1]{
                Tuple.Create(TrackedHandJoint.ThumbProximal, 1f)}; }
        public Thumb(Handedness handedness) : base(handedness){}
    }
    
    public class Index : ThreeZonesActor
    {
        public override ActorEnum[] getActorTypes() 
            { return new ActorEnum[1]{
                ActorEnum.Index};
            }
        override public Tuple<TrackedHandJoint, float>[] getTip() 
            { return new Tuple<TrackedHandJoint, float>[1]{
                Tuple.Create(TrackedHandJoint.IndexTip, 1f)}; }
        // override protected Tuple<TrackedHandJoint, float>[] getCenter() 
        //     { return new Tuple<TrackedHandJoint, float>[1]{
        //         Tuple.Create(TrackedHandJoint.IndexIntermediate, 1f)}; }
        // override protected Tuple<TrackedHandJoint, float>[] getBasis() 
        //     { return new Tuple<TrackedHandJoint, float>[1]{
        //         Tuple.Create(TrackedHandJoint.IndexProximal, 1f)}; }
        
        // Had to change the placeholder values from MRTK2 to MRTK3
        override public Tuple<TrackedHandJoint, float>[] getCenter()
        {
            return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.IndexTip, 0.25f),
                Tuple.Create(TrackedHandJoint.IndexIntermediate, 0.75f)};
        }
        override public Tuple<TrackedHandJoint, float>[] getBasis() 
            { return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.IndexIntermediate, 0.55f), 
                Tuple.Create(TrackedHandJoint.IndexProximal, 0.45f)}; }
        public Index(Handedness handedness) : base(handedness){}
    }
    
    public class Middle : ThreeZonesActor
    {
        public override ActorEnum[] getActorTypes() 
            { return new ActorEnum[1]{
                ActorEnum.Middle};
            }
        override public Tuple<TrackedHandJoint, float>[] getTip() 
            { return new Tuple<TrackedHandJoint, float>[1]{
                Tuple.Create(TrackedHandJoint.MiddleTip, 1f)}; }
        // override protected Tuple<TrackedHandJoint, float>[] getCenter() 
        //     { return new Tuple<TrackedHandJoint, float>[1]{
        //         Tuple.Create(TrackedHandJoint.MiddleIntermediate, 1f)}; }
        // override protected Tuple<TrackedHandJoint, float>[] getBasis() 
        //     { return new Tuple<TrackedHandJoint, float>[1]{
        //         Tuple.Create(TrackedHandJoint.MiddleProximal, 1f)}; }
        
        // Had to change the placeholder values from MRTK2 to MRTK3
        override public Tuple<TrackedHandJoint, float>[] getCenter() 
            { return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.MiddleTip, 0.25f),
                Tuple.Create(TrackedHandJoint.MiddleIntermediate, 0.75f)}; }
        override public Tuple<TrackedHandJoint, float>[] getBasis() 
            { return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.MiddleIntermediate, 0.55f), 
                Tuple.Create(TrackedHandJoint.MiddleProximal, 0.45f)}; }
        public Middle(Handedness handedness) : base(handedness){}
    }
    
    public class Ring : ThreeZonesActor
    {
        public override ActorEnum[] getActorTypes() 
            { return new ActorEnum[1]{
                ActorEnum.Ring};
            }
        override public Tuple<TrackedHandJoint, float>[] getTip() 
            { return new Tuple<TrackedHandJoint, float>[1]{
                Tuple.Create(TrackedHandJoint.RingTip, 1f)}; }
        // override protected Tuple<TrackedHandJoint, float>[] getCenter() 
        //     { return new Tuple<TrackedHandJoint, float>[1]{
        //         Tuple.Create(TrackedHandJoint.RingIntermediate, 1f)}; }
        // override protected Tuple<TrackedHandJoint, float>[] getBasis() 
        //     { return new Tuple<TrackedHandJoint, float>[1]{
        //         Tuple.Create(TrackedHandJoint.RingProximal, 1f)}; }
        
        // Had to change the placeholder values from MRTK2 to MRTK3
        override public Tuple<TrackedHandJoint, float>[] getCenter() 
            { return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.RingTip, 0.25f),
                Tuple.Create(TrackedHandJoint.RingIntermediate, 0.75f)}; }
        override public Tuple<TrackedHandJoint, float>[] getBasis() 
            { return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.RingIntermediate, 0.55f), 
                Tuple.Create(TrackedHandJoint.RingProximal, 0.45f)}; }
        public Ring(Handedness handedness) : base(handedness){}
    }
    
    public class Little : ThreeZonesActor
    {
        public override ActorEnum[] getActorTypes() 
            { return new ActorEnum[1]{
                ActorEnum.Little};
            }
        override public Tuple<TrackedHandJoint, float>[] getTip() 
            { return new Tuple<TrackedHandJoint, float>[1]{
                Tuple.Create(TrackedHandJoint.LittleTip, 1f)}; }
        // override protected Tuple<TrackedHandJoint, float>[] getProximal() 
        //     { return new Tuple<TrackedHandJoint, float>[1]{
        //         Tuple.Create(TrackedHandJoint.LittleProximal, 1f)}; }
        
        override public Tuple<TrackedHandJoint, float>[] getCenter() 
            { return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.LittleTip, 0.65f),
                Tuple.Create(TrackedHandJoint.LittleProximal, 0.35f)}; }

        // Had to change the placeholder values from MRTK2 to MRTK3
        override public Tuple<TrackedHandJoint, float>[] getBasis() 
            { return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.LittleTip, 0.35f),
                Tuple.Create(TrackedHandJoint.LittleProximal, 0.65f)}; }
        public Little(Handedness handedness) : base(handedness){}
    }
}
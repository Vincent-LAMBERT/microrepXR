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
// using UnityEngine.XR.Hands; // new for Handedness and TrackedHandJoint

namespace Microgestures 
{
    public class ThumbAwayIndex : FourZonesActor
    {
        public override ActorEnum[] getActorTypes() 
            { return new ActorEnum[2]{
                ActorEnum.ThumbAwayIndex,
                ActorEnum.IndexAwayThumb};
            }
        override public bool isWristOriented() { return true; }
        public ThumbAwayIndex(Handedness handedness) : base(handedness){}

        override protected Tuple<TrackedHandJoint, float>[] getTip() 
            { return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.ThumbTip, 0.5f), 
                Tuple.Create(TrackedHandJoint.IndexTip, 0.5f)}; }
        override protected Tuple<TrackedHandJoint, float>[] getCenter() 
            { return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.ThumbTip, 0.5f), 
                Tuple.Create(TrackedHandJoint.IndexIntermediate, 0.5f)}; }
        override protected Tuple<TrackedHandJoint, float>[] getBasis() 
            { return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.ThumbTip, 0.5f), 
                Tuple.Create(TrackedHandJoint.IndexProximal, 0.5f)}; }
        override protected Tuple<TrackedHandJoint, float>[] getThumbBase() 
            { return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.IndexTip, 0.5f), 
                Tuple.Create(TrackedHandJoint.ThumbProximal, 0.5f)}; }
    }

    public class ThumbAwayMiddle : FourZonesActor
    {
        public override ActorEnum[] getActorTypes() 
            { return new ActorEnum[2]{
                ActorEnum.ThumbAwayMiddle,
                ActorEnum.MiddleAwayThumb};
            }
        override public bool isWristOriented() { return true; }
        public ThumbAwayMiddle(Handedness handedness) : base(handedness){}

        override protected Tuple<TrackedHandJoint, float>[] getTip() 
            { return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.ThumbTip, 0.5f), 
                Tuple.Create(TrackedHandJoint.MiddleTip, 0.5f)}; }
        override protected Tuple<TrackedHandJoint, float>[] getCenter() 
            { return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.ThumbTip, 0.5f), 
                Tuple.Create(TrackedHandJoint.MiddleIntermediate, 0.5f)}; }
        override protected Tuple<TrackedHandJoint, float>[] getBasis() 
            { return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.ThumbTip, 0.5f), 
                Tuple.Create(TrackedHandJoint.MiddleProximal, 0.5f)}; }
        override protected Tuple<TrackedHandJoint, float>[] getThumbBase() 
            { return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.MiddleTip, 0.5f), 
                Tuple.Create(TrackedHandJoint.ThumbProximal, 0.5f)}; }
    }
    
    public class ThumbAwayRing : FourZonesActor
    {
        public override ActorEnum[] getActorTypes() 
            { return new ActorEnum[2]{
                ActorEnum.ThumbAwayRing,
                ActorEnum.RingAwayThumb};
            }
        override public bool isWristOriented() { return true; }
        public ThumbAwayRing(Handedness handedness) : base(handedness){}

        override protected Tuple<TrackedHandJoint, float>[] getTip() 
            { return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.ThumbTip, 0.5f), 
                Tuple.Create(TrackedHandJoint.RingTip, 0.5f)}; }
        override protected Tuple<TrackedHandJoint, float>[] getCenter() 
            { return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.ThumbTip, 0.5f), 
                Tuple.Create(TrackedHandJoint.RingIntermediate, 0.5f)}; }
        override protected Tuple<TrackedHandJoint, float>[] getBasis() 
            { return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.ThumbTip, 0.5f), 
                Tuple.Create(TrackedHandJoint.RingProximal, 0.5f)}; }
        override protected Tuple<TrackedHandJoint, float>[] getThumbBase() 
            { return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.RingTip, 0.5f), 
                Tuple.Create(TrackedHandJoint.ThumbProximal, 0.5f)}; }
    }
    
    public class ThumbAwayLittle : ThreeZonesActor
    {
        public override ActorEnum[] getActorTypes() 
            { return new ActorEnum[2]{
                ActorEnum.ThumbAwayLittle,
                ActorEnum.LittleAwayThumb};
            }
        override public bool isWristOriented() { return true; }
        public ThumbAwayLittle(Handedness handedness) : base(handedness){}

        override protected Tuple<TrackedHandJoint, float>[] getTip() 
            { return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.ThumbTip, 0.5f), 
                Tuple.Create(TrackedHandJoint.LittleTip, 0.5f)}; }
        override protected Tuple<TrackedHandJoint, float>[] getCenter() 
            { return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.ThumbTip, 0.5f), 
                Tuple.Create(TrackedHandJoint.LittleIntermediate, 0.5f)}; }
        override protected Tuple<TrackedHandJoint, float>[] getBasis() 
            { return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.LittleTip, 0.5f), 
                Tuple.Create(TrackedHandJoint.ThumbProximal, 0.5f)}; }
    }
    
    public class IndexAwayMiddle : OneZoneActor
    {
        public override ActorEnum[] getActorTypes() 
            { return new ActorEnum[2]{
                ActorEnum.IndexAwayMiddle,
                ActorEnum.MiddleAwayIndex};
            }
        override public bool isWristOriented() { return true; }
        public IndexAwayMiddle(Handedness handedness) : base(handedness){}

        override protected Tuple<TrackedHandJoint, float>[] getTip() 
            { return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.IndexTip, 0.5f), 
                Tuple.Create(TrackedHandJoint.MiddleTip, 0.5f)}; }
    }
    
    public class MiddleAwayRing : OneZoneActor
    {
        public override ActorEnum[] getActorTypes() 
            { return new ActorEnum[2]{
                ActorEnum.MiddleAwayRing,
                ActorEnum.RingAwayMiddle};
            }
        override public bool isWristOriented() { return true; }
        public MiddleAwayRing(Handedness handedness) : base(handedness){}

        override protected Tuple<TrackedHandJoint, float>[] getTip() 
            { return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.MiddleTip, 0.5f), 
                Tuple.Create(TrackedHandJoint.RingTip, 0.5f)}; }
    }
    
    public class RingAwayLittle : OneZoneActor
    {
        public override ActorEnum[] getActorTypes() 
            { return new ActorEnum[2]{
                ActorEnum.RingAwayLittle,
                ActorEnum.LittleAwayRing};
            }
        override public bool isWristOriented() { return true; }
        public RingAwayLittle(Handedness handedness) : base(handedness){}

        override protected Tuple<TrackedHandJoint, float>[] getTip() 
            { return new Tuple<TrackedHandJoint, float>[2]{
                Tuple.Create(TrackedHandJoint.RingTip, 0.5f), 
                Tuple.Create(TrackedHandJoint.LittleTip, 0.5f)}; }
    }
}
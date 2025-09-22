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
using MixedReality.Toolkit;
using Microgestures;
using UnityEngine.UIElements;

namespace Microgestures 
{
    [Serializable, AddComponentMenu("Location", 0)]
    public class Location
    {
        public ActorEnum actor;

        public OneZoneActorZone oneZoneActorZone;
        public TwoZoneActorZone twoZoneActorZone;
        public ThreeZoneActorZone threeZoneActorZone;

        public Location(ActorEnum actor, OneZoneActorZone zone) {
            this.actor = actor;
            this.oneZoneActorZone = zone;
        }

        public Location(ActorEnum actor, TwoZoneActorZone zone) {
            this.actor = actor;
            this.twoZoneActorZone = zone;
        }

        public Location(ActorEnum actor, ThreeZoneActorZone zone) {
            this.actor = actor;
            this.threeZoneActorZone = zone;
        }

        public Tuple<TrackedHandJoint, float>[] getJoints(Handedness handedness)
        {
            switch (actor)
            {
                case ActorEnum.Thumb: return getJointsForActor(new Thumb(handedness));
                case ActorEnum.Index: return getJointsForActor(new Index(handedness));
                case ActorEnum.Middle: return getJointsForActor(new Middle(handedness));
                case ActorEnum.Ring: return getJointsForActor(new Ring(handedness));
                case ActorEnum.Little: return getJointsForActor(new Little(handedness));
                case ActorEnum.IndexJoinedMiddle: return getJointsForActor(new IndexJoinedMiddle(handedness));
                case ActorEnum.MiddleJoinedIndex: return getJointsForActor(new IndexJoinedMiddle(handedness));
                case ActorEnum.MiddleJoinedRing: return getJointsForActor(new MiddleJoinedRing(handedness));
                case ActorEnum.RingJoinedMiddle: return getJointsForActor(new MiddleJoinedRing(handedness));
                case ActorEnum.RingJoinedLittle: return getJointsForActor(new RingJoinedLittle(handedness));
                case ActorEnum.LittleJoinedRing: return getJointsForActor(new RingJoinedLittle(handedness));
                case ActorEnum.ThumbAwayIndex: return getJointsForActor(new ThumbAwayIndex(handedness));
                case ActorEnum.ThumbAwayMiddle: return getJointsForActor(new ThumbAwayMiddle(handedness));
                case ActorEnum.ThumbAwayRing: return getJointsForActor(new ThumbAwayRing(handedness));
                case ActorEnum.ThumbAwayLittle: return getJointsForActor(new ThumbAwayLittle(handedness));
                case ActorEnum.IndexAwayThumb: return getJointsForActor(new ThumbAwayIndex(handedness));
                case ActorEnum.IndexAwayMiddle: return getJointsForActor(new IndexAwayMiddle(handedness));
                case ActorEnum.MiddleAwayThumb: return getJointsForActor(new ThumbAwayMiddle(handedness));
                case ActorEnum.MiddleAwayIndex: return getJointsForActor(new IndexAwayMiddle(handedness));
                case ActorEnum.MiddleAwayRing: return getJointsForActor(new MiddleAwayRing(handedness));
                case ActorEnum.RingAwayThumb: return getJointsForActor(new ThumbAwayRing(handedness));
                case ActorEnum.RingAwayMiddle: return getJointsForActor(new MiddleAwayRing(handedness));
                case ActorEnum.RingAwayLittle: return getJointsForActor(new RingAwayLittle(handedness));
                case ActorEnum.LittleAwayThumb: return getJointsForActor(new ThumbAwayLittle(handedness));
                case ActorEnum.LittleAwayRing: return getJointsForActor(new RingAwayLittle(handedness));
                default: return null;
            }
        }

        private Tuple<TrackedHandJoint, float>[] getJointsForActor(Actor actor)
        {
            if (OneZoneActorEnum.TryParse(this.actor.ToString(), out OneZoneActorEnum oneZoneActor))
            {   return ((OneZoneActor) actor).getTip(); }
            else if (TwoZoneActorEnum.TryParse(this.actor.ToString(), out TwoZoneActorEnum twoZoneActor))
            {
                if (twoZoneActorZone == TwoZoneActorZone.Tip)
                    return ((TwoZonesActor) actor).getTip();
                else
                    return ((TwoZonesActor) actor).getProximal();
            }
            else if (ThreeZoneActorEnum.TryParse(this.actor.ToString(), out ThreeZoneActorEnum threeZoneActor))
            {
                if (threeZoneActorZone == ThreeZoneActorZone.Tip)
                    return ((ThreeZonesActor) actor).getTip();
                else if (threeZoneActorZone == ThreeZoneActorZone.Center)
                    return ((ThreeZonesActor) actor).getCenter();
                else // Basis
                    return ((ThreeZonesActor) actor).getBasis();
            }
            return null;
        }

        public ActorEnum getActorEnum() { return actor; }

        public static FingerEnum getFingerEnum(ActorEnum actor)
            { return (FingerEnum) Enum.Parse(typeof(FingerEnum), actor.ToString()); }

        public static ActorEnum getActorEnum(FingerEnum actor)
            { return (ActorEnum) Enum.Parse(typeof(ActorEnum), actor.ToString()); }

        public static JoinableFingerEnum getJoinableFingerEnum(ActorEnum actor)
            { return (JoinableFingerEnum) Enum.Parse(typeof(JoinableFingerEnum), actor.ToString()); }

        public static ActorEnum getActorEnum(JoinableFingerEnum actor)
            { return (ActorEnum) Enum.Parse(typeof(ActorEnum), actor.ToString()); }

        public static JoinedWithIndexEnum getJoinedWithIndexEnum(ActorEnum actor)
            { return (JoinedWithIndexEnum) Enum.Parse(typeof(JoinedWithIndexEnum), actor.ToString()); }

        public static ActorEnum getActorEnum(JoinedWithIndexEnum actor)
            { return (ActorEnum) Enum.Parse(typeof(ActorEnum), actor.ToString()); }

        public static JoinedWithMiddleEnum getJoinedWithMiddleEnum(ActorEnum actor)
            { return (JoinedWithMiddleEnum) Enum.Parse(typeof(JoinedWithMiddleEnum), actor.ToString()); }

        public static ActorEnum getActorEnum(JoinedWithMiddleEnum actor)
            { return (ActorEnum) Enum.Parse(typeof(ActorEnum), actor.ToString()); }

        public static JoinedWithRingEnum getJoinedWithRingEnum(ActorEnum actor)
            { return (JoinedWithRingEnum) Enum.Parse(typeof(JoinedWithRingEnum), actor.ToString()); }

        public static ActorEnum getActorEnum(JoinedWithRingEnum actor)
            { return (ActorEnum) Enum.Parse(typeof(ActorEnum), actor.ToString()); }

        public static JoinedWithLittleEnum getJoinedWithLittleEnum(ActorEnum actor)
            { return (JoinedWithLittleEnum) Enum.Parse(typeof(JoinedWithLittleEnum), actor.ToString()); }

        public static ActorEnum getActorEnum(JoinedWithLittleEnum actor)
            { return (ActorEnum) Enum.Parse(typeof(ActorEnum), actor.ToString()); }

        public static AwayToThumbEnum getAwayToThumbEnum(ActorEnum actor)
            { return (AwayToThumbEnum) Enum.Parse(typeof(AwayToThumbEnum), actor.ToString()); }

        public static ActorEnum getActorEnum(AwayToThumbEnum actor)
            { return (ActorEnum) Enum.Parse(typeof(ActorEnum), actor.ToString()); }

        public static AwayToIndexEnum getAwayToIndexEnum(ActorEnum actor)
            { return (AwayToIndexEnum) Enum.Parse(typeof(AwayToIndexEnum), actor.ToString()); }

        public static ActorEnum getActorEnum(AwayToIndexEnum actor)
            { return (ActorEnum) Enum.Parse(typeof(ActorEnum), actor.ToString()); }

        public static AwayToMiddleEnum getAwayToMiddleEnum(ActorEnum actor)
            { return (AwayToMiddleEnum) Enum.Parse(typeof(AwayToMiddleEnum), actor.ToString()); }

        public static ActorEnum getActorEnum(AwayToMiddleEnum actor)
            { return (ActorEnum) Enum.Parse(typeof(ActorEnum), actor.ToString()); }

        public static AwayToRingEnum getAwayToRingEnum(ActorEnum actor)
            { return (AwayToRingEnum) Enum.Parse(typeof(AwayToRingEnum), actor.ToString()); }

        public static ActorEnum getActorEnum(AwayToRingEnum actor)
            { return (ActorEnum) Enum.Parse(typeof(ActorEnum), actor.ToString()); }

        public static AwayToLittleEnum getAwayToLittleEnum(ActorEnum actor)
            { return (AwayToLittleEnum) Enum.Parse(typeof(AwayToLittleEnum), actor.ToString()); }

        public static ActorEnum getActorEnum(AwayToLittleEnum actor)
            { return (ActorEnum) Enum.Parse(typeof(ActorEnum), actor.ToString()); }


        public OneZoneActorZone getOneZoneActorZone() { return oneZoneActorZone; }
        public TwoZoneActorZone getTwoZoneActorZone() { return twoZoneActorZone; }
        public ThreeZoneActorZone getThreeZoneActorZone() { return threeZoneActorZone; }
        
        public static JoinedActorEnum getJoinedActorEnum(ActorEnum actor)
        {
            try
            {
                return (JoinedActorEnum) Enum.Parse(typeof(JoinedActorEnum), actor.ToString());
            }
            catch (ArgumentException)
            {
                return JoinedActorEnum.IndexJoinedMiddle;
            }
        }

        public static AwayActorEnum getAwayActorEnum(ActorEnum actor)
            { 
                try {
                    return (AwayActorEnum) Enum.Parse(typeof(AwayActorEnum), actor.ToString());
                }
                catch (ArgumentException) {
                    return AwayActorEnum.ThumbAwayIndex;
                }
            }

    }

    [Serializable, AddComponentMenu("UniqueLocation", 0)]
    public class UniqueLocation : Location {
        public UniqueLocation(ActorEnum actor, OneZoneActorZone zone) : base(actor, zone) {}
        public UniqueLocation(ActorEnum actor, TwoZoneActorZone zone) : base(actor, zone) {}
        public UniqueLocation(ActorEnum actor, ThreeZoneActorZone zone) : base(actor, zone) {}
    }

    [Serializable, AddComponentMenu("JoinedLocation", 0)]
    public class JoinedLocation : Location {
        public JoinedLocation(ActorEnum actor, OneZoneActorZone zone) : base(actor, zone) {}
        public JoinedLocation(ActorEnum actor, TwoZoneActorZone zone) : base(actor, zone) {}
        public JoinedLocation(ActorEnum actor, ThreeZoneActorZone zone) : base(actor, zone) {}
    }
    [Serializable, AddComponentMenu("AwayLocation", 0)]
    public class AwayLocation : Location {
        public AwayLocation(ActorEnum actor, OneZoneActorZone zone) : base(actor, zone) {}
        public AwayLocation(ActorEnum actor, TwoZoneActorZone zone) : base(actor, zone) {}
        public AwayLocation(ActorEnum actor, ThreeZoneActorZone zone) : base(actor, zone) {}
    }

    public enum ActorEnum  
    {
        Thumb, Index, Middle, Ring, Little,
        IndexJoinedMiddle, MiddleJoinedIndex,
        MiddleJoinedRing, RingJoinedMiddle,
        RingJoinedLittle, LittleJoinedRing,

        ThumbAwayIndex, IndexAwayThumb,
        ThumbAwayMiddle, MiddleAwayThumb,
        ThumbAwayRing, RingAwayThumb,
        ThumbAwayLittle, LittleAwayThumb,
        IndexAwayMiddle, MiddleAwayIndex,
        MiddleAwayRing, RingAwayMiddle,
        RingAwayLittle, LittleAwayRing
    }

    public enum OneZoneActorEnum 
        { IndexAwayMiddle, MiddleAwayIndex,
          MiddleAwayRing, RingAwayMiddle,
          RingAwayLittle, LittleAwayRing }
    public enum TwoZoneActorEnum { Thumb, Little }
    public enum ThreeZoneActorEnum { Index, Middle, Ring, ThumbAwayLittle, LittleAwayThumb,
          IndexJoinedMiddle, MiddleJoinedIndex,
          MiddleJoinedRing, RingJoinedMiddle,
          RingJoinedLittle, LittleJoinedRing}

    public enum FingerEnum  { Thumb, Index, Middle, Ring, Little }


    public enum JoinedActorEnum 
        { IndexJoinedMiddle, MiddleJoinedIndex,
          MiddleJoinedRing, RingJoinedMiddle,
          RingJoinedLittle, LittleJoinedRing
        }

    public enum JoinableFingerEnum  { Index, Middle, Ring, Little }
    public enum JoinedWithIndexEnum  { Middle }
    public enum JoinedWithMiddleEnum  { Index, Ring }
    public enum JoinedWithRingEnum  { Middle, Little }
    public enum JoinedWithLittleEnum  { Ring }

    public enum AwayActorEnum 
    { 
        ThumbAwayIndex, IndexAwayThumb,
        ThumbAwayMiddle, MiddleAwayThumb,
        ThumbAwayRing, RingAwayThumb,
        ThumbAwayLittle, LittleAwayThumb,
        IndexAwayMiddle, MiddleAwayIndex,
        MiddleAwayRing, RingAwayMiddle,
        RingAwayLittle, LittleAwayRing
    }

    public enum AwayToThumbEnum { Index, Middle, Ring, Little }
    public enum AwayToIndexEnum { Thumb, Middle }
    public enum AwayToMiddleEnum { Thumb, Index, Ring }
    public enum AwayToRingEnum { Thumb, Middle, Little }
    public enum AwayToLittleEnum { Thumb, Ring }

    public enum OneZoneActorZone { Tip }
    public enum TwoZoneActorZone { Tip, Proximal }
    public enum ThreeZoneActorZone { Tip, Center, Basis }
}
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

namespace Microgestures 
{
    [Serializable, AddComponentMenu("Location", 0)]
    public class Location
    {
        public ActorEnum actor;

        public OneZoneActorZone oneZoneActorZone;
        public TwoZoneActorZone twoZoneActorZone;
        public ThreeZoneActorZone threeZoneActorZone;
        public FourZoneActorZone fourZoneActorZone;

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

        public Location(ActorEnum actor, FourZoneActorZone zone) {
            this.actor = actor;
            this.fourZoneActorZone = zone;
        }

        public ActorEnum getActorEnum() { return actor; }

        public static FingerEnum getFingerEnum(ActorEnum actor)
            { return (FingerEnum) Enum.Parse(typeof(FingerEnum), actor.ToString()); }

        public static ActorEnum getActorEnum(FingerEnum actor)
            { return (ActorEnum) Enum.Parse(typeof(ActorEnum), actor.ToString()); }

        public static MainJoinedFingerEnum getMainJoinedFingerEnum(ActorEnum actor)
            { return (MainJoinedFingerEnum) Enum.Parse(typeof(MainJoinedFingerEnum), actor.ToString()); }

        public static ActorEnum getActorEnum(MainJoinedFingerEnum actor)
            { return (ActorEnum) Enum.Parse(typeof(ActorEnum), actor.ToString()); }

        public static IndexJoinedFingerEnum getIndexJoinedFingerEnum(ActorEnum actor)
            { return (IndexJoinedFingerEnum) Enum.Parse(typeof(IndexJoinedFingerEnum), actor.ToString()); }

        public static ActorEnum getActorEnum(IndexJoinedFingerEnum actor)
            { return (ActorEnum) Enum.Parse(typeof(ActorEnum), actor.ToString()); }

        public static MiddleJoinedFingerEnum getMiddleJoinedFingerEnum(ActorEnum actor)
            { return (MiddleJoinedFingerEnum) Enum.Parse(typeof(MiddleJoinedFingerEnum), actor.ToString()); }

        public static ActorEnum getActorEnum(MiddleJoinedFingerEnum actor)
            { return (ActorEnum) Enum.Parse(typeof(ActorEnum), actor.ToString()); }

        public static RingJoinedFingerEnum getRingJoinedFingerEnum(ActorEnum actor)
            { return (RingJoinedFingerEnum) Enum.Parse(typeof(RingJoinedFingerEnum), actor.ToString()); }

        public static ActorEnum getActorEnum(RingJoinedFingerEnum actor)
            { return (ActorEnum) Enum.Parse(typeof(ActorEnum), actor.ToString()); }

        public static LittleJoinedFingerEnum getLittleJoinedFingerEnum(ActorEnum actor)
            { return (LittleJoinedFingerEnum) Enum.Parse(typeof(LittleJoinedFingerEnum), actor.ToString()); }

        public static ActorEnum getActorEnum(LittleJoinedFingerEnum actor)
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
        public FourZoneActorZone getFourZoneActorZone() { return fourZoneActorZone; }
    
        public static JoinedActorEnum getJoinedActorEnum(ActorEnum actor)
            { 
                try {
                    return (JoinedActorEnum) Enum.Parse(typeof(JoinedActorEnum), actor.ToString());
                }
                catch (ArgumentException) {
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
        public UniqueLocation(ActorEnum actor, FourZoneActorZone zone) : base(actor, zone) {}
    }

    [Serializable, AddComponentMenu("JoinedLocation", 0)]
    public class JoinedLocation : Location {
        public JoinedLocation(ActorEnum actor, OneZoneActorZone zone) : base(actor, zone) {}
        public JoinedLocation(ActorEnum actor, TwoZoneActorZone zone) : base(actor, zone) {}
        public JoinedLocation(ActorEnum actor, ThreeZoneActorZone zone) : base(actor, zone) {}
        public JoinedLocation(ActorEnum actor, FourZoneActorZone zone) : base(actor, zone) {}
    }
    [Serializable, AddComponentMenu("AwayLocation", 0)]
    public class AwayLocation : Location {
        public AwayLocation(ActorEnum actor, OneZoneActorZone zone) : base(actor, zone) {}
        public AwayLocation(ActorEnum actor, TwoZoneActorZone zone) : base(actor, zone) {}
        public AwayLocation(ActorEnum actor, ThreeZoneActorZone zone) : base(actor, zone) {}
        public AwayLocation(ActorEnum actor, FourZoneActorZone zone) : base(actor, zone) {}
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
        { IndexJoinedMiddle, MiddleJoinedIndex,
          MiddleJoinedRing, RingJoinedMiddle,
          RingJoinedLittle, LittleJoinedRing,

          IndexAwayMiddle, MiddleAwayIndex,
          MiddleAwayRing, RingAwayMiddle,
          RingAwayLittle, LittleAwayRing }
    public enum TwoZoneActorEnum { Thumb, Little }
    public enum ThreeZoneActorEnum { Index, Middle, Ring, ThumbAwayLittle, LittleAwayThumb}
    public enum FourZoneActorEnum 
    { 
        ThumbAwayIndex, IndexAwayThumb,
        ThumbAwayMiddle, MiddleAwayThumb,
        ThumbAwayRing, RingAwayThumb,
    }

    public enum FingerEnum  { Thumb, Index, Middle, Ring, Little }


    public enum JoinedActorEnum 
        { IndexJoinedMiddle, MiddleJoinedIndex,
          MiddleJoinedRing, RingJoinedMiddle,
          RingJoinedLittle, LittleJoinedRing
        }

    public enum MainJoinedFingerEnum  { Index, Middle, Ring, Little }
    public enum IndexJoinedFingerEnum  { Middle }
    public enum MiddleJoinedFingerEnum  { Index, Ring }
    public enum RingJoinedFingerEnum  { Middle, Little }
    public enum LittleJoinedFingerEnum  { Ring }

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
    public enum FourZoneActorZone { Tip, Center, Basis, ThumbBase }
}
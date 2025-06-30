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

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
abstract public class LocationPropertyDrawer : ConditionnalPropertyDrawer
{    
    protected SerializedProperty actorProp;
    protected SerializedProperty oneZoneActorZoneProp;
    protected SerializedProperty twoZoneActorZoneProp;
    protected SerializedProperty threeZoneActorZoneProp;
    protected SerializedProperty fourZoneActorZoneProp;

    protected OneZoneActorZone oneZoneActorZone;
    protected TwoZoneActorZone twoZoneActorZone;
    protected ThreeZoneActorZone threeZoneActorZone;
    protected FourZoneActorZone fourZoneActorZone;

    protected bool oneZoneActor;
    protected bool twoZoneActor;
    protected bool threeZoneActor;
    protected bool fourZoneActor;

    protected void initializeLocationPropertyHeight() {
        propertyHeight = 50;
    }
}


#endif
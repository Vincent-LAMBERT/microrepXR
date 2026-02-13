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
using System.Linq;
using MixedReality.Toolkit;
// using UnityEngine.XR.Hands; // new for Handedness and XRHandJointID


namespace Microgestures 
{
    public abstract class Actor
    {
        protected List<ARObject> arObjects;
        protected HandJointPose pose;
        protected Handedness handedness;

        public Actor(Handedness handedness) { this.handedness = handedness; }
        public abstract void instantiate(Transform transform);       
        public void update()
        {
            foreach (ARObject arObject in arObjects) {
                if (arObject.visibleJoints(handedness, isWristOriented(), out pose))
                {
                    arObject.setPose(pose);
                    arObject.setActive(true);
                }
                else
                {
                    arObject.setActive(false);
                }
            }
        }

        public bool isActorType(ActorEnum actor) {
            return getActorTypes().Contains(actor);
        }
        abstract public ActorEnum[] getActorTypes();
        virtual public bool isWristOriented() { return false; }
        abstract public void add(GameObject gameObject, TransformElements transformElements, Stack<Behavior> behaviors, Location zone, Command command);
    }
    
    public abstract class OneZoneActor : Actor
    {
        public List<ARObject> tip = new List<ARObject>();
        abstract public Tuple<TrackedHandJoint, float>[] getTip(); 

        public OneZoneActor(Handedness handedness) : base(handedness){}


        override public void instantiate(Transform transform)
        {
            arObjects = new List<ARObject>();
            foreach(var t in tip) {
                if (t.obj) {
                    t.instantiate(transform, getTip());
                    arObjects.Add(t);
                }
            }
        }

        public override void add(GameObject gameObject, TransformElements transformElements, Stack<Behavior> behaviors, Location location, Command command)
        {
            OneZoneActorZone zone = location.getOneZoneActorZone();
            ARObject arObj = new ARObject(gameObject, transformElements, command, behaviors);
            tip.Add(arObj);
        }
    }
    
    public abstract class TwoZonesActor : Actor
    {
        public List<ARObject> tip = new List<ARObject>();
        public List<ARObject> proximal = new List<ARObject>();
        abstract public Tuple<TrackedHandJoint, float>[] getTip(); 
        abstract public Tuple<TrackedHandJoint, float>[] getProximal(); 

        public TwoZonesActor(Handedness handedness) : base(handedness){}

        override public void instantiate(Transform transform)
        {
            arObjects = new List<ARObject>();
            foreach(var t in tip) {
                if (t.obj) {
                    t.instantiate(transform, getTip());
                    arObjects.Add(t);
                }
            }
            foreach(var p in proximal) {
                if (p.obj) {
                    p.instantiate(transform, getProximal());
                    arObjects.Add(p);
                }
            }
        }
        
        public override void add(GameObject gameObject, TransformElements transformElements, Stack<Behavior> behaviors, Location location, Command command) {
            TwoZoneActorZone zone = location.getTwoZoneActorZone();
            ARObject arObj = new ARObject(gameObject, transformElements, command, behaviors);
            switch (zone) {
               case TwoZoneActorZone.Tip:
                   tip.Add(arObj);
                   break;
               default :
                   proximal.Add(arObj);
                   break;
           }
        }
    }

    public abstract class ThreeZonesActor : Actor
    {
        public List<ARObject> tip = new List<ARObject>();
        public List<ARObject> center = new List<ARObject>();
        public List<ARObject> basis = new List<ARObject>();
        abstract public Tuple<TrackedHandJoint, float>[] getTip(); 
        abstract public Tuple<TrackedHandJoint, float>[] getCenter(); 
        abstract public Tuple<TrackedHandJoint, float>[] getBasis(); 

        public ThreeZonesActor(Handedness handedness) : base(handedness){}

        override public void instantiate(Transform transform)
        {
            arObjects = new List<ARObject>();
            foreach(var t in tip) {
                if (t.obj) {
                    t.instantiate(transform, getTip());
                    arObjects.Add(t);
                }
            }
            foreach(var c in center) {
                if (c.obj) {
                    c.instantiate(transform, getCenter());
                    arObjects.Add(c);
                }
            }
            foreach(var b in basis) {
                if (b.obj) {
                    b.instantiate(transform, getBasis());
                    arObjects.Add(b);
                }
            }
        }

        public override void add(GameObject gameObject, TransformElements transformElements, Stack<Behavior> behaviors, Location location, Command command) {
            ThreeZoneActorZone zone = location.getThreeZoneActorZone();
            ARObject arObj = new ARObject(gameObject, transformElements, command, behaviors);
            switch (zone) {
               case ThreeZoneActorZone.Tip:
                   tip.Add(arObj);
                   break;
               case ThreeZoneActorZone.Center:
                   center.Add(arObj);
                   break;
               default :
                   basis.Add(arObj);
                   break;
           }
        }
    }
}
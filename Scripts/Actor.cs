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
        public void update(bool state)
        {
            foreach (ARObject arObject in arObjects) {
                if (state && arObject.visibleJoints(handedness, isWristOriented(), out pose))
                {
                    if (arObject.obj)
                    {
                        arObject.obj.transform.position = pose.Position;
                        arObject.obj.transform.rotation = pose.Rotation;
                        arObject.obj.SetActive(true);
                    }
                }
                else
                {
                    arObject.obj.SetActive(false);
                }
            }
        }

        public bool isActorType(ActorEnum actor) {
            return getActorTypes().Contains(actor);
        }
        abstract public ActorEnum[] getActorTypes();
        virtual public bool isWristOriented() { return false; }
        abstract public void add(GameObject gameObject, Stack<Behavior> behaviors, Location zone);
    }
    
    public abstract class OneZoneActor : Actor
    {
        public List<ARObject> tip = new List<ARObject>();
        abstract protected Tuple<TrackedHandJoint, float>[] getTip(); 

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
        
        public override void add(GameObject gameObject, Stack<Behavior> behaviors, Location location) {
            OneZoneActorZone zone = location.getOneZoneActorZone();
            ARObject arObj = new ARObject(gameObject, behaviors);
            tip.Add(arObj);
        }
    }
    
    public abstract class TwoZonesActor : Actor
    {
        public List<ARObject> tip = new List<ARObject>();
        public List<ARObject> proximal = new List<ARObject>();
        abstract protected Tuple<TrackedHandJoint, float>[] getTip(); 
        abstract protected Tuple<TrackedHandJoint, float>[] getProximal(); 

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
        
        public override void add(GameObject gameObject, Stack<Behavior> behaviors, Location location) {
            TwoZoneActorZone zone = location.getTwoZoneActorZone();
            ARObject arObj = new ARObject(gameObject, behaviors);
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
        abstract protected Tuple<TrackedHandJoint, float>[] getTip(); 
        abstract protected Tuple<TrackedHandJoint, float>[] getCenter(); 
        abstract protected Tuple<TrackedHandJoint, float>[] getBasis(); 

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

        public override void add(GameObject gameObject, Stack<Behavior> behaviors, Location location) {
            ThreeZoneActorZone zone = location.getThreeZoneActorZone();
            ARObject arObj = new ARObject(gameObject, behaviors);
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

    public abstract class FourZonesActor : Actor
    {
        public List<ARObject> tip = new List<ARObject>();
        public List<ARObject> center = new List<ARObject>();
        public List<ARObject> basis = new List<ARObject>();
        public List<ARObject> thumbBase = new List<ARObject>();
        abstract protected Tuple<TrackedHandJoint, float>[] getTip(); 
        abstract protected Tuple<TrackedHandJoint, float>[] getCenter(); 
        abstract protected Tuple<TrackedHandJoint, float>[] getBasis(); 
        abstract protected Tuple<TrackedHandJoint, float>[] getThumbBase(); 

        public FourZonesActor(Handedness handedness) : base(handedness){}

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
            foreach(var tB in thumbBase) {
                if (tB.obj) {
                    tB.instantiate(transform, getThumbBase());
                    arObjects.Add(tB);
                }
            }
        }

        public override void add(GameObject gameObject, Stack<Behavior> behaviors, Location location) {
            FourZoneActorZone zone = location.getFourZoneActorZone();
            ARObject arObj = new ARObject(gameObject, behaviors);
            switch (zone) {
               case FourZoneActorZone.Tip:
                   tip.Add(arObj);
                   break;
               case FourZoneActorZone.Center:
                   center.Add(arObj);
                   break;
               case FourZoneActorZone.Basis:
                   basis.Add(arObj);
                   break;
               default :
                   thumbBase.Add(arObj);
                   break;
           }
        }
    }
}
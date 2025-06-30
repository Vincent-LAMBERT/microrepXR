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
using UnityEngine.XR; // new for XRNode
using MixedReality.Toolkit.Subsystems; // used to get the HandsAggregatorSubsystem
using UnityEngine.XR.Management; // used to get the XRGeneralSettings

namespace Microgestures 
{
    [System.Serializable]
    public class Hand
    {
        private Thumb thumb;
        private Index index;
        private Middle middle;
        private Ring ring;
        public Little little;

        private IndexJoinedMiddle indexJoinedMiddle;
        private MiddleJoinedRing middleJoinedRing;
        private RingJoinedLittle ringJoinedLittle;

        private ThumbAwayIndex thumbAwayIndex;
        private ThumbAwayMiddle thumbAwayMiddle;
        private ThumbAwayRing thumbAwayRing;
        private ThumbAwayLittle thumbAwayLittle;
        private IndexAwayMiddle indexAwayMiddle;
        private MiddleAwayRing middleAwayRing;
        private RingAwayLittle ringAwayLittle;

        public Handedness handedness;

        List<Actor> actors;
        List<GameObject> skeleton;

        public Hand(Handedness handedness) {
            this.handedness = handedness;
        }
        
        public void initialize(Artefact[] artefacts)
        {
            this.thumb = new Thumb(this.handedness);
            this.index = new Index(this.handedness);
            this.middle = new Middle(this.handedness);
            this.ring = new Ring(this.handedness);
            this.little = new Little(this.handedness);
            
            this.indexJoinedMiddle = new IndexJoinedMiddle(this.handedness);
            this.middleJoinedRing = new MiddleJoinedRing(this.handedness);
            this.ringJoinedLittle = new RingJoinedLittle(this.handedness);
            
            this.thumbAwayIndex = new ThumbAwayIndex(this.handedness);
            this.thumbAwayMiddle = new ThumbAwayMiddle(this.handedness);
            this.thumbAwayRing = new ThumbAwayRing(this.handedness);
            this.thumbAwayLittle = new ThumbAwayLittle(this.handedness);
            this.indexAwayMiddle = new IndexAwayMiddle(this.handedness);
            this.middleAwayRing = new MiddleAwayRing(this.handedness);
            this.ringAwayLittle = new RingAwayLittle(this.handedness);

            actors = new List<Actor>();

            actors.Add(this.thumb);
            actors.Add(this.index);
            actors.Add(this.middle);
            actors.Add(this.ring);
            actors.Add(this.little);
            
            actors.Add(this.indexJoinedMiddle);
            actors.Add(this.middleJoinedRing);
            actors.Add(this.ringJoinedLittle);
            
            actors.Add(this.thumbAwayIndex);
            actors.Add(this.thumbAwayMiddle);
            actors.Add(this.thumbAwayRing);
            actors.Add(this.thumbAwayLittle);
            actors.Add(this.indexAwayMiddle);
            actors.Add(this.middleAwayRing);
            actors.Add(this.ringAwayLittle);

            foreach (Artefact art in artefacts) { handleArtefact(art); }
        }

        private void handleArtefact(Artefact art) {
            Placeholder ph = art.getPlaceholder();
            place(art.getGameObject(), getAllBehaviors(ph), ph.getLocation());
        }

        private Stack<Behavior> getAllBehaviors(Placeholder ph) {
            Stack<Behavior> behaviors = new Stack<Behavior>();
            behaviors.Push(getCurrentDisplayModeRelatedBehavior(this.handedness));
            behaviors.Push(ph.getFingerStatusBehavior(handedness));
            return behaviors;
        }

        private static Behavior getCurrentDisplayModeRelatedBehavior(Handedness handedness) {
            return Behavior.transparencyOnThumbMovement(handedness);
        }

        private void place(GameObject gameObject, Stack<Behavior> behaviors, Location location) {
            foreach (Actor actor in actors) {
                if (actor.isActorType(location.getActorEnum())) {
                    actor.add(gameObject, behaviors, location);
                    break;
                }
            }
        }

        public void instantiate(Transform transform)
        {
            foreach (Actor actor in actors) { actor.instantiate(transform); }
        }

        public void update(bool state)
        {
            foreach (Actor actor in actors)
            {
                actor.update(state);
            }
        }
    }
}

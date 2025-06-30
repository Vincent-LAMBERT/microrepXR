// using MixedReality.Toolkit; // old for Handedness

using MixedReality.Toolkit;
// using UnityEngine.XR.Hands; // new for Handedness and XRHandJointID
// using MixedReality.Toolkit.Utilities;  // old for MixedRealityHandPose
using UnityEngine; // used for Pose instead of MixedRealityHandPose
using System.Collections.Generic;
using UnityEngine.XR; // new for XRNode
using MixedReality.Toolkit.Subsystems; // used to get the HandsAggregatorSubsystem
using UnityEngine.XR.Management; // used to get the XRGeneralSettings

namespace Microgestures 
{
    [AddComponentMenu("Behavior", 0)]
    public class Behavior
    {
        public BehaviorType type;
        public Handedness handedness;
        private Dictionary<int, float> colorDict = new Dictionary<int, float>();

        public Behavior(BehaviorType type) {
            this.type = type;
            this.handedness = Handedness.None;
        }

        public Behavior(BehaviorType type, Handedness handedness) {
            this.type = type;
            this.handedness = handedness;
        }

        public BehaviorType getType() {
            return type;
        }

        public void setHandedness(Handedness handedness) {
            this.handedness = handedness;
        }

        public Handedness getHandedness() {
            return handedness;
        }


        private IEnumerable<GameObject> GetChildren(GameObject obj)
        {
            for (int i = 0; i < obj.transform.childCount; i++) {
                yield return obj.transform.GetChild(i).gameObject;
            }
        }

        public void setInitialTransparency(GameObject obj) {
            MeshRenderer renderer;
            GameObject child;
            for (int i = 0; i < obj.transform.childCount; i++) {
                child = obj.transform.GetChild(i).gameObject;
                if (child.transform.childCount>0) {
                    setInitialTransparency(child);
                } else {
                    if(child.GetComponent<MeshRenderer>() == null) {
                        renderer = child.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
                    } else {
                        renderer = child.GetComponent<MeshRenderer>();
                    }
                    // Splitting is necessary because the left hand side is a copy
                    colorDict[i] = renderer.sharedMaterial.color.a;
                }
            }
        }

        private void alterTransparency(GameObject obj, float value) {
            MeshRenderer renderer;
            GameObject child;
            for (int i = 0; i < obj.transform.childCount; i++) {
                child = obj.transform.GetChild(i).gameObject;
                if (child.transform.childCount>0) {
                    alterTransparency(child, value);
                } else {
                    if(child.GetComponent<MeshRenderer>() == null) {
                        renderer = child.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
                    } else {
                        renderer = child.GetComponent<MeshRenderer>();
                    }
                    // Splitting is necessary because the left hand side is a copy
                    Color c = renderer.material.color;
                    // Changing the alpha value
                    c.a = colorDict[i]*value;
                    // Reassigning it
                    renderer.material.color = c;
                }
            }
        }

        private void setVisible(GameObject obj, bool value) {
            MeshRenderer renderer;
            GameObject child;
            for (int i = 0; i < obj.transform.childCount; i++) {
                child = obj.transform.GetChild(i).gameObject;
                if (child.transform.childCount>0) {
                    setVisible(child, value);
                } else {
                    if(child.GetComponent<MeshRenderer>() == null) {
                        renderer = child.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
                    } else {
                        renderer = child.GetComponent<MeshRenderer>();
                    }
                    // Reassigning it
                    renderer.enabled = value;
                }
            }
        }

        public void use(GameObject obj, List<UnityEngine.Vector3> positions) {
            switch (this.getType()) {
                case BehaviorType.Nothing:
                    setVisible(obj, true);
                    break;
                case BehaviorType.TransparencyOnThumbMovement:
                    transparencyOnThumbMovementBehavior(obj);
                    break;
                case BehaviorType.TransparencyOnProximity:
                    transparencyOnProximityBehavior(obj, positions);
                    break;
                case BehaviorType.TransparencyOnDistance:
                    transparencyOnDistanceBehavior(obj, positions);
                    break;
                default :
                    useCustom(obj, positions);
                    break;
            }
        }

        private float joinedMinFingersDistance = 60f;
        private float joinedMaxFFingersDistance = 90f;
        private float disjoinedMinFingersDistance = 0f;
        private float disjoinedMaxFFingersDistance = 60f;
        private float thumbDistanceFingersDistance = 70f;

        private void transparencyOnThumbMovementBehavior(GameObject obj) {
            float dist = calculateFingersMinDistanceWithThumb();
            if (dist<=thumbDistanceFingersDistance) {
                setVisible(obj, false);
            } else {
                setVisible(obj, true);
            }
        }

        private void transparencyOnProximityBehavior(GameObject obj, List<UnityEngine.Vector3> positions) {
            transparencyBehavior(obj, positions, disjoinedMinFingersDistance, disjoinedMaxFFingersDistance, 0f, 1f);
        }

        private void transparencyOnDistanceBehavior(GameObject obj, List<UnityEngine.Vector3> positions) {
            transparencyBehavior(obj, positions, joinedMinFingersDistance, joinedMaxFFingersDistance, 1f, 0f);
        }

        private void transparencyBehavior(GameObject obj, List<UnityEngine.Vector3> positions, 
                float lowerLimit, float upperLimit, float lowerTransparency, float upperTransparency) {
            float dist = calculateDistanceBetweenPositions(positions);
            if (dist<=lowerLimit) {
                alterTransparency(obj, lowerTransparency);
            } else if (dist>=upperLimit) {
                alterTransparency(obj, upperTransparency);
            } else {
                float newTransparency = remap(dist, lowerLimit, upperLimit, lowerTransparency, upperTransparency);
                alterTransparency(obj, newTransparency);
            }
        }

        public float calculateDistanceBetweenPositions(List<UnityEngine.Vector3> positions) {
            float distance = 0;
            int n = positions.Count;
            for(int i = 0; i < n; i++) {
                for(int j = 0; j < n/2; j++) {
                    distance += UnityEngine.Vector3.Distance(positions[i], positions[j]);
                }
            }
            int edges = (n*(n-1))/2;
            return (distance*1000)/edges;
        }

        HandJointPose pose;

        public float calculateFingersMinDistanceWithThumb() {
            HandJointPose thumbPose = new HandJointPose();
            List<HandJointPose> positions = new List<HandJointPose>();
            var handSubsystems = new List<HandsAggregatorSubsystem>();
            SubsystemManager.GetSubsystems(handSubsystems);
            for (var i = 0; i < handSubsystems.Count; ++i)
            {
                var handSubsystem = handSubsystems[i];
                if (handSubsystem.running)
                {
                    if (getHandedness() == Handedness.Left)
                    {
                        if (handSubsystem.TryGetJoint(TrackedHandJoint.ThumbTip, XRNode.LeftHand, out pose)) { thumbPose = pose; }
                        if (handSubsystem.TryGetJoint(TrackedHandJoint.IndexTip, XRNode.LeftHand, out pose)) { positions.Add(pose); }
                        if (handSubsystem.TryGetJoint(TrackedHandJoint.MiddleTip, XRNode.LeftHand, out pose)) { positions.Add(pose); }
                        if (handSubsystem.TryGetJoint(TrackedHandJoint.RingTip, XRNode.LeftHand, out pose)) { positions.Add(pose); }
                        if (handSubsystem.TryGetJoint(TrackedHandJoint.LittleTip, XRNode.LeftHand, out pose)) { positions.Add(pose); }
                    } else {
                        if (handSubsystem.TryGetJoint(TrackedHandJoint.ThumbTip, XRNode.RightHand, out pose)) { thumbPose = pose; }
                        if (handSubsystem.TryGetJoint(TrackedHandJoint.IndexTip, XRNode.RightHand, out pose)) { positions.Add(pose); }
                        if (handSubsystem.TryGetJoint(TrackedHandJoint.MiddleTip, XRNode.RightHand, out pose)) { positions.Add(pose); }
                        if (handSubsystem.TryGetJoint(TrackedHandJoint.RingTip, XRNode.RightHand, out pose)) { positions.Add(pose); }
                        if (handSubsystem.TryGetJoint(TrackedHandJoint.LittleTip, XRNode.RightHand, out pose)) { positions.Add(pose); }
                    }
                }
            }
            
            List<float> dists = new List<float>();
            
            float min = 0;
            if (positions.Count>=1) {
                min = UnityEngine.Vector3.Distance(thumbPose.Position, positions[0].Position);
                float tempMin;
                for(int i = 0; i < positions.Count; i++) {
                    tempMin = UnityEngine.Vector3.Distance(thumbPose.Position, positions[i].Position);
                    dists.Add(tempMin);
                    if (tempMin<min) {
                        min = tempMin;
                    }
                }
            }
            return min*1000;
        }

        public float remap(float from, float fromMin, float fromMax, float toMin,  float toMax)
        {
            var fromAbs  =  from - fromMin;
            var fromMaxAbs = fromMax - fromMin;      
        
            var normal = fromAbs / fromMaxAbs;
    
            var toMaxAbs = toMax - toMin;
            var toAbs = toMaxAbs * normal;
    
            var to = toAbs + toMin;
        
            return to;
        }

        public void useCustom(GameObject obj, List<UnityEngine.Vector3> positions) {}

        public static Behavior nothing(Handedness handedness) { 
            return new Behavior(BehaviorType.Nothing, handedness);
        }
        public static Behavior transparencyOnThumbMovement(Handedness handedness) { 
            return new Behavior(BehaviorType.TransparencyOnThumbMovement, handedness);
        }
        public static Behavior transparencyOnProximity(Handedness handedness) { 
            return new Behavior(BehaviorType.TransparencyOnProximity, handedness);
        }
        public static Behavior transparencyOnDistance(Handedness handedness) { 
            return new Behavior(BehaviorType.TransparencyOnDistance, handedness);
        }
    }

    public enum BehaviorType {
        Nothing,
        TransparencyOnThumbMovement,
        TransparencyOnProximity,
        TransparencyOnDistance,
    }
}


using System;
using MixedReality.Toolkit;
// using UnityEngine.XR.Hands; // new for Handedness and XRHandJointID
// using MixedReality.Toolkit.Utilities;  // old for MixedRealityHandPose
using UnityEngine; // used for Pose instead of MixedRealityHandPose
using System.Collections.Generic;
using UnityEngine.XR; // new for XRNode
using MixedReality.Toolkit.Subsystems; // used to get the HandsAggregatorSubsystem
using UnityEngine.XR.Management; // used to get the XRGeneralSettings
using TMPro; // used for TMP_Text

namespace Microgestures 
{
    [AddComponentMenu("Behavior", 0)]
    public class Behavior
    {
        public BehaviorType type;
        public Handedness handedness;
        public Location location;
        private Dictionary<int, float> colorDict = new Dictionary<int, float>();

        public Behavior(BehaviorType type)
        {
            this.type = type;
            this.handedness = Handedness.None;
            this.location = null;
        }

        public Behavior(BehaviorType type, Handedness handedness, Location location)
        {
            this.type = type;
            this.handedness = handedness;
            this.location = location;
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
            GameObject child;
            for (int i = 0; i < obj.transform.childCount; i++) {
                child = obj.transform.GetChild(i).gameObject;
                if (child.transform.childCount>0) {
                    setInitialTransparency(child);
                } else {
                    // Test if the child is named "Command"
                    if (child.name != "Command")
                    {
                        if (child.GetComponent<MeshRenderer>() != null)
                        {
                            MeshRenderer renderer = child.GetComponent<MeshRenderer>();
                            // Splitting is necessary because the left hand side is a copy
                            colorDict[i] = renderer.sharedMaterial.color.a;
                        }
                    }
                }
            }
        }

        private void alterTransparency(GameObject obj, float value) {
            GameObject child;
            for (int i = 0; i < obj.transform.childCount; i++) {
                child = obj.transform.GetChild(i).gameObject;
                if (child.transform.childCount>0) {
                    alterTransparency(child, value);
                } else {
                    // Test if the child is named "Command"
                    if (child.name != "Command")
                    {
                        if (child.GetComponent<MeshRenderer>() != null)
                        {
                            MeshRenderer renderer = child.GetComponent<MeshRenderer>();

                            // Only for a transparent material 
                            // Change the alpha value for each material of the renderer
                            foreach (var mat in renderer.materials)
                            {
                                Color c = mat.color;
                                // Changing the alpha value
                                c.a = colorDict[i] * value;
                                // Reassigning it
                                mat.color = c;
                            }

                        }
                    }
                }
            }
        }
        

        private void setVisible(GameObject obj, bool value)
        {
            GameObject child;
            for (int i = 0; i < obj.transform.childCount; i++)
            {
                child = obj.transform.GetChild(i).gameObject;
                if (child.transform.childCount > 0)
                {
                    setVisible(child, value);
                }
                else
                {
                    MeshRenderer renderer;
                    if (child.GetComponent<MeshRenderer>() == null)
                    {
                        renderer = child.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
                    }
                    else
                    {
                        renderer = child.GetComponent<MeshRenderer>();
                    }
                    // Reassigning it
                    renderer.enabled = value;
                }
            }
        }

        public void use(GameObject obj) {
            switch (this.getType()) {
                case BehaviorType.Nothing:
                    setVisible(obj, true);
                    break;
                case BehaviorType.TransparencyOnThumbMovement:
                    transparencyOnThumbMovementBehavior(obj);
                    break;
                case BehaviorType.TransparencyIfFingerJoined:
                    transparencyIfFingersJoinedBehavior(obj);
                    break;
                case BehaviorType.TransparencyIfNotFarAway:
                    transparencyIfNotFarAwayBehavior(obj);
                    break;
                case BehaviorType.TransparencyIfFingerNotJoined:
                    transparencyIfFingersNotJoinedBehavior(obj);
                    break;
                default :
                    useCustom(obj);
                    break;
            }
        }

        // private float joinedMinFingersDistance = 60f;
        // private float joinedMaxFFingersDistance = 90f;
        private float joinedMinFingersDistance = 25f;
        private float joinedMaxFFingersDistance = 28f;
        private float distanceMinFingersDistance = 22f;
        private float distanceMaxFFingersDistance = 25f;
        private float proximityMinFingersDistance = 70f;
        private float proximityMaxFFingersDistance = 80f;
        // private float thumbDistanceFingersDistance = 70f;
        private float thumbDistanceFingersDistance = 70f;

        private void transparencyOnThumbMovementBehavior(GameObject obj) {
            float dist = calculateFingersMinDistanceWithThumb();
            if (dist<=thumbDistanceFingersDistance) {
                setVisible(obj, false);
            } else {
                setVisible(obj, true);
            }
        }

        private void transparencyIfFingersJoinedBehavior(GameObject obj) {
            Tuple<TrackedHandJoint, float>[] joints = location.getJoints(handedness);
            List<UnityEngine.Vector3> positions = computeInvolvedPositionsFromJoints(joints);
            // There should be only one joint in the list
            UnityEngine.Vector3 position = positions[0];
            List<UnityEngine.Vector3> closePositions = getClosePositions(joints[0].Item1);
            float minDist = float.MaxValue;

            foreach (UnityEngine.Vector3 closePosition in closePositions)
            {
                float dist = calculateDistanceBetweenPositions(new List<UnityEngine.Vector3> { position, closePosition });
                if (dist < minDist)
                {
                    minDist = dist;
                }
            }
            transparencyBehavior(obj, minDist, joinedMinFingersDistance, joinedMaxFFingersDistance, 0f, 1f);
        }

        private List<UnityEngine.Vector3> getClosePositions(TrackedHandJoint joint)
        {
            if (joint == TrackedHandJoint.IndexTip) {
                return new List<UnityEngine.Vector3> {
                    getJointPosition(TrackedHandJoint.MiddleTip)
                };
            } else if (joint == TrackedHandJoint.IndexIntermediate) {
                return new List<UnityEngine.Vector3> {
                    getJointPosition(TrackedHandJoint.MiddleIntermediate)
                };
            } else if (joint == TrackedHandJoint.IndexProximal) {
                return new List<UnityEngine.Vector3> {
                    getJointPosition(TrackedHandJoint.MiddleProximal)
                };
            } else if (joint == TrackedHandJoint.MiddleTip) {
                return new List<UnityEngine.Vector3> {
                    getJointPosition(TrackedHandJoint.IndexTip),
                    getJointPosition(TrackedHandJoint.RingTip),
                };
            } else if (joint == TrackedHandJoint.MiddleIntermediate) {
                return new List<UnityEngine.Vector3> {
                    getJointPosition(TrackedHandJoint.IndexIntermediate),
                    getJointPosition(TrackedHandJoint.RingIntermediate),
                };
            } else if (joint == TrackedHandJoint.MiddleProximal) {
                return new List<UnityEngine.Vector3> {
                    getJointPosition(TrackedHandJoint.IndexProximal),
                    getJointPosition(TrackedHandJoint.RingProximal),
                };
            } else if (joint == TrackedHandJoint.RingTip) {
                return new List<UnityEngine.Vector3> {
                    getJointPosition(TrackedHandJoint.MiddleTip),
                    getJointPosition(TrackedHandJoint.LittleTip),
                };
            } else if (joint == TrackedHandJoint.RingIntermediate) {
                return new List<UnityEngine.Vector3> {
                    getJointPosition(TrackedHandJoint.MiddleIntermediate),
                    getJointPosition(TrackedHandJoint.LittleIntermediate),
                };
            } else if (joint == TrackedHandJoint.RingProximal) {
                return new List<UnityEngine.Vector3> {
                    getJointPosition(TrackedHandJoint.MiddleProximal),
                    getJointPosition(TrackedHandJoint.LittleProximal),
                };
            } else if (joint == TrackedHandJoint.LittleTip) {
                return new List<UnityEngine.Vector3> {
                    getJointPosition(TrackedHandJoint.RingTip),
                };
            } else if (joint == TrackedHandJoint.LittleIntermediate) {
                return new List<UnityEngine.Vector3> {
                    getJointPosition(TrackedHandJoint.RingIntermediate),
                };
            } else if (joint == TrackedHandJoint.LittleProximal) {
                return new List<UnityEngine.Vector3> {
                    getJointPosition(TrackedHandJoint.RingProximal),
                };
            } else {
                return new List<UnityEngine.Vector3> {};
            }
        }

        private UnityEngine.Vector3 getJointPosition(TrackedHandJoint joint)
        {
            HandJointPose pose = new HandJointPose();
            var handSubsystems = new List<HandsAggregatorSubsystem>();
            SubsystemManager.GetSubsystems(handSubsystems);
            for (var i = 0; i < handSubsystems.Count; ++i)
            {
                var handSubsystem = handSubsystems[i];
                if (handSubsystem.running)
                {
                    if (getHandedness() == Handedness.Left)
                    {
                        if (handSubsystem.TryGetJoint(joint, XRNode.LeftHand, out pose))
                        {
                            return pose.Position;
                        }
                    }
                    else
                    {
                        if (handSubsystem.TryGetJoint(joint, XRNode.RightHand, out pose))
                        {
                            return pose.Position;
                        }
                    }
                }
            }
            return new UnityEngine.Vector3(0,0,0);
        }

        private void transparencyIfNotFarAwayBehavior(GameObject obj)
        {
            Tuple<TrackedHandJoint, float>[] joints = location.getJoints(handedness);
            List<UnityEngine.Vector3> positions = computeInvolvedPositionsFromJoints(joints);
            float dist = calculateDistanceBetweenPositions(positions);
            transparencyBehavior(obj, dist, proximityMinFingersDistance, proximityMaxFFingersDistance, 0f, 1f);
        }

        private void transparencyIfFingersNotJoinedBehavior(GameObject obj) {
            Tuple<TrackedHandJoint, float>[] joints = location.getJoints(handedness);
            List<UnityEngine.Vector3> positions = computeInvolvedPositionsFromJoints(joints);
            float dist = calculateDistanceBetweenPositions(positions);
            transparencyBehavior(obj, dist, distanceMinFingersDistance, distanceMaxFFingersDistance, 1f, 0f);
        }

        private void transparencyBehavior(GameObject obj, float dist, 
                float lowerLimit, float upperLimit, float lowerTransparency, float upperTransparency) {
            if (dist <= lowerLimit)
            {
                alterTransparency(obj, lowerTransparency);
            }
            else if (dist >= upperLimit)
            {
                alterTransparency(obj, upperTransparency);
            }
            else
            {
                float newTransparency = remap(dist, lowerLimit, upperLimit, lowerTransparency, upperTransparency);
                alterTransparency(obj, newTransparency);
            }
        }
        
        public List<UnityEngine.Vector3> computeInvolvedPositionsFromJoints(Tuple<TrackedHandJoint, float>[] joints) {
            List<UnityEngine.Vector3> positions = new List<UnityEngine.Vector3>();
            HandJointPose pose;
            var handSubsystems = new List<HandsAggregatorSubsystem>();
            SubsystemManager.GetSubsystems(handSubsystems);
            for (var i = 0; i < handSubsystems.Count; ++i)
            {
                var handSubsystem = handSubsystems[i];
                if (handSubsystem.running)
                {
                    if (getHandedness() == Handedness.Left)
                    {
                        foreach (var joint in joints)
                        {
                            if (handSubsystem.TryGetJoint(joint.Item1, XRNode.LeftHand, out pose))
                            {
                                positions.Add(pose.Position);
                            }
                        }
                    }
                    else
                    {
                        foreach (var joint in joints)
                        {
                            if (handSubsystem.TryGetJoint(joint.Item1, XRNode.RightHand, out pose))
                            {
                                positions.Add(pose.Position);
                            }
                        }
                    }
                }
            }
            return positions;
        }

        public float calculateDistanceBetweenPositions(List<UnityEngine.Vector3> positions)
        {
            float distance = 0;
            int n = positions.Count;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n / 2; j++)
                {
                    distance += UnityEngine.Vector3.Distance(positions[i], positions[j]);
                }
            }
            int edges = (n * (n - 1)) / 2;
            return (distance * 1000) / edges;
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

        public void useCustom(GameObject obj) {}

        public static Behavior nothing(Handedness handedness) { 
            return new Behavior(BehaviorType.Nothing, handedness, null);
        }
        public static Behavior transparencyOnThumbMovement(Handedness handedness) { 
            return new Behavior(BehaviorType.TransparencyOnThumbMovement, handedness, null);
        }
        public static Behavior transparencyIfFingersJoined(Handedness handedness, Location location) { 
            return new Behavior(BehaviorType.TransparencyIfFingerJoined, handedness, location);
        }
        public static Behavior transparencyIfNotFarAway(Handedness handedness, Location location) { 
            return new Behavior(BehaviorType.TransparencyIfNotFarAway, handedness, location);
        }
        public static Behavior transparencyIfFingersNotJoined(Handedness handedness, Location location) { 
            return new Behavior(BehaviorType.TransparencyIfFingerNotJoined, handedness, location);
        }
    }

    public enum BehaviorType {
        Nothing,
        TransparencyOnThumbMovement,
        TransparencyIfFingerJoined,
        TransparencyIfNotFarAway,
        TransparencyIfFingerNotJoined,
    }
}


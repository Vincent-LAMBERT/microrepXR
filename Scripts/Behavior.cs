using System;
using MixedReality.Toolkit;
// using UnityEngine.XR.Hands; // new for Handedness and XRHandJointID
// using MixedReality.Toolkit.Utilities;  // old for MixedRealityHandPose
using UnityEngine; // used for Pose instead of MixedRealityHandPose
using System.Collections.Generic;
using UnityEngine.XR; // new for XRNode
using MixedReality.Toolkit.Subsystems; // used to get the HandsAggregatorSubsystem
using UnityEngine.XR.Management; // used to get the XRGeneralSettings
using TMPro;
using System.Numerics;
using System.Diagnostics.Tracing;
// using System.ComponentModel.DataAnnotations; // used for TMP_Text

namespace Microgestures 
{
    [AddComponentMenu("Behavior", 0)]
    public class Behavior
    {
        public BehaviorType type;
        public Handedness handedness;
        public Location location;
        public HandOrientation handOrientation;
        private Dictionary<int, float> colorDict = new Dictionary<int, float>();

        public Behavior(BehaviorType type)
        {
            this.type = type;
            this.handedness = Handedness.None;
            this.location = null;
            this.handOrientation = HandOrientation.Front;
        }

        public Behavior(BehaviorType type, Handedness handedness)
        {
            this.type = type;
            this.handedness = handedness;
            this.location = null;
            this.handOrientation = HandOrientation.Front;
        }

        public Behavior(BehaviorType type, Handedness handedness, Location location)
        {
            this.type = type;
            this.handedness = handedness;
            this.location = location;
            this.handOrientation = HandOrientation.Front;
        }

        public Behavior(BehaviorType type, Handedness handedness, HandOrientation handOrientation)
        {
            this.type = type;
            this.handedness = handedness;
            this.location = null;
            this.handOrientation = handOrientation;
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
                    } else {
                        // Change the alpha value of the text color
                        TMP_Text m_TextComponent = child.GetComponent<TMP_Text>();
                        m_TextComponent.alpha = value;
                    }
                }
            }
        }

        public Boolean computeVisibility(GameObject obj) {
            switch (this.getType()) {
                case BehaviorType.Nothing:
                    return true;
                case BehaviorType.VisibilityOnThumbMovement:
                    return visibilityOnThumbMovementBehavior(obj);
                case BehaviorType.VisibilityIfFingerJoined:
                    return visibilityIfFingersJoinedBehavior(obj);
                case BehaviorType.VisibilityIfNotFarAway:
                    return visibilityIfNotFarAwayBehavior(obj);
                case BehaviorType.VisibilityIfFingerNotJoined:
                    return visibilityIfFingersNotJoinedBehavior(obj);
                case BehaviorType.VisibilityIfFingerUp:
                    return visibilityIfFingerUpBehavior(obj);
                case BehaviorType.VisibilityIfFingerDown:
                    return visibilityIfFingerDownBehavior(obj);
                case BehaviorType.VisibilityIfHandDoesNotFace:
                    return visibilityIfHandDoesNotFace(obj);
                default :
                    return true;
            }
        }

        // private float joinedMinFingersDistance = 60f;
        // private float joinedMaxFFingersDistance = 90f;
        private float joinedMinFingersDistance = 29f;
        private float joinedMaxFFingersDistance = 32f;
        // private float joinedMinFingersDistance = 40f;
        // private float joinedMaxFFingersDistance = 60f;
        private float distanceMinFingersDistance = 26f;
        private float distanceMaxFFingersDistance = 29f;
        private float proximityMinFingersDistance = 70f;
        private float proximityMaxFFingersDistance = 80f;
        // private float thumbDistanceFingersDistance = 70f;
        private float thumbDistanceFingersDistance = 70f;
        private float minAngle = 0f;
        private float maxAngle = 180f;
        private float closureThresholdAngle = 90;

        private Boolean pinkyIsInvolved(Tuple<TrackedHandJoint, float>[] joints)
        {
            foreach (Tuple<TrackedHandJoint, float> joint in joints)
            {
                TrackedHandJoint trackedJoint = joint.Item1;
                if (trackedJoint == TrackedHandJoint.LittleTip || 
                    trackedJoint == TrackedHandJoint.LittleIntermediate || 
                    trackedJoint == TrackedHandJoint.LittleProximal) {
                    return true;
                }
            }
            return false;
        }

        private Boolean visibilityOnThumbMovementBehavior(GameObject obj) {
            float dist = calculateFingersMinDistanceWithThumb();
            if (dist<=thumbDistanceFingersDistance) {
                // setVisible(obj, false);
                return false;
            } else {
                // setVisible(obj, true);
                return true;
            }
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

        private UnityEngine.Quaternion getJointRotation(TrackedHandJoint joint)
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
                            return pose.Rotation;
                        }
                    }
                    else
                    {
                        if (handSubsystem.TryGetJoint(joint, XRNode.RightHand, out pose))
                        {
                            return pose.Rotation;
                        }
                    }
                }
            }
            return new UnityEngine.Quaternion(0,0,0,0);
        }

        private Boolean visibilityIfNotFarAwayBehavior(GameObject obj)
        {
            Tuple<TrackedHandJoint, float>[] joints = location.getJoints(handedness);
            List<UnityEngine.Vector3> positions = computeInvolvedPositionsFromJoints(joints);
            float dist = calculateDistanceBetweenPositions(positions);
            return visiblityBehavior(obj, dist, proximityMinFingersDistance, proximityMaxFFingersDistance);
        }

        private Boolean visibilityIfFingersJoinedBehavior(GameObject obj) {
            Tuple<TrackedHandJoint, float>[] joints = location.getJoints(handedness);
            bool pinkyInvolved = pinkyIsInvolved(joints); 
            List<UnityEngine.Vector3> positions = computeInvolvedPositionsFromJoints(joints);
            float dist = calculateDistanceBetweenPositions(positions);
            if (pinkyInvolved)
            {
                return visiblityBehavior(obj, dist, joinedMinFingersDistance+5f, joinedMaxFFingersDistance+5f);
            }
            return visiblityBehavior(obj, dist, joinedMinFingersDistance, joinedMaxFFingersDistance);
        }

        private Boolean visibilityIfFingersNotJoinedBehavior(GameObject obj) {
            Tuple<TrackedHandJoint, float>[] joints = location.getJoints(handedness);
            bool pinkyInvolved = pinkyIsInvolved(joints); 
            List<UnityEngine.Vector3> positions = computeInvolvedPositionsFromJoints(joints);
            float dist = calculateDistanceBetweenPositions(positions);
            if (pinkyInvolved)
            {
                return visiblityBehavior(obj, dist, distanceMinFingersDistance+5f, distanceMaxFFingersDistance+5f, true);
            }
                return visiblityBehavior(obj, dist, distanceMinFingersDistance, distanceMaxFFingersDistance, true);
        }

        private Boolean visibilityIfFingerUpBehavior(GameObject obj)
        {
            Tuple<TrackedHandJoint, float>[] jointFinger = location.getJoints(handedness);
            UnityEngine.Quaternion rotation = getJointRotation(jointFinger[0].Item1);
            UnityEngine.Vector3 normalVectorFinger = rotation * UnityEngine.Vector3.up;
            
            Tuple<TrackedHandJoint, float>[] jointsPalm = new Palm(handedness).getTip();
            List<UnityEngine.Vector3> positionsPalm = computeInvolvedPositionsFromJoints(jointsPalm);
            UnityEngine.Vector3 normalVectorPalm = computeNormalVector(positionsPalm);

            float angleDeg = Math.Abs(computeAngleBetweenVectors(normalVectorFinger, normalVectorPalm, 'y'));
            // UnityEngine.Debug.Log("angleDeg: "+angleDeg);
            return visiblityBehavior(obj, angleDeg, closureThresholdAngle, maxAngle);
        }

        private Boolean visibilityIfFingerDownBehavior(GameObject obj)
        {
            Tuple<TrackedHandJoint, float>[] jointFinger = location.getJoints(handedness);
            UnityEngine.Quaternion rotation = getJointRotation(jointFinger[0].Item1);
            UnityEngine.Vector3 normalVectorFinger = rotation * UnityEngine.Vector3.up;
            
            Tuple<TrackedHandJoint, float>[] jointsPalm = new Palm(handedness).getTip();
            List<UnityEngine.Vector3> positionsPalm = computeInvolvedPositionsFromJoints(jointsPalm);
            UnityEngine.Vector3 normalVectorPalm = computeNormalVector(positionsPalm);

            float angleDeg = Math.Abs(computeAngleBetweenVectors(normalVectorFinger, normalVectorPalm, 'y'));
            // UnityEngine.Debug.Log("angleDeg: "+angleDeg);
            return visiblityBehavior(obj, angleDeg, minAngle, closureThresholdAngle, true);
        }

        private Boolean visibilityIfHandDoesNotFace(GameObject obj) {
            Tuple<TrackedHandJoint, float>[] joints = new Palm(handedness).getTip();
            List<UnityEngine.Vector3> positions = computeInvolvedPositionsFromJoints(joints);
            UnityEngine.Vector3 normalVector = computeNormalVector(positions);
            UnityEngine.Vector3 projectedVector = projectVectorOnFloor(normalVector);
            UnityEngine.Vector3 sightVector = getSightVector();

            float angleDeg = computeAngleBetweenVectors(projectedVector, sightVector, 'y');

            Tuple<HandOrientation, float, float> orientBoundaries = getAngleProperties(normalVector, angleDeg);
            HandOrientation orientation = orientBoundaries.Item1;
            float boundary = orientBoundaries.Item2;
            float perfectAngle = orientBoundaries.Item3;
            // UnityEngine.Debug.Log("angleDeg: " + angleDeg + " | orientation: " + orientation);

            if (this.handOrientation==orientation) {
                // setVisible(obj, true);
                // // visiblityBehavior(obj, angleDeg, boundary, perfectAngle, 0f, 1f);
                return true;
            } else {
                // setVisible(obj, false);
                return false;
            }
        }

        private UnityEngine.Vector3 computeNormalVector(List<UnityEngine.Vector3> points)
        {
            if (points.Count != 3)
            {
                throw new System.ArgumentException("The list must have 3 points.");
            }

            UnityEngine.Vector3 p0 = points[0];
            UnityEngine.Vector3 p1 = points[1];
            UnityEngine.Vector3 p2 = points[2];

            UnityEngine.Vector3 v1 = p1 - p0;
            UnityEngine.Vector3 v2 = p2 - p0;
            UnityEngine.Vector3 normal = UnityEngine.Vector3.Cross(v1, v2).normalized;

            return normal;
        }

        private UnityEngine.Vector3 projectVectorOnFloor(UnityEngine.Vector3 vec)
        {
            UnityEngine.Vector3 floorNormal = UnityEngine.Vector3.up;
            float dotProduct = UnityEngine.Vector3.Dot(vec, floorNormal);
            return UnityEngine.Vector3.Normalize(vec - dotProduct * floorNormal);
        }

        private UnityEngine.Vector3 getSightVector()
        {
            Camera headCamera = Camera.main;

            if (headCamera != null)
            {
                // Get the position and rotation of the head
                UnityEngine.Vector3 headPosition = headCamera.transform.position;
                UnityEngine.Quaternion headRotation = headCamera.transform.rotation;

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
                            if (handSubsystem.TryGetJoint(TrackedHandJoint.Wrist, XRNode.LeftHand, out pose))
                            {
                                UnityEngine.Vector3 wristPosition = pose.Position;
                                return UnityEngine.Vector3.Normalize(headPosition - wristPosition);
                            }
                        }
                        else
                        {
                            if (handSubsystem.TryGetJoint(TrackedHandJoint.Wrist, XRNode.RightHand, out pose))
                            {
                                UnityEngine.Vector3 wristPosition = pose.Position;
                                return UnityEngine.Vector3.Normalize(headPosition - wristPosition);
                            }
                        }
                    }
                }
            }
            return UnityEngine.Vector3.zero; 
        }

        private float computeAngleBetweenVectors(UnityEngine.Vector3 vectorA, UnityEngine.Vector3 vectorB, char axe='y')
        {
            // Produit scalaire
            float dotProduct = UnityEngine.Vector3.Dot(vectorA, vectorB);
            // Produit vectoriel (composante Y pour l'orientation)
            float crossProduct = 0f;
            switch (axe)
            {
                case 'x':
                    crossProduct = UnityEngine.Vector3.Cross(vectorA, vectorB).x;
                    break;
                case 'y':
                    crossProduct = UnityEngine.Vector3.Cross(vectorA, vectorB).y;
                    break;
                case 'z':
                    crossProduct = UnityEngine.Vector3.Cross(vectorA, vectorB).z;
                    break;
            }
            // Calcul de l'angle orienté en radians, puis en degrés
            float angleRad = Mathf.Atan2(crossProduct, dotProduct);
            float angleDeg = angleRad * Mathf.Rad2Deg;
            return angleDeg;
        }

        private Tuple<HandOrientation, float, float> getAngleProperties(UnityEngine.Vector3 normalVector, float angleDeg)
        {
            // - is right, + is left, 0 is back, (180/-180) is front, 
            // normalVector[1] is y value (0.75 normalized is the threshold, 
            // higher and the hand is facing up or down somehow)
            Dictionary<(double boundary, double perfectAngle), HandOrientation> orientBoundaries = new Dictionary<(double boundary, double perfectAngle), HandOrientation>();

            orientBoundaries.Add((-45, 0), HandOrientation.Back);
            orientBoundaries.Add((-45, -90), HandOrientation.Right);
            orientBoundaries.Add((-135, -90), HandOrientation.Right);
            orientBoundaries.Add((-135, -180), HandOrientation.Front);
            orientBoundaries.Add((135, 180), HandOrientation.Front);
            orientBoundaries.Add((135, 90), HandOrientation.Left);
            orientBoundaries.Add((45, 90), HandOrientation.Left);
            orientBoundaries.Add((45, 0), HandOrientation.Back);


            // UnityEngine.Debug.Log("Math.Sqrt(normalVector[1]): " + Math.Sqrt(Math.Abs(normalVector[1])) + " --- Math.Sqrt(0.75): "+Math.Sqrt(0.75));
            if (Math.Sqrt(Math.Abs(normalVector[1])) < Math.Sqrt(0.75))
            {
                foreach (var orientBoundary in orientBoundaries) {
                    float boundary = (float) orientBoundary.Key.boundary;
                    float perfectAngle = (float) orientBoundary.Key.perfectAngle;
                    HandOrientation orient = orientBoundary.Value;
                    bool lower_limit = (boundary < angleDeg) & (angleDeg < perfectAngle);
                    bool upper_limit = (boundary > angleDeg) & (angleDeg > perfectAngle);
                    // UnityEngine.Debug.Log("angleDeg: " + angleDeg + " | orientation: " + orient + " --- boundary: "+boundary+" - perfectAngle: "+perfectAngle+" | lower_limit: "+lower_limit+" --- upper_limit: "+upper_limit);
                    if (lower_limit || upper_limit) {
                        return new Tuple<HandOrientation, float, float>(orient, boundary, perfectAngle);
                    }
                }
            }
            // UnityEngine.Debug.Log("ERROR");
            // Hand faces up or down
            return new Tuple<HandOrientation, float, float>(HandOrientation.Back, (float) 999.999, (float)  999.999);
        }

        private Boolean visiblityBehavior(GameObject obj, float dist, 
                float lowerLimit, float upperLimit, bool reversed=false) {
            if (dist <= lowerLimit)
            {
                // alterTransparency(obj, lowerTransparency);
                if (reversed) { return true; }
                return false;
            }
            else if (dist >= upperLimit)
            {
                // alterTransparency(obj, upperTransparency);
                if (reversed) { return false; }
                return true;
            }
            else
            {
                // float newTransparency = remap(dist, lowerLimit, upperLimit, lowerTransparency, upperTransparency);
                // alterTransparency(obj, newTransparency);
                return true;
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

        public static Behavior nothing(Handedness handedness) { 
            return new Behavior(BehaviorType.Nothing, handedness, null);
        }
        public static Behavior visibilityOnThumbMovement(Handedness handedness) { 
            return new Behavior(BehaviorType.VisibilityOnThumbMovement, handedness);
        }
        public static Behavior visibilityIfFingersJoined(Handedness handedness, Location location) { 
            return new Behavior(BehaviorType.VisibilityIfFingerJoined, handedness, location);
        }
        public static Behavior visibilityIfNotFarAway(Handedness handedness, Location location) { 
            return new Behavior(BehaviorType.VisibilityIfNotFarAway, handedness, location);
        }
        public static Behavior visibilityIfFingersNotJoined(Handedness handedness, Location location) { 
            return new Behavior(BehaviorType.VisibilityIfFingerNotJoined, handedness, location);
        }

        public static Behavior visibilityIfFingerUp(Handedness handedness, Location location) { 
            return new Behavior(BehaviorType.VisibilityIfFingerUp, handedness, location);
        }

        public static Behavior visibilityIfFingerDown(Handedness handedness, Location location) { 
            return new Behavior(BehaviorType.VisibilityIfFingerDown, handedness, location);
        }

        public static Behavior visibilityIfHandDoesNotFace(Handedness handedness, HandOrientation handOrientation) {
            return new Behavior(BehaviorType.VisibilityIfHandDoesNotFace, handedness, handOrientation);
        }
    }

    public enum BehaviorType {
        Nothing,
        VisibilityOnThumbMovement,
        VisibilityIfFingerJoined,
        VisibilityIfNotFarAway,
        VisibilityIfFingerNotJoined,
        VisibilityIfFingerUp,
        VisibilityIfFingerDown,
        VisibilityIfHandDoesNotFace,
    }
}


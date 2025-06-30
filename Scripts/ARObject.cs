using System.Diagnostics;
using System.Globalization;
// using MixedReality.Toolkit.Utilities;  // old for MixedRealityHandPose
using UnityEngine; // used for Pose instead of MixedRealityHandPose
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using TMPro;
using Microgestures;
// using UnityEngine.XR.Hands; // new for Handedness and XRHandJointID
using MixedReality.Toolkit;
using UnityEngine.XR; // new for XRNode
using MixedReality.Toolkit.Subsystems; // used to get the HandsAggregatorSubsystem
using UnityEngine.XR.Management; // used to get the XRGeneralSettings

namespace Microgestures 
{
    [System.Serializable]
    public struct ARObject
    {
        public GameObject obj;
        private Stack<Behavior> behaviors;
        Tuple<TrackedHandJoint, float>[] joints;
        HandJointPose localpose;
        List<Vector3> jointPositions;

        public ARObject(GameObject obj, Stack<Behavior> behaviors) {
            this.obj = obj;
            foreach (Behavior b in behaviors) {
                b.setInitialTransparency(obj);
            }
            this.behaviors = behaviors;
            this.joints = null;
            this.localpose = new HandJointPose();;
            this.jointPositions = null;
        }

        public void instantiate(Transform transform, params Tuple<TrackedHandJoint, float>[] joints) 
        {   
            this.obj = UnityEngine.Object.Instantiate(this.obj, transform);
            this.joints = joints;
        }

        public bool visibleJoints(Handedness handedness, bool wristOriented, out HandJointPose pose) {
            jointPositions = new List<Vector3> ();
            UnityEngine.Debug.Log("wristOriented: " + wristOriented);
            try
            {
                if (wristOriented)
                {
                    // pose = getStartingJoint(handedness);
                    pose = getWristOrientedJoint(handedness);
                }
                else
                {
                    pose = getCenterJoint(handedness);
                    UnityEngine.Debug.Log("pose: " + pose);
                }
            }
            catch (InvalidOperationException e)
            {
                pose = new HandJointPose();
                UnityEngine.Debug.Log(e.Message);
                return false;
            }

            Vector3 headPosition = Camera.main.transform.position;
            Vector3 headForward = Camera.main.transform.forward;

            pose = tweakPoseToCorrectHololens(headPosition, pose);        

            useBehaviors(jointPositions);

            return true;
        }

        private HandJointPose tweakPoseToCorrectHololens(Vector3 OH, HandJointPose pose) {
            Vector3 OW = pose.Position;
            Vector3 HW = OW-OH;
            HW.Normalize();
            pose.Position -= HW*0.02f;
            return pose;
        }

        private HandJointPose getWristOrientedJoint(Handedness handedness) {
            Vector3 OA = getStartingJoint(handedness).Position;
            Vector3 OC = getCenterJoint(handedness).Position;
            Vector3 OW = getWristJoint(handedness).Position;

            Vector3 WA = OA-OW;
            Vector3 AC = OC-OA;

            return new HandJointPose(OA, Quaternion.LookRotation(AC, WA), 1.0f);
        }

        private HandJointPose getCenterJoint(Handedness handedness) {
            Vector3 pos = new Vector3();
            Quaternion rot = new Quaternion();
            var handSubsystems = new List<HandsAggregatorSubsystem>();
            SubsystemManager.GetSubsystems(handSubsystems);
            for (var i = 0; i < handSubsystems.Count; ++i)
            {
                var handSubsystem = handSubsystems[i];
                if (handSubsystem.running)
                {
                    if (handedness == Handedness.Left)
                    {
                        foreach (Tuple<TrackedHandJoint, float> joint in joints)
                        {
                            if (handSubsystem.TryGetJoint(joint.Item1, XRNode.LeftHand, out localpose))
                            {
                                pos += localpose.Position * joint.Item2;
                                rot = localpose.Rotation;
                                jointPositions.Add(localpose.Position);
                            }
                            else
                            {
                                throw new InvalidOperationException("Hand not visible");
                            }
                        }
                    }
                    else
                    {
                        foreach (Tuple<TrackedHandJoint, float> joint in joints)
                        {
                            if (handSubsystem.TryGetJoint(joint.Item1, XRNode.RightHand, out localpose))
                            {
                                pos += localpose.Position * joint.Item2;
                                rot = localpose.Rotation;
                                jointPositions.Add(localpose.Position);
                            }
                            else
                            {
                                throw new InvalidOperationException("Hand not visible");
                            }
                        }
                    }                    
                }
            }
            Pose p = new Pose(pos, rot);
            return new HandJointPose(p, 1.0f);
        }

        private HandJointPose getStartingJoint(Handedness handedness)
        {
            var handSubsystems = new List<HandsAggregatorSubsystem>();
            SubsystemManager.GetSubsystems(handSubsystems);
            for (var i = 0; i < handSubsystems.Count; ++i)
            {
                var handSubsystem = handSubsystems[i];
                if (handSubsystem.running)
                {
                    if (handedness == Handedness.Left)
                    {
                        if (handSubsystem.TryGetJoint(joints[0].Item1, XRNode.LeftHand, out localpose))
                        {
                            return localpose;
                        }
                        else
                        {
                            throw new InvalidOperationException("Hand not visible");
                        }
                    } else {
                        if (handSubsystem.TryGetJoint(joints[0].Item1, XRNode.RightHand, out localpose))
                        {
                            return localpose;
                        }
                        else
                        {
                            throw new InvalidOperationException("Hand not visible");
                        }
                    }                    
                }
            }
            return new HandJointPose();
        }

        private HandJointPose getWristJoint(Handedness handedness) {
            var handSubsystems = new List<HandsAggregatorSubsystem>();
            SubsystemManager.GetSubsystems(handSubsystems);
            for (var i = 0; i < handSubsystems.Count; ++i)
            {
                var handSubsystem = handSubsystems[i];
                if (handSubsystem.running)
                {
                    if (handedness == Handedness.Left)
                    {
                        if (handSubsystem.TryGetJoint(TrackedHandJoint.Wrist, XRNode.LeftHand, out localpose))
                        {
                            return localpose;
                        }
                        else
                        {
                            throw new InvalidOperationException("Hand not visible");
                        }
                    } else {
                        if (handSubsystem.TryGetJoint(TrackedHandJoint.Wrist, XRNode.RightHand, out localpose))
                        {
                            return localpose;
                        }
                        else
                        {
                            throw new InvalidOperationException("Hand not visible");
                        }
                    }                    
                }
            }
            return new HandJointPose();
        }

        public void useBehaviors(List<Vector3> positions) {
            foreach (Behavior behavior in behaviors) {
                behavior.use(obj, positions);
            }
        }
    }
}
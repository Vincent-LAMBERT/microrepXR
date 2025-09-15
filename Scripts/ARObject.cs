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
        public TransformElements transformElements;
        public Command command;
        public TextMeshPro textMesh;
        private Stack<Behavior> behaviors;
        Tuple<TrackedHandJoint, float>[] joints;
        HandJointPose localpose;
        List<Vector3> jointPositions;

        public ARObject(GameObject obj, TransformElements transformElements, Command command, Stack<Behavior> behaviors) {
            this.obj = obj;
            this.transformElements = transformElements;
            this.command = command;
            this.textMesh = null;
            foreach (Behavior b in behaviors)
            {
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

            // this.textMesh = this.obj.AddComponent<TextMeshPro>();
            GameObject textObject = new GameObject("Command");
            this.textMesh = textObject.AddComponent<TextMeshPro>();

            // Add the text object as a child of the object
            textObject.transform.SetParent(this.obj.transform);

            // Set text properties
            this.textMesh.text = this.command.text;
            this.textMesh.fontSize = this.command.fontSize;
            this.textMesh.color = this.command.textColor;
            this.textMesh.alignment = TextAlignmentOptions.Center;

            // Enable outline
            this.textMesh.enableVertexGradient = true;
            this.textMesh.outlineWidth = this.command.outlineWidth;
            this.textMesh.outlineColor = this.command.outlineColor;

            // Set scale to 1
            this.textMesh.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        
        public void setPose(HandJointPose pose) {
            if (obj != null)
            {
                obj.transform.position = pose.Position;
                obj.transform.rotation = pose.Rotation;
                
                obj.transform.transform.Translate(new Vector3(this.transformElements.positionX, 0, 0), Space.Self);
                obj.transform.transform.Translate(new Vector3(0, this.transformElements.positionY, 0), Space.Self);
                obj.transform.transform.Translate(new Vector3(0, 0, this.transformElements.positionZ), Space.Self);

                obj.transform.transform.Rotate(new Vector3(this.transformElements.rotationX, 0, 0));
                obj.transform.transform.Rotate(new Vector3(0, this.transformElements.rotationY, 0));
                obj.transform.transform.Rotate(new Vector3(0, 0, this.transformElements.rotationZ));
            }
            if (command != null)
            {
                this.textMesh.transform.position = pose.Position;
                this.textMesh.transform.rotation = pose.Rotation;

                // Add a new rotation to make it face the pulp of the finger
                this.textMesh.transform.Rotate(new Vector3(-90, 0, 0));
                this.textMesh.transform.Rotate(new Vector3(0, 0, 180));

                // Offset the text position based on the textLocation
                float offsetUp = 0.02f; // Adjust this value as needed
                float offsetDown = 0.015f; // Adjust this value as needed
                // Adjust the offset based on the length of the text
                float offsetLeft = 0.0035f * this.command.text.Length; // Adjust this value as needed
                float offsetRight = 0.004f * this.command.text.Length; // Adjust this value as needed
                switch (this.command.textLocation)
                {
                    case TextLocation.Up:
                        this.textMesh.transform.Translate(new Vector3(0, offsetUp, 0), Space.Self);
                        break;
                    case TextLocation.Down:
                        this.textMesh.transform.Translate(new Vector3(0, -offsetDown, 0), Space.Self);
                        break;
                    case TextLocation.Left:
                        this.textMesh.transform.Translate(new Vector3(-offsetLeft, 0, 0), Space.Self);
                        break;
                    case TextLocation.Right:
                        this.textMesh.transform.Translate(new Vector3(offsetRight, 0, 0), Space.Self);
                        break;
                }
                
                this.textMesh.transform.Translate(new Vector3(this.transformElements.positionX, 0, 0), Space.Self);
                this.textMesh.transform.Translate(new Vector3(0, this.transformElements.positionY, 0), Space.Self);
                this.textMesh.transform.Translate(new Vector3(0, 0, this.transformElements.positionZ), Space.Self);

                this.textMesh.transform.Rotate(new Vector3(this.transformElements.rotationX, 0, 0));
                this.textMesh.transform.Rotate(new Vector3(0, this.transformElements.rotationY, 0));
                this.textMesh.transform.Rotate(new Vector3(0, 0, this.transformElements.rotationZ));
            }
        }

        public void setActive(bool state) {
            if (obj != null) {
                obj.SetActive(state);
            }
            if (command != null) {
                this.textMesh.gameObject.SetActive(state);
            }
        }

        public bool visibleJoints(Handedness handedness, bool wristOriented, out HandJointPose pose)
        {
            jointPositions = new List<Vector3>();
            // UnityEngine.Debug.Log("wristOriented: " + wristOriented);
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
                    // UnityEngine.Debug.Log("pose: " + pose);
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